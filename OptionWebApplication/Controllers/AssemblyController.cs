﻿using Microsoft.AspNetCore.Mvc;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using OptionWebApplication.ViewModels;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using MigraDoc;
using PdfSharp;

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

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
        public async Task<IActionResult> Print(int id)
        {
            Assembly assembly = await _assemblyRepository.GetByIdAsync(id);

            return View(assembly);
        }
        public async Task<IActionResult> Pdf(int id)
        {
            var assembly = await _assemblyRepository.GetByIdAsync(id);
            DateTime date1 = DateTime.Now;
            
            Document document = new Document();
            Section section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;//стандартный размер страницы
            section.PageSetup.Orientation = Orientation.Portrait;//ориентация
            section.PageSetup.BottomMargin = 10;//нижний отступ
            section.PageSetup.TopMargin = 10;//верхний отступ
            Paragraph paragraphId = new Paragraph();
            paragraphId.Format.Font.Size = 14;
            paragraphId.Format.Font.Bold = true;
            paragraphId.Format.Alignment = ParagraphAlignment.Center;
            Text textId = new Text("Сопроводительный лист сборки № " + assembly.Id + " от " + date1.ToString("d"));
            paragraphId.Add(textId);
            section.Add(paragraphId);
            Paragraph paragraphCompany = new Paragraph();
            Text textCompany = new Text("Компания: " + assembly.Company + "\n");
            paragraphCompany.Format.Font.Size = 26;
            paragraphCompany.Format.Alignment = ParagraphAlignment.Center;
            paragraphCompany.Add(textCompany);
            section.Add(paragraphCompany);
            Paragraph paragraphEmpty = new Paragraph();
            Text textEmpty = new Text("\n");
            paragraphEmpty.Add(textEmpty);
            section.Add(paragraphEmpty);
            Paragraph paragraphSerial = new Paragraph();
            Text textSerial = new Text("\nS/N устройств(а): " + assembly.SerialNumber);
            paragraphSerial.Format.Font.Size = 11;
            paragraphSerial.Format.Alignment = ParagraphAlignment.Center;
            paragraphSerial.Add(textSerial);
            section.Add(paragraphSerial);
            Paragraph paragraphEmpty2 = new Paragraph();
            Text textEmpty2 = new Text("\n");
            paragraphEmpty2.Add(textEmpty2);
            section.Add(paragraphEmpty2);
            Paragraph paragraphEmpty3 = new Paragraph();
            Text textEmpty3 = new Text("\n");
            paragraphEmpty3.Add(textEmpty3);
            section.Add(paragraphEmpty3);

            Paragraph paragraphSerial2 = new Paragraph();
            Text textSerial2 = new Text("Замена комплектующих");
            paragraphSerial2.Format.Font.Size = 11;
            paragraphSerial2.Format.Alignment = ParagraphAlignment.Center;
            paragraphSerial2.Add(textSerial2);
            section.Add(paragraphSerial2);

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save("FirstPDFDocument.pdf");// сохраняем
            return View();
        }
    }
}
 