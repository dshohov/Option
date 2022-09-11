using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using OptionWebApplication.ViewModels;

namespace OptionWebApplication.Controllers
{
    public class AssemblyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAssemblyRepository _assemblyRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        public AssemblyController(ApplicationDbContext context, IAssemblyRepository assemblyRepository, IWebHostEnvironment appEnvironment)
        {

            _context = context;
            _appEnvironment = appEnvironment;
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
           
            _assemblyRepository.CreatePdf(assembly);
            return View(assembly);
        }
        //Search within an assembly(Поиск внутри сборки)
        public async Task<IActionResult> DetailsBySerialNumber(string serialnumber)
        {
            Assembly assemblybyserialnumber = await _assemblyRepository.GetAssemblyBySerialNumber(serialnumber);
            _assemblyRepository.CreatePdf(assemblybyserialnumber);
            return View(assemblybyserialnumber);
        }
        //Go to creation page(Переход к странице создания)
        public IActionResult Create()
        {
            return View("Create");
        }
        //Post request to the server to create a new class object Assembly(Post запрос на сервер с целью создания нового обьекта класса сборки)
        [HttpPost]
        public async Task<IActionResult> Create(Assembly assembly)
        {

           
            assembly.CheckEngenire = false;
            assembly.PartySerialNumber = assembly.SerialNumber + "-" + Convert.ToString( Convert.ToInt32(assembly.SerialNumber) + assembly.Party -1); 
            _assemblyRepository.Add(assembly);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var assembly = await _assemblyRepository.GetByIdAsync(id);
            if (assembly == null) return View("Error");
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
                DateCreate = assembly.DateCreate,
                SerialNumber = assembly.SerialNumber,
                Company = assembly.Company,
                TypeDevice = assembly.TypeDevice,
                CheckEngenire = assembly.CheckEngenire,
                Party = assembly.Party,
                Component = assembly.Component,
                ChangeComponents = assembly.ChangeComponents,
                OtherWork = assembly.OtherWork,
                
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
        public async Task<IActionResult> Edit(int id, EditAssemblyViewModel assemblyVM, IFormFile uploadedFile, IFormFile setificateFile)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Failed to edit club");
            //    return View("Edit", assemblyVM);
            //}

            var assembly = new Assembly
            {
                Id = id,
                SerialNumber = assemblyVM.SerialNumber,
                Company = assemblyVM.Company,
                TypeDevice = assemblyVM.TypeDevice,
                Party = assemblyVM.Party,
                CheckEngenire = assemblyVM.CheckEngenire,
                DateCreate = assemblyVM.DateCreate,
                Component = assemblyVM.Component,
                ChangeComponents = assemblyVM.ChangeComponents,
                OtherWork = assemblyVM.OtherWork,

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
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/Assembly/Signature/" + Convert.ToString(assembly.Id + "Signature.pdf");
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                AssemblyFiles file = new AssemblyFiles { Name = Convert.ToString(assembly.Id + "Signature"), Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }
            if (setificateFile != null)
            {
                // путь к папке Files
                string path = "/Files/Assembly/Sertification/" + Convert.ToString(assembly.Id + "Sertification.pdf");
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await setificateFile.CopyToAsync(fileStream);
                }
                AssemblyFiles file = new AssemblyFiles { Name = Convert.ToString(assembly.Id + "Sertification"), Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();   
            }
            assembly.PartySerialNumber = assembly.SerialNumber + "-" + Convert.ToString(Convert.ToInt32(assembly.SerialNumber) + assembly.Party - 1);
            _assemblyRepository.Update(assembly);
            return RedirectToAction("Index");
        }
        public IActionResult GetFileSerialNumber()
        {
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "PdfAssembly.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            return PhysicalFile(file_path, file_type);
        }
        public IActionResult GetSertificationFile(int id)
        {
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "C:/Users/User/Desktop/Option/OptionWebApplication/wwwroot/Files/Assembly/Sertification/" + Convert.ToString(id) + "Sertification.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            
            return PhysicalFile(file_path, file_type);

        }
        public IActionResult GetSignatureFile(int id)
        {
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "C:/Users/User/Desktop/Option/OptionWebApplication/wwwroot/Files/Assembly/Signature/" + Convert.ToString(id) + "Signature.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно

            return PhysicalFile(file_path, file_type);

        }
    }
}
 