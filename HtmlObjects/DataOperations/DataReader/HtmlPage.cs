using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjects.DataOperations.DataReader
{
    public class HtmlPage
    {
        public string Document { get; set; }

        public override string ToString()
        {
            return Document;
        }
    }
}
