using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Models;

namespace OptionWebApplication.Controllers
{
    public class AssemblyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AssemblyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Assembly> assembly = _context.Assemblies.ToList();
            return View(assembly);  
        }
    }
}
