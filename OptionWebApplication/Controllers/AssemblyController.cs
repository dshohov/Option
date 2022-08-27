using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Detail(int id)
        {
            Assembly assembly = _context.Assemblies.FirstOrDefault(c => c.Id == id);
            return View(assembly);
        }
    }
}
