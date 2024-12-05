using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdftest.LiquidParsers
{
    internal class DotLIquidParser
    {
        public string Parse(string htmlStr, object model)
        {
            var templateObject = Template.Parse(htmlStr);
            var htmlContent = templateObject.Render(Hash.FromAnonymousObject(model));

            return htmlContent;
        }
    }
}
