using pdftest;
using pdftest.Core.PdfRenderers;
using pdftest.LiquidParsers;
using pdftest.PdfRenderers;

var dotLiquidParser = new DotLIquidParser();
var fluidParser = new FluidParser();
var pdfServicePdfSharp = new PdfSharpService();
var pdfServicePuppeteer = new PuppeteerService();
var pdfServiceSelectPdf = new SelectPdfService();
var pdfServiceDinkToPdf = new DinkToPdfService();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

string cssStr = File.ReadAllText(@"templates\styles.css");
var htmlStr = File.ReadAllText(@"templates\testwithcss.html");
var htmlHeader = File.ReadAllText(@"templates\header.html");

// Create a list to hold the products
List<Product> productList = [];

// Random number generator for prices
var random = new Random();

// Generate 50 products
for (int i = 1; i <= 50; i++)
{
    productList.Add(new Product
    {
        Description = $"Product description {i}",
        Name = $"Product {i}",
        Price = random.Next(1, 101) // Random price between 1 and 100
    });
}
var model = new { products = productList, pagetitle = "Test Page" };

// DotLiquid example
var htmlContent = dotLiquidParser.Parse(htmlStr, model);
pdfServicePdfSharp.Generate(htmlContent, cssStr, "example-DotLiquid");
//await pdfServicePuppeteer.Generate(htmlContent, "templates/styles.css", "example-DotLiquid");
pdfServiceSelectPdf.Generate(htmlContent, htmlHeader);
pdfServiceDinkToPdf.Generate(htmlContent);

//// Fluid example
//var parsedHtml = fluidParser.Parse(htmlStr, model);
//pdfServicePdfSharp.Generate(parsedHtml, cssStr, "example-Fluid");
//await pdfServicePuppeteer.Generate(parsedHtml, "templates/styles.css", "example-Fluid");

//// Flexbox html example
//string cssStrFlex = File.ReadAllText(@"templates\stylesflex.css");
//var htmlStrFlex = File.ReadAllText(@"templates\testflexbox.html");
//pdfServicePdfSharp.Generate(htmlStrFlex, cssStrFlex, "example-flex");
//await pdfServicePuppeteer.Generate(htmlStrFlex, "templates/stylesflex.css", "example-flex");
