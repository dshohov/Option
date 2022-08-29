using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using OptionWebApplication.ViewModels;

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

        public async Task<IActionResult> Delete(int id)
        {
            var guarentee = await _guarenteeRepository.GetByIdAsync(id);
            if (guarentee == null) return View("Error");
            return View(guarentee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGuarentee(int id)
        {
            var guarentee = await _guarenteeRepository.GetByIdAsync(id);
            if (guarentee == null) return View("Error");

            _guarenteeRepository.Delete(guarentee);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var guarentee = await _guarenteeRepository.GetByIdAsync(id);
            if (guarentee == null) return View("Error");
            var guarenteeVM = new EditGuarenteeViewModel
            {
                SerialNumber = guarentee.SerialNumber,
                TypeDevice = guarentee.TypeDevice,
                DateIn = guarentee.DateIn,
                DateOut = guarentee.DateOut,
                Details = guarentee.Details,
                FaultDetection = guarentee.FaultDetection,
                Conclusion = guarentee.Conclusion,
                DiagnosticPeople = guarentee.DiagnosticPeople,
                ComplectedWork = guarentee.ComplectedWork,
                RepairPeople = guarentee.RepairPeople
            };
            return View(guarenteeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGuarenteeViewModel guarenteeVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", guarenteeVM);
            }

            var guarentee = new Guarentee
            {
                Id = id,
                SerialNumber = guarenteeVM.SerialNumber,
                TypeDevice = guarenteeVM.TypeDevice,
                DateIn = guarenteeVM.DateIn,
                DateOut = guarenteeVM.DateOut,
                Details = guarenteeVM.Details,
                FaultDetection = guarenteeVM.FaultDetection,
                Conclusion = guarenteeVM.Conclusion,
                DiagnosticPeople = guarenteeVM.DiagnosticPeople,
                ComplectedWork = guarenteeVM.ComplectedWork,
                RepairPeople = guarenteeVM.RepairPeople
            };

            _guarenteeRepository.Update(guarentee);
            return RedirectToAction("Index");
        }
    }
}
