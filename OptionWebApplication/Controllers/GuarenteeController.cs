using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;

namespace OptionWebApplication.Controllers
{
    public class GuarenteeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGuarenteeRepository _guarenteeRepository;
        public GuarenteeController(ApplicationDbContext context, IGuarenteeRepository guarenteeRepository)
        {
            _context = context;
            _guarenteeRepository = guarenteeRepository;

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
        public async Task<IActionResult> DetailsBySerialNumber(int serialnumber)
        {
            Guarentee guarenteebyserialnumber = await _guarenteeRepository.GetGuarenteeBySerialNumber(serialnumber);
            return View(guarenteebyserialnumber);
        }
    }
}
