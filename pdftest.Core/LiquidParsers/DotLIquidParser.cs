using DotLiquid;

namespace pdftest.LiquidParsers
{
    public class DotLIquidParser
    {
        public string Parse(string htmlStr, object model)
        {
            var templateObject = Template.Parse(htmlStr);
            var htmlContent = templateObject.Render(Hash.FromAnonymousObject(model));

            return htmlContent;
        }
    }
}
