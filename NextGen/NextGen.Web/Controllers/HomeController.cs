using Microsoft.AspNetCore.Mvc;

namespace NextGen.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
