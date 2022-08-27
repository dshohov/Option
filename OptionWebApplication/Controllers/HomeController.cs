using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using System.Diagnostics;

namespace OptionWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGuarenteeRepository _guarenteeRepository;
        private readonly IAssemblyRepository _assemblyRepository;

        public HomeController(ApplicationDbContext context, IAssemblyRepository assemblyRepository, IGuarenteeRepository guarenteeRepository)
        {
            _context = context;
            _assemblyRepository = assemblyRepository;
            _guarenteeRepository = guarenteeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> DetailsBySerialNumber(int serialnumber)
        {
            Assembly assemblybyserialnumber = await _assemblyRepository.GetAssemblyBySerialNumber(serialnumber);
            Guarentee guarenteebyserialnumber = await _guarenteeRepository.GetGuarenteeBySerialNumber(serialnumber);
            if (assemblybyserialnumber != null)
                return View("ADetailsBySerialNumber",assemblybyserialnumber);
            if(guarenteebyserialnumber != null)
                return View("GDetailsBySerialNumber",guarenteebyserialnumber);
            return View("Index");
            
        }
    }
}