using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using OptionWebApplication.Repository;
using OptionWebApplication.ViewModels;

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
        public async Task<IActionResult> DetailsBySerialNumber(string serialnumber)
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

        public async Task<IActionResult> Delete(int id)
        {
            var assembly = await _assemblyRepository.GetByIdAsync(id);
            if(assembly == null) return View("Error");
            return View(assembly);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAssembly(int id)
        {
            var assembly = await _assemblyRepository.GetByIdAsync(id);
            if (assembly == null) return View("Error");
            
            _assemblyRepository.Delete(assembly);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var assembly = await _assemblyRepository.GetByIdAsync(id);
            if (assembly == null) return View("Error");
            var assemblyVM = new EditAssemblyViewModel
            {
                SerialNumber = assembly.SerialNumber,
                TypeDevice = assembly.TypeDevice,
                ChangeComponents = assembly.ChangeComponents,
                OtherWork = assembly.OtherWork,
                Company = assembly.Company,
                Step1 = assembly.Step1,
                Step2 = assembly.Step2,
                Step3 = assembly.Step3,
                Step4 = assembly.Step4,
                Step5 = assembly.Step5,
                People1 = assembly.People1,
                People2 = assembly.People2,
                People3 = assembly.People3,
                People4 = assembly.People4,
                People5 = assembly.People5
            };
            return View(assemblyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditAssemblyViewModel assemblyVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", assemblyVM);
            }
            
            var assembly = new Assembly
            {
                Id = id,
                SerialNumber = assemblyVM.SerialNumber,
                TypeDevice = assemblyVM.TypeDevice,
                ChangeComponents = assemblyVM.ChangeComponents,
                OtherWork = assemblyVM.OtherWork,
                Company = assemblyVM.Company,
                Step1 = assemblyVM.Step1,
                Step2 = assemblyVM.Step2,
                Step3 = assemblyVM.Step3,
                Step4 = assemblyVM.Step4,
                Step5 = assemblyVM.Step5,
                People1 = assemblyVM.People1,
                People2 = assemblyVM.People2,
                People3 = assemblyVM.People3,
                People4 = assemblyVM.People4,
                People5 = assemblyVM.People5
            };

            _assemblyRepository.Update(assembly);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Pdf(int id)
        {
            var assembly = await _assemblyRepository.GetByIdAsync(id);

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Test.pdf", FileMode.Create));
            doc.Open();
            Paragraph paragraph = new Paragraph("This is my 23 line using Paragraph. \n hi World");
            doc.Add(paragraph);

            List list = new List(List.UNORDERED);
            list.Add(new ListItem("One"));
            list.Add(assembly.TypeDevice);
            list.Add("Three");
            list.Add("Four");
            list.Add("Five");
            list.Add(new ListItem("One"));
            doc.Add(list);
            doc.Close();
            return View();
        }
    }
}
