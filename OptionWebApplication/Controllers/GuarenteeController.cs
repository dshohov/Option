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
        //Guarentee Home Page(Главная страница гарантии)
        public IActionResult Index()
        {
            List<Guarentee> guarentees = _context.Guarentes.ToList();
            return View(guarentees);
        }
        //View details(Посмотреть детально)
        public IActionResult Detail(int id)
        {
            Guarentee guarentee = _context.Guarentes.FirstOrDefault(c => c.Id == id);
            return View(guarentee);
        }
        //Search within an Guarentee(Поиск внутри гарантии)
        public async Task<IActionResult> DetailsBySerialNumber(int serialnumber)
        {
            Guarentee guarenteebyserialnumber = await _guarenteeRepository.GetGuarenteeBySerialNumber(serialnumber);
            return View(guarenteebyserialnumber);
        }
        //Go to creation page(Переход к странице создания)
        public IActionResult Create()
        {
            return View();
        }
        //Post request to the server to create a new class object Assembly(Post запрос на сервер с целью создания нового обьекта класса сборки)
        [HttpPost]
        public async Task<IActionResult> Create(Guarentee guarentee)
        {
            if (!ModelState.IsValid)
            {
                return View(guarentee);
            }
            _guarenteeRepository.Add(guarentee);
            return RedirectToAction("Index");
        }
    }
}
