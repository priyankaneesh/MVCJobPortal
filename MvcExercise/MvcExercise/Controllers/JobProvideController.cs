using Microsoft.AspNetCore.Mvc;

namespace MvcExercise.Controllers
{
    public class JobProvideController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
