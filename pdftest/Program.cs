using DotLiquid;
using Fluid;
using pdftest;
using pdftest.PdfRenderers;

var pdfServicePdfSharp = new PdfSharpService();
var pdfServicePuppeteer = new PuppeteerService();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

string cssStr = File.ReadAllText(@"templates\styles.css");
var htmlStr = File.ReadAllText(@"templates\test.html");

List<Product> productList = [new Product { description = "the best product", name = "Product 1", price = 30 }, new Product { description = "the worst product", name = "Product 2", price = 10 }];
var container = new { products = productList, pagetitle = "Test Page" };

var templateObject = Template.Parse(htmlStr);
var htmlContent = templateObject.Render(Hash.FromAnonymousObject(container));

pdfServicePdfSharp.Generate(htmlContent, cssStr, "example-LIQUID");
await pdfServicePuppeteer.Generate(htmlContent, "templates/styles.css", "example-LIQUID");


#region Fluid Library

var options = new TemplateOptions
{
    MemberAccessStrategy = new UnsafeMemberAccessStrategy()
};
//options.MemberAccessStrategy.Register<Product>();

var parser = new FluidParser();

if (parser.TryParse(htmlStr, out var template, out var error))
{
    var context = new TemplateContext(container, options);
    var parsedHtml = template.Render(context);

    pdfServicePdfSharp.Generate(parsedHtml, cssStr, "FLUID-example");
    await pdfServicePuppeteer.Generate(parsedHtml, "templates/styles.css", "FLUID-example");
}
else
{
    Console.WriteLine($"Error: {error}");
}

#endregion