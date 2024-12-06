using pdftest;
using pdftest.LiquidParsers;
using pdftest.PdfRenderers;

var dotLiquidParser = new DotLIquidParser();
var fluidParser = new FluidParser();
var pdfServicePdfSharp = new PdfSharpService();
var pdfServicePuppeteer = new PuppeteerService();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

string cssStr = File.ReadAllText(@"templates\styles.css");
var htmlStr = File.ReadAllText(@"templates\test.html");

List<Product> productList = [
    new Product {
        Description = "the best product",
        Name = "Product 1",
        Price = 30 
    },
    new Product {
        Description = "the worst product",
        Name = "Product 2",
        Price = 10
    }];
var model = new { products = productList, pagetitle = "Test Page" };

// DotLiquid example
var htmlContent = dotLiquidParser.Parse(htmlStr, model);
pdfServicePdfSharp.Generate(htmlContent, cssStr, "example-DotLiquid");
await pdfServicePuppeteer.Generate(htmlContent, "templates/styles.css", "example-DotLiquid");

// Fluid example
var parsedHtml = fluidParser.Parse(htmlStr, model);
pdfServicePdfSharp.Generate(parsedHtml, cssStr, "example-Fluid");
await pdfServicePuppeteer.Generate(parsedHtml, "templates/styles.css", "example-Fluid");

// Flexbox html example
string cssStrFlex = File.ReadAllText(@"templates\stylesflex.css");
var htmlStrFlex = File.ReadAllText(@"templates\testflexbox.html");
pdfServicePdfSharp.Generate(htmlStrFlex, cssStrFlex, "example-flex");
await pdfServicePuppeteer.Generate(htmlStrFlex, "templates/stylesflex.css", "example-flex");
