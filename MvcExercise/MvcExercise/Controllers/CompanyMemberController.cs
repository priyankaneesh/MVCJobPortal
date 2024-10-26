using Microsoft.AspNetCore.Mvc;

namespace MvcExercise.Controllers
{
    public class CompanyMemberController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
