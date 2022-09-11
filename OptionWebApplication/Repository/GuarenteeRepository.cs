using Microsoft.EntityFrameworkCore;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using PdfSharp.Pdf;

namespace OptionWebApplication.Repository
{
    public class GuarenteeRepository : IGuarenteeRepository
    {
        private readonly ApplicationDbContext _context;
        public GuarenteeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Guarentee guarentee)
        {
            _context.Add(guarentee);
            return Save();
        }

        public bool Delete(Guarentee guarentee)
        {
            _context.Remove(guarentee);
            return Save();
        }

        public async Task<IEnumerable<Guarentee>> GetAll()
        {
            return await _context.Guarentes.ToListAsync();
        }

        public async Task<Guarentee> GetByIdAsync(int id)
        {
            return await _context.Guarentes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Guarentee> GetGuarenteeBySerialNumber(string serialnumber)
        {
            return await _context.Guarentes.FirstOrDefaultAsync(i => i.SerialNumber == serialnumber);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Guarentee guarentee)
        {
            _context.Update(guarentee);
            return Save();
        }
        public void CreatePdf(Guarentee guarentee)
        {
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
            Text textId = new Text("Сопроводительный лист сборки № " + guarentee.Id + " от " + date1.ToString("d"));
            paragraphId.Add(textId);
            section.Add(paragraphId);
            Paragraph paragraphCompany = new Paragraph();
            Text textCompany = new Text("Компания: " + guarentee.Company + "\n");
            paragraphCompany.Format.Font.Size = 26;
            paragraphCompany.Format.Alignment = ParagraphAlignment.Center;
            paragraphCompany.Add(textCompany);
            section.Add(paragraphCompany);
            Paragraph paragraphEmpty = new Paragraph();
            Text textEmpty = new Text("\n");
            paragraphEmpty.Add(textEmpty);
            section.Add(paragraphEmpty);
            Paragraph paragraphSerial = new Paragraph();
            Text textSerial = new Text("\nS/N устройств(а): " + guarentee.SerialNumber);
            paragraphSerial.Format.Font.Size = 11;
            paragraphSerial.Format.Alignment = ParagraphAlignment.Center;
            paragraphSerial.Add(textSerial);
            section.Add(paragraphSerial);
            Paragraph paragraphEmpty2 = new Paragraph();
            Text textEmpty2 = new Text("Тип устройств(а) " + guarentee.TypeDevice);
            paragraphEmpty2.Format.Font.Size = 11;
            paragraphEmpty2.Format.Alignment = ParagraphAlignment.Center;
            paragraphEmpty2.Add(textEmpty2);
            section.Add(paragraphEmpty2);
            Paragraph paragraphEmpty3 = new Paragraph();
            Text textEmpty3 = new Text("Дата получения: " + guarentee.DateIn + "Дата отправления: " + guarentee.DateOut);
            paragraphEmpty3.Format.Font.Size = 11;
            paragraphEmpty3.Format.Alignment = ParagraphAlignment.Center;
            paragraphEmpty3.Add(textEmpty3);
            section.Add(paragraphEmpty3);

            Paragraph paragraphSerial2 = new Paragraph();
            Text textSerial2 = new Text("Рекламация: " + guarentee.Details);
            paragraphSerial2.Format.Font.Size = 11;
            paragraphSerial2.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial2.Add(textSerial2);
            section.Add(paragraphSerial2);

            Paragraph paragraphEmpty4 = new Paragraph();
            Text textEmpty4 = new Text("\n");
            paragraphEmpty4.Add(textEmpty4);
            section.Add(paragraphEmpty4);

            Paragraph paragraphSerial3 = new Paragraph();
            Text textSerial3 = new Text("Выявленные неисправности:" + guarentee.FaultDetection);
            paragraphSerial3.Format.Font.Size = 11;
            paragraphSerial3.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial3.Add(textSerial3);
            section.Add(paragraphSerial3);

            Paragraph paragraphSerial4 = new Paragraph();
            Text textSerial4 = new Text("\n");
            paragraphSerial4.Format.Font.Size = 11;
            paragraphSerial4.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial4.Add(textSerial4);
            section.Add(paragraphSerial4);

            Paragraph paragraphEmpty5 = new Paragraph();
            Text textEmpty5 = new Text("Заключение: " + guarentee.Conclusion);
            paragraphEmpty5.Format.Font.Size = 11;
            paragraphEmpty5.Format.Alignment = ParagraphAlignment.Left;
            paragraphEmpty5.Add(textEmpty5);
            section.Add(paragraphEmpty5);

            Paragraph paragraphEmpty6 = new Paragraph();
            Text textEmpty6 = new Text("\n");
            paragraphEmpty6.Add(textEmpty6);
            section.Add(paragraphEmpty6);

            Paragraph paragraphSerial5 = new Paragraph();
            Text textSerial5 = new Text("Диагностику осуществил (ФИО, подпись):" + guarentee.DiagnosticPeople);
            paragraphSerial5.Format.Font.Size = 11;
            paragraphSerial5.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial5.Add(textSerial5);
            section.Add(paragraphSerial5);

            Paragraph paragraphEmpty7 = new Paragraph();
            Text textEmpty7 = new Text("\n");
            paragraphEmpty7.Add(textEmpty7);
            section.Add(paragraphEmpty7);

            Paragraph paragraphSerial6 = new Paragraph();
            Text textSerial6 = new Text("Выполненные работы: " + guarentee.ComplectedWork);
            paragraphSerial6.Format.Font.Size = 11;
            paragraphSerial6.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial6.Add(textSerial6);
            section.Add(paragraphSerial6);

            Paragraph paragraphEmpty8 = new Paragraph();
            Text textEmpty8 = new Text("\n");
            paragraphEmpty8.Add(textEmpty8);
            section.Add(paragraphEmpty8);

            Paragraph paragraphSerial7 = new Paragraph();
            Text textSerial7 = new Text("Ремонт осуществил (ФИО, подпись): " + guarentee.RepairPeople);
            paragraphSerial7.Format.Font.Size = 11;
            paragraphSerial7.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial7.Add(textSerial7);
            section.Add(paragraphSerial7);

            Paragraph paragraphEmpty9 = new Paragraph();
            Text textEmpty9 = new Text("\n");
            paragraphEmpty9.Add(textEmpty9);
            section.Add(paragraphEmpty9);

            Paragraph paragraphSerial10 = new Paragraph();
            Text textSerial10 = new Text("Проверил: ");
            paragraphSerial10.Format.Font.Size = 11;
            paragraphSerial10.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial10.Add(textSerial10);
            section.Add(paragraphSerial10);

            Paragraph paragraphSerial8 = new Paragraph();
            Text textSerial8 = new Text("Инженер ОТК ");
            paragraphSerial8.Format.Font.Size = 11;
            paragraphSerial8.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial8.Add(textSerial8);
            section.Add(paragraphSerial8);

            Paragraph paragraphSerial9 = new Paragraph();
            Text textSerial9 = new Text("____________");
            paragraphSerial9.Format.Font.Size = 11;
            paragraphSerial9.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial9.Add(textSerial9);
            section.Add(paragraphSerial9);
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save("PdfGuarentee.pdf");
            // сохраняем
        }
    }
}
