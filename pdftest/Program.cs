using pdftest;
using pdftest.LiquidParsers;
using pdftest.PdfRenderers;

var dotLiquidParser = new DotLIquidParser();
var fluentParser = new FluentParser();
var pdfServicePdfSharp = new PdfSharpService();
var pdfServicePuppeteer = new PuppeteerService();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

string cssStr = File.ReadAllText(@"templates\styles.css");
var htmlStr = File.ReadAllText(@"templates\test.html");

List<Product> productList = [new Product { description = "the best product", name = "Product 1", price = 30 }, new Product { description = "the worst product", name = "Product 2", price = 10 }];
var model = new { products = productList, pagetitle = "Test Page" };

var htmlContent = dotLiquidParser.Parse(htmlStr, model);
pdfServicePdfSharp.Generate(htmlContent, cssStr, "example-LIQUID");
await pdfServicePuppeteer.Generate(htmlContent, "templates/styles.css", "example-LIQUID");


var parsedHtml = fluentParser.Parse(htmlStr, model);
pdfServicePdfSharp.Generate(parsedHtml, cssStr, "example-FLUID");
await pdfServicePuppeteer.Generate(parsedHtml, "templates/styles.css", "example-FLUID");
