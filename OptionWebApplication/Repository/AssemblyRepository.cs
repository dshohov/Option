using Microsoft.EntityFrameworkCore;
using OptionWebApplication.Data;
using OptionWebApplication.Interfaces;
using OptionWebApplication.Models;
using System.Linq;
using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace OptionWebApplication.Repository
{
    public class AssemblyRepository : IAssemblyRepository
    {
        private readonly ApplicationDbContext _context;
        public AssemblyRepository(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
        }

        public bool Add(Assembly assembly)  
        {
            _context.Add(assembly);
            return Save();
        }

        public bool Delete(Assembly assembly)
        {
            _context.Remove(assembly);
            return Save();
        }

        public async Task<IEnumerable<Assembly>> GetAll()
        {
            return await _context.Assemblies.ToListAsync();
        }

        public async Task<Assembly> GetByIdAsync(int id)
        {
            return await _context.Assemblies.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Assembly> GetAssemblyBySerialNumber(string serialnumber)
        {
            
            return await _context.Assemblies.FirstOrDefaultAsync(i => i.SerialNumber == serialnumber);   
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;        
        }

        public bool Update(Assembly assembly)
        {
            _context.Update(assembly);
            return Save();
        }

        public void CreatePdf (Assembly assembly,bool type)
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

            Paragraph paragraphEmpty4 = new Paragraph();
            Text textEmpty4 = new Text("\n");
            paragraphEmpty4.Add(textEmpty4);
            section.Add(paragraphEmpty4);

            Paragraph paragraphSerial3 = new Paragraph();
            Text textSerial3 = new Text("Товар: " + assembly.Component);
            paragraphSerial3.Format.Font.Size = 11;
            paragraphSerial3.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial3.Add(textSerial3);
            section.Add(paragraphSerial3);

            Paragraph paragraphSerial4 = new Paragraph();
            Text textSerial4 = new Text("Замена: " + assembly.ChangeComponents);
            paragraphSerial4.Format.Font.Size = 11;
            paragraphSerial4.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial4.Add(textSerial4);
            section.Add(paragraphSerial4);

            Paragraph paragraphEmpty5 = new Paragraph();
            Text textEmpty5 = new Text("\n");
            paragraphEmpty5.Add(textEmpty5);
            section.Add(paragraphEmpty5);

            Paragraph paragraphSerial5 = new Paragraph();
            Text textSerial5 = new Text("Внеплановые доработки: " + assembly.OtherWork);
            paragraphSerial5.Format.Font.Size = 11;
            paragraphSerial5.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial5.Add(textSerial5);
            section.Add(paragraphSerial5);

            Paragraph paragraphEmpty6 = new Paragraph();
            Text textEmpty6 = new Text("\n");
            paragraphEmpty6.Add(textEmpty6);
            section.Add(paragraphEmpty6);

            Paragraph paragraphSerial6 = new Paragraph();
            Text textSerial6 = new Text("Ответственные лица");
            paragraphSerial6.Format.Font.Size = 11;
            paragraphSerial6.Format.Alignment = ParagraphAlignment.Center;
            paragraphSerial6.Add(textSerial6);
            section.Add(paragraphSerial6);

            Paragraph paragraphEmpty7 = new Paragraph();
            Text textEmpty7 = new Text("\n");
            paragraphEmpty7.Add(textEmpty7);
            section.Add(paragraphEmpty7);

            section.Add(createTable(assembly));

            Paragraph paragraphEmpty8 = new Paragraph();
            Text textEmpty8 = new Text("\n");
            paragraphEmpty8.Add(textEmpty8);
            section.Add(paragraphEmpty8);

            Paragraph paragraphSerial7 = new Paragraph();
            Text textSerial7 = new Text("Проверил:");
            paragraphSerial7.Format.Font.Size = 11;
            paragraphSerial7.Format.Alignment = ParagraphAlignment.Left;
            paragraphSerial7.Add(textSerial7);
            section.Add(paragraphSerial7);

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
            if (type == false)
            {
                pdfRenderer.PdfDocument.Save("PdfSerialNumber.pdf");
            }
            else
            {
                pdfRenderer.PdfDocument.Save("PdfSerialNumberParty.pdf");
            }
// сохраняем
        }
        public MigraDoc.DocumentObjectModel.Tables.Table createTable(Assembly assembly)
        {
            MigraDoc.DocumentObjectModel.Tables.Table table = new MigraDoc.DocumentObjectModel.Tables.Table();
            //table = page.AddTable();
            table.Style = "Table";
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            //create column
            MigraDoc.DocumentObjectModel.Tables.Column column1 = new MigraDoc.DocumentObjectModel.Tables.Column();
            column1 = table.AddColumn("0,5cm");
            column1.Format.Alignment = ParagraphAlignment.Left;
            //MigraDoc.DocumentObjectModel.Tables.Row tableRow = table.AddRow();
            MigraDoc.DocumentObjectModel.Tables.Column column2 = new MigraDoc.DocumentObjectModel.Tables.Column();
            column2 = table.AddColumn("10cm");
            column2.Format.Alignment = ParagraphAlignment.Left;
            MigraDoc.DocumentObjectModel.Tables.Column column3 = new MigraDoc.DocumentObjectModel.Tables.Column();
            column3 = table.AddColumn("5cm");
            column3.Format.Alignment = ParagraphAlignment.Left;

            //create rows from list of values
            MigraDoc.DocumentObjectModel.Tables.Row tableRow = table.AddRow();
            MigraDoc.DocumentObjectModel.Tables.Row tableRow1 = table.AddRow();
            MigraDoc.DocumentObjectModel.Tables.Row tableRow2 = table.AddRow();
            MigraDoc.DocumentObjectModel.Tables.Row tableRow3 = table.AddRow();
            MigraDoc.DocumentObjectModel.Tables.Row tableRow4 = table.AddRow();
            MigraDoc.DocumentObjectModel.Tables.Row tableRow5 = table.AddRow();
            //add the value
            tableRow.Cells[0].AddParagraph("№");
            tableRow.Cells[1].AddParagraph("Этап сборки (полная сборка, установка мат.платы, КМ, упаковка и т.д.)");
            tableRow.Cells[2].AddParagraph("Выполнил(ФИО, подпись)");
            tableRow1.Cells[0].AddParagraph("1");
            tableRow1.Cells[1].AddParagraph(assembly.Step1);
            tableRow1.Cells[2].AddParagraph(Convert.ToString(assembly.People1));
            tableRow2.Cells[0].AddParagraph("2");
            tableRow2.Cells[1].AddParagraph(NullElementInPdf(assembly.Step2));
            tableRow2.Cells[2].AddParagraph(NullElementInPdf(Convert.ToString(assembly.People2)));
            tableRow3.Cells[0].AddParagraph("3");
            tableRow3.Cells[1].AddParagraph(NullElementInPdf(assembly.Step3));
            tableRow3.Cells[2].AddParagraph(NullElementInPdf(Convert.ToString(assembly.People3)));
            tableRow4.Cells[0].AddParagraph("4");
            tableRow4.Cells[1].AddParagraph(NullElementInPdf(assembly.Step4));
            tableRow4.Cells[2].AddParagraph(NullElementInPdf(Convert.ToString(assembly.People4)));
            tableRow5.Cells[0].AddParagraph("5");
            tableRow5.Cells[1].AddParagraph(NullElementInPdf(assembly.Step5));
            tableRow5.Cells[2].AddParagraph(NullElementInPdf(Convert.ToString(assembly.People5)));

            return table;
        }

        public string NullElementInPdf(string element)
        {
            if (element == null)
                element = "-";
            if (element == "")
                element = "-";
            return element;
        }

      
      
    }
}
