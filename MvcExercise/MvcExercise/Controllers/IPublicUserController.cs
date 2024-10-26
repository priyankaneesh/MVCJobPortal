using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcExercise.Dtos;
using MvcExercise.Enums;
using MvcExercise.Exceptions;
using MvcExercise.Interfaces;
using MvcExercise.Models;

namespace MvcExercise.Controllers
{
    public class IPublicUserController : Controller
    {
        private IMapper _mapper;
        private readonly IUserService _userService;
        public MvcExerciseDbContext _db;
        public string errorMessage = "";
        public IPublicUserController(IMapper mapper, IUserService userService, MvcExerciseDbContext db)
        {
            _mapper = mapper;
            _userService = userService;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(UserDtos userDtos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(userDtos);
                    if ((_db.Users.Any(x => x.Email == user.Email)))
                        {
                        throw new UserAlreadyExistException("Email already exists");
                    }
                    var RegisterUser = _userService.Register(user);
                    if (RegisterUser != null)
                    {
                        ViewBag.ErrorMessage = "Registration Successfull";
                        return RedirectToAction("Login", "IPublicUser");
                    }
                    ViewBag.ErrorMessage = "Registration Failed";


                }
            }
           
            catch(UserAlreadyExistException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the Login page or Home page
            return RedirectToAction("Login", "IPublicUser");
        }
        [HttpPost]
        public IActionResult Login(LoginDtos loginDtos)
        {
            if (ModelState.IsValid)
            {
                var user1 = _userService.GetUserLogin(loginDtos);
                if (user1 != null)
                {
                    HttpContext.Session.SetString("userId", user1.Id.ToString());
                   
                    if (user1.Role == Roles.JobProvider)
                    {
                        HttpContext.Session.SetString("Role", "JobProvide");
                        return RedirectToAction("Index", "JobProvide");
                    }
                    else if (user1.Role == Roles.JobSeeker)
                    {
                        HttpContext.Session.SetString("Role", "JobSeeker");
                        return RedirectToAction("Index", "JobSeeker");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Role", "CompanyMember");
                        return RedirectToAction("Index", "CompanyMember");
                    }
                }
                else
                {
                    ViewBag.errorMessage = "Login Failed";
                }
            }


            return View();
        }
    }
}
