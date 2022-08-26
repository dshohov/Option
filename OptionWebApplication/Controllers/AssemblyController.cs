using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OptionWebApplication.Controllers
{
    public class AssemblyController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
    }
}
