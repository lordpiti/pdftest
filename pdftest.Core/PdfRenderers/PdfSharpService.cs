using PdfSharp;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace pdftest.PdfRenderers
{
    public class PdfSharpService
    {
        public void Generate(string htmlContent, string cssContent, string outputFilename)
        {
            CssData css2 = PdfGenerator.ParseStyleSheet(cssContent);
            var pdf2 = PdfGenerator.GeneratePdf(htmlContent, PageSize.A4, cssData: css2);
            pdf2.Save($"{outputFilename}-pdfsharp.pdf");
        }
    }
}
