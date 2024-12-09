using DinkToPdf;
using System;

namespace pdftest.Core.PdfRenderers
{
    public class DinkToPdfService
    {
        public void Generate(string htmlContent)
        {
            // Initialize the converter
            var converter = new BasicConverter(new PdfTools());

            // Set up the object that holds the options for PDF generation
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Out = @"sample-DinkToPdf.pdf"  // Output path for the generated PDF
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        PagesCount = true, // Enable page numbers
                    },
                }
            };

            // Generate the PDF and save it to the specified output path
            converter.Convert(doc);
            Console.WriteLine("PDF has been generated successfully!");
        }
    }
}
