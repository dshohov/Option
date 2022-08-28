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
        //Assembly Home Page(Главная страница сборки)
        public async Task<IActionResult> Index()
        {
            IEnumerable<Assembly> assembly = await _assemblyRepository.GetAll();
            return View(assembly);
        }
        //View details(Посмотреть детально)
        public async Task<IActionResult> Detail(int id)
        {
            Assembly assembly = await _assemblyRepository.GetByIdAsync(id);
            return View(assembly);
        }
        //Search within an assembly(Поиск внутри сборки)
        public async Task<IActionResult> DetailsBySerialNumber(int serialnumber)
        {
            Assembly assemblybyserialnumber = await _assemblyRepository.GetAssemblyBySerialNumber(serialnumber);
            return View(assemblybyserialnumber);
        }
        //Go to creation page(Переход к странице создания)
        public IActionResult Create()
        {
            return View();
        }
        //Post request to the server to create a new class object Assembly(Post запрос на сервер с целью создания нового обьекта класса сборки)
        [HttpPost]
        public async Task<IActionResult> Create(Assembly assembly)
        {
            if (!ModelState.IsValid)
            {
                return View(assembly);
            }
            _assemblyRepository.Add(assembly);
            return RedirectToAction("Index");
        }

    }
}
