using Fluid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdftest.LiquidParsers
{
    internal class FluentParser
    {
        public string Parse(string htmlStr, object model)
        {
            var options = new TemplateOptions
            {
                MemberAccessStrategy = new UnsafeMemberAccessStrategy()
            };
            //options.MemberAccessStrategy.Register<Product>();

            var parser = new FluidParser();
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
