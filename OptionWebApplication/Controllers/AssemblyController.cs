using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;

namespace OptionWebApplication.Controllers
{
    public class AssemblyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAssemblyRepository _assemblyRepository;
        public AssemblyController(ApplicationDbContext context, IAssemblyRepository assemblyRepository)
        {
            _context = context;
            _assemblyRepository = assemblyRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Assembly> assembly = await _assemblyRepository.GetAll();
            return View(assembly);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Assembly assembly = await _assemblyRepository.GetByIdAsync(id);
            return View(assembly);
        }

        public async Task<IActionResult> DetailsBySerialNumber(int serialnumber)
        {
            Assembly assemblybyserialnumber = await _assemblyRepository.GetAssemblyBySerialNumber(serialnumber);
            return View(assemblybyserialnumber);
        }
    }
}
