using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcExercise.Models;

namespace MvcExercise.Controllers
{
    public class JobSeekerController : Controller
    {
        private readonly MvcExerciseDbContext _db;
        private IMapper _mapper;

        public JobSeekerController(MvcExerciseDbContext db)
        {
            _db = db;
        }
        public string errorMessage = "";
        public List<Job> JobList { get; set; }
        [HttpGet]
        public IActionResult Index()
        {
            string userId = HttpContext.Session.GetString("userId");
            Guid id = Guid.Parse(userId);
            var user = _db.Users.Where(x => x.Id == id).FirstOrDefault();

            ViewBag.user = user.FirstName + " " + user.LastName;
            JobList = _db.Jobs.ToList();
            return View(JobList);
        }
        public Job job { get; set; }
        [HttpGet]

        public IActionResult Details(Guid id)
        {

            job = _db.Jobs.Where(x => x.Id == id).FirstOrDefault();
            if (job == null)
            {
                ViewBag.errorMessage = "Job not found";
                return NotFound();
            }
            return View(job);
        }
        //[HttpGet]
        //public IActionResult Apply(Guid id)
        //{

        //    var job = _db.Jobs.FirstOrDefault(x => x.Id == id);
        //    if (job == null)
        //    {
        //        ViewBag.errorMessage = "Job not found";
        //        return View("Error"); // Redirect to an error view if the job is not found
        //    }
        //    ViewBag.Job = job;
        //    return View();
        //}
        [HttpPost]
        public IActionResult Apply(Guid id)
        {

           

            // Assume you have a way to get the current user's ID
            var userId  =Guid.Parse(HttpContext.Session.GetString("userId"));

            // Check if the user has already applied for the job
            bool alreadyApplied = _db.AppliedJobs.Any(aj => aj.JobId == id && aj.UserId == userId);
            if (alreadyApplied)
            {
                ViewBag.ErrorMessage = "You have already applied for this job.";
                return View();
            }

            // Retrieve the job details to apply
            var job = _db.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null)
            {
                ViewBag.ErrorMessage = "Job not found.";
                return RedirectToAction("Index", "JobSeeker"); // Redirect to job listings if job doesn't exist
            }

            // Create a new AppliedJobs entry
            var jobApplication = new AppliedJobs
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                JobId = job.Id,
                AppliedDate = DateTime.Now,
                Job = job,
                Status = "Pending"
            };

            _db.AppliedJobs.Add(jobApplication);
            _db.SaveChanges();

            ViewBag.successMessage = "Job applied successfully!"; 
            AppliedJobsList = _db.AppliedJobs
                                 .Where(aj => aj.UserId == id)
                                 .Include(aj => aj.Job) // Ensures Job details are loaded
                                 .ToList();


            return RedirectToAction("ApplyedJobsList", "JobSeeker");


        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            var userId = Guid.Parse(HttpContext.Session.GetString("userId"));

           ViewBag.appliedJobsCount = _db.AppliedJobs.Count(aj => aj.UserId == userId);
           ViewBag.savedJobsCount = _db.SavedJobs.Count(sj => sj.UserId == userId);
            ViewBag.Name= _db.Users.Where(x => x.Id == userId).FirstOrDefault().FirstName + " " + _db.Users.Where(x => x.Id == userId).FirstOrDefault().LastName;
            ViewBag.Email = _db.Users.Where(x => x.Id == userId).FirstOrDefault().Email;
            return View();
        }
        [HttpPost]

        public IActionResult SaveJobs(Guid id)
        {
            var userId = Guid.Parse(HttpContext.Session.GetString("userId"));

            // Check if the user has already Saved for the job
            bool alreadySaved = _db.SavedJobs.Any(sj => sj.JobId == id && sj.UserId == userId);
            if (alreadySaved)
            {
                ViewBag.errorMessage = "You have already saved this job.";
            }

            // Create a new SavedJobs entry
            var savedJob = new SavedJobs
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                JobId = id,
                SavedDate = DateTime.Now
            };

 
           
            _db.SavedJobs.Add(savedJob);
            _db.SaveChanges();

            ViewBag.successMessage = "Job saved successfully!";
            AppliedJobsList = _db.AppliedJobs
                                 .Where(aj => aj.UserId == id)
                                 .Include(aj => aj.Job) // Ensures Job details are loaded
                                 .ToList();


            return RedirectToAction("SavedJobsList", "JobSeeker"); // Pass the list to the view if it contains entries

        }
        public List<AppliedJobs> AppliedJobsList { get; set; } = new List<AppliedJobs>();
        [HttpGet]

        public IActionResult ApplyedJobsList()
        {
            string userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                ViewBag.errorMessage = "User is not logged in.";
                return View();
            }

            Guid id = Guid.Parse(userId);

            // Retrieve applied jobs for the user, including job details
            AppliedJobsList = _db.AppliedJobs
                                 .Where(aj => aj.UserId == id)
                                 .Include(aj => aj.Job) // Ensures Job details are loaded
                                 .ToList();

            if (AppliedJobsList.Any())
            {
                return View(AppliedJobsList); // Pass the list to the view if it contains entries
            }
            else
            {
                ViewBag.errorMessage = "No Jobs Applied";
            }

            return View();
        }
        public List<SavedJobs> SavedJobsLists { get; set; } = new List<SavedJobs>();
        [HttpGet]

        public IActionResult SavedJobsList()
        {
            string userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                ViewBag.errorMessage = "User is not logged in.";
                return View();
            }

            Guid id = Guid.Parse(userId);

            // Retrieve saved jobs for the user, including job details
SavedJobsLists = _db.SavedJobs.Where(sj => sj.UserId == id).Include(sj => sj.Job).ToList();

            if (SavedJobsLists.Any())
            {
                return View(SavedJobsLists); // Pass the list to the view if it contains entries
            }
            else
            {
                ViewBag.errorMessage = "No Jobs Saved";
            }

            return View();

        }
       
        [HttpPost]
public IActionResult SaveRemove(Guid id)
        {
            string userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                ViewBag.errorMessage = "User is not logged in.";
                return View();
            }
            Guid uid = Guid.Parse(userId);
            var savedJob = _db.SavedJobs.FirstOrDefault(sj => sj.Id == id );


            if (savedJob != null) {

                SavedJobsLists.Remove(savedJob);
                _db.SavedJobs.Remove(savedJob);
                _db.SaveChanges();

                ViewBag.successMessage = "Job removed from saved list.";
                return RedirectToAction("SavedJobsList", "JobSeeker");

            }

            ViewBag.errorMessage = "Error while removing job from saved list.";
            return View();
        }
        
    }

    
}
