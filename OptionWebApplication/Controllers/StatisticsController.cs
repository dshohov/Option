using Microsoft.AspNetCore.Mvc;

namespace OptionWebApplication.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
