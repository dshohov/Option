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
        private readonly IWebHostEnvironment _appEnvironment;
        public GuarenteeController(ApplicationDbContext context, IGuarenteeRepository guarenteeRepository, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _guarenteeRepository = guarenteeRepository;

        }
        //Guarentee Home Page(Главная страница гарантии)
        public IActionResult Index()
        {
            List<Guarentee> guarentees = _context.Guarentes.ToList();
            return View(guarentees);
        }
        //View details(Посмотреть детально)
        public async Task<IActionResult> Detail(int id)
        {
            Guarentee guarentee = await _guarenteeRepository.GetByIdAsync(id);
            _guarenteeRepository.CreatePdf(guarentee);
            return View(guarentee);
        }
        //Search within an Guarentee(Поиск внутри гарантии)
        public async Task<IActionResult> DetailsBySerialNumber(string serialnumber)
        {
            Guarentee guarenteebyserialnumber = await _guarenteeRepository.GetGuarenteeBySerialNumber(serialnumber);
            _guarenteeRepository.CreatePdf(guarenteebyserialnumber);
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
                Company = guarentee.Company,
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
        public async Task<IActionResult> Edit(int id, EditGuarenteeViewModel guarenteeVM, IFormFile uploadedFile, IFormFile setificateFile)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Failed to edit club");
            //    return View("Edit", guarenteeVM);
            //}

            var guarentee = new Guarentee
            {
                Id = id,
                Company = guarenteeVM.Company,
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
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "C:/inetpub/sites/app.option/wwwroot/Files/Guarenrtee/Signature/" + Convert.ToString(guarentee.Id + "Signature.pdf");
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                AssemblyFiles file = new AssemblyFiles { Name = Convert.ToString(guarentee.Id + "Signature"), Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }
            if (setificateFile != null)
            {
                // путь к папке Files
                string path = "C:/inetpub/sites/app.option/wwwroot/Files/Guarenrtee/Sertification/" + Convert.ToString(guarentee.Id + "Sertification.pdf");
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await setificateFile.CopyToAsync(fileStream);
                }
                AssemblyFiles file = new AssemblyFiles { Name = Convert.ToString(guarentee.Id + "Sertification"), Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }
            _guarenteeRepository.Update(guarentee);
            return RedirectToAction("Detail", guarentee);
        }
        public IActionResult GetFileSerialNumber()
        {
            // Путь к файлу
            string file_path = "C:/inetpub/sites/app.option/wwwroot/Files/Document/PdfGuarentee.pdf";
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            return PhysicalFile(file_path, file_type);
        }
        public IActionResult GetSertificationFile(int id)
        {
            string file_path = "C:/inetpub/sites/app.option/wwwroot/Files/Guarenrtee/Sertification/" + Convert.ToString(id) + "Sertification.pdf";
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно

            return PhysicalFile(file_path, file_type);

        }
        public IActionResult GetSignatureFile(int id)
        {
            string file_path = "C:/inetpub/sites/app.option/wwwroot/Files/Guarenrtee/Signature/" + Convert.ToString(id) + "Signature.pdf";
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно

            return PhysicalFile(file_path, file_type);

        }

    }
}
