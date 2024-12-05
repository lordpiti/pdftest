using PuppeteerSharp;

namespace pdftest.PdfRenderers
{
    internal class PuppeteerService
    {
        public async Task Generate(string htmlContent, string cssFilePath, string outputFilename)
        {
            await new BrowserFetcher().DownloadAsync();
            using var browser2 = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            using var page2 = await browser2.NewPageAsync();
            await page2.SetContentAsync(htmlContent);
            await page2.AddStyleTagAsync(new AddTagOptions { Path = cssFilePath }); // also possible to inject the css string
            await page2.PdfAsync($"{outputFilename}-puppeteersharp.pdf", new PdfOptions { PrintBackground = true });
        }
    }
}
