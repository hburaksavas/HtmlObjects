using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjects.DataOperations.DataReader
{
    public abstract class HtmlPageBuilder
    {
        protected HtmlPage htmlPage;


        public HtmlPage HtmlPage
        {
            get { return htmlPage; }
        }



        public abstract void ReadHtml(string sourcePath);
    }
}
