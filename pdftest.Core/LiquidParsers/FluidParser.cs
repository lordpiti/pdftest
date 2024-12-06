using Fluid;
using System;

namespace pdftest.LiquidParsers
{
    public class FluidParser
    {
        public string Parse(string htmlStr, object model)
        {
            var options = new TemplateOptions
            {
                MemberAccessStrategy = new UnsafeMemberAccessStrategy()
            };
            //options.MemberAccessStrategy.Register<Product>();

            var parser = new Fluid.FluidParser();
            if (parser.TryParse(htmlStr, out var template, out var error))
            {
                var context = new TemplateContext(model, options);
                var parsedHtml = template.Render(context);
                return parsedHtml;
            }
            else
            {
                throw new Exception(error);
            }
        }
    }
}
