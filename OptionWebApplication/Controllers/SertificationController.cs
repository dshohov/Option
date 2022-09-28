using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using OptionWebApplication.ViewModels;

namespace OptionWebApplication.Controllers
{
    public class SertificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISertificationRepository _sertificationRepository;
        
        private readonly IWebHostEnvironment _appEnvironment;
        public SertificationController(ApplicationDbContext context,ISertificationRepository sertificationRepository, IWebHostEnvironment appEnvironment)
        {
            _sertificationRepository = sertificationRepository;
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Sertification> sertifications = await _sertificationRepository.GetAll();
            return View(sertifications);
        }
        public async Task<IActionResult> OldSertification()
        {
            IEnumerable<Sertification> sertifications = await _sertificationRepository.GetAll();
            return View(sertifications);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Sertification sertification = await _sertificationRepository.GetByIdAsync(id);
            return View(sertification);
        }
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sertification sertification, IFormFile uploadedFileSertification)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Create", sertification);
            }
            sertification.State = false;
            
            _sertificationRepository.Add(sertification);
            if (sertification.Name == null)
            {
                sertification.Name = "Спецификация № " + sertification.Id;
            }
            if (uploadedFileSertification != null)
            {
                // путь к папке Files
                string path = "/Files/Sertification/" + Convert.ToString(sertification.Id + "Sertification.pdf");
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFileSertification.CopyToAsync(fileStream);
                }
                AssemblyFiles file = new AssemblyFiles { Name = Convert.ToString(sertification.Id + "Sertification"), Path = path };
                _context.Files.Add(file);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            var sertification = await _sertificationRepository.GetByIdAsync(id);
            if (sertification == null) return View("Error");
            return View(sertification);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSertification(int id)
        {
            var assembly = await _sertificationRepository.GetByIdAsync(id);
            if (assembly == null) return View("Error");

            _sertificationRepository.Delete(assembly);
            return RedirectToAction("Index");

        }
        public IActionResult GetSertification(int id)
        {
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "C:/inetpub/sites/app.option/wwwroot/Files/Sertification/" + Convert.ToString(id) + "Sertification.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно

            return PhysicalFile(file_path, file_type);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sertification = await _sertificationRepository.GetByIdAsync(id);
            if (sertification == null) return View("Error");
            var sertificationVM = new EditSertificationViewModel
            {
                Name = sertification.Name,
                State = sertification.State
            };
            return View(sertificationVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSertificationViewModel sertificationVM, IFormFile uploadedFileSertification)
        {

            var sertification = new Sertification
            {
                Id = id,
                Name = sertificationVM.Name,
                State = sertificationVM.State
            };
            if (uploadedFileSertification != null)
            {
                // путь к папке Files
                string path = "C:/inetpub/sites/app.option/wwwroot/Files/Sertification/" + Convert.ToString(sertification.Id + "Sertification.pdf");
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFileSertification.CopyToAsync(fileStream);
                }
                AssemblyFiles file = new AssemblyFiles { Name = Convert.ToString(sertification.Id + "Sertification"), Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }
            _sertificationRepository.Update(sertification);
            return RedirectToAction("Index");
        }
    }

}
