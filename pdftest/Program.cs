// See https://aka.ms/new-console-template for more information
using Fluid;
using PdfSharp;
using pdftest;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

Console.WriteLine("Hello, World!");
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

string cssStr = File.ReadAllText(@"templates\styles.css");
CssData css = PdfGenerator.ParseStyleSheet(cssStr);

var htmlStr = File.ReadAllText(@"templates\test.html");

List<Product> productList = [new Product { description = "the best product", name = "Product 1", price = 30 }, new Product { description = "the worst product", name = "Product 2", price = 10 }];
var container = new { products = productList, Firstname = "Bill", Lastname = "Gates" };

var options = new TemplateOptions
{
    MemberAccessStrategy = new UnsafeMemberAccessStrategy()
};
//options.MemberAccessStrategy.Register<Product>();

var parser = new FluidParser();

//var source = "Hello {{ Firstname }} {{ Lastname }} {{ \"/my/fancy/url\" | append: \".html\" }}";

if (parser.TryParse(htmlStr, out var template, out var error))
{
    var context = new TemplateContext(container, options);

    var parsedHtml = template.Render(context);
    Console.WriteLine(parsedHtml);

    var pdf = PdfGenerator.GeneratePdf(parsedHtml, PageSize.A4, cssData: css);
    const string filename = "HtmlToPdfExample.pdf";
    pdf.Save(filename);
}
else
{
    Console.WriteLine($"Error: {error}");
}