using Fluid;
using PdfSharp;
using pdftest;
using PuppeteerSharp;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

Console.WriteLine("Hello, World!");
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

string cssStr = File.ReadAllText(@"templates\styles.css");
var htmlStr = File.ReadAllText(@"templates\test.html");

List<Product> productList = [new Product { description = "the best product", name = "Product 1", price = 30 }, new Product { description = "the worst product", name = "Product 2", price = 10 }];
var container = new { products = productList, pagetitle = "Test Page" };

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
    Console.WriteLine(parsedHtml);

    #region PdfSharp

    CssData css = PdfGenerator.ParseStyleSheet(cssStr);
    var pdf = PdfGenerator.GeneratePdf(parsedHtml, PageSize.A4, cssData: css);
    const string filename = "pdfsharp-example.pdf";
    pdf.Save(filename);

    #endregion

    #region PuppeteerSharp

    await new BrowserFetcher().DownloadAsync();
    using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
    using var page = await browser.NewPageAsync();
    await page.SetContentAsync(parsedHtml);
    await page.AddStyleTagAsync(new AddTagOptions { Path = "templates/styles.css" }); // also possible to inject the css string
    await page.PdfAsync("pupetteersharp-example.pdf");

    #endregion
}
else
{
    Console.WriteLine($"Error: {error}");
}