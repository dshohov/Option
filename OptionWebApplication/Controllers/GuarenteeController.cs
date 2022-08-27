using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Models;

namespace OptionWebApplication.Controllers
{
    public class GuarenteeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GuarenteeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Guarentee> guarentees = _context.Guarentes.ToList();
            return View(guarentees);
        }
        public IActionResult Detail(int id)
        {
            Guarentee guarentee = _context.Guarentes.FirstOrDefault(c => c.Id == id);
            return View(guarentee);
        }
    }
}
