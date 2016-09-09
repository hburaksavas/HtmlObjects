using HtmlObjects.BusinessOperations.AdapterOperations;
using HtmlObjects.DataOperations.DataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjects.ServiceOperations
{
    public class GeneralService:Service
    {


        #region Fields

        private Adapter adapter;

        #endregion

        /// <summary>
        /// GeneralService örneğini direkt source vererek oluşturmak için kullanılan constructor
        /// </summary>
        /// <param name="source"></param>
        public GeneralService(string source)
        {
            adapter = new Adapter(source);
        }


        /// <summary>
        /// GeneralService örneğini builder ve sourcepath vererek, source'u nereden alınacağı söylenmiş olur
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sourcePath"></param>
        public GeneralService(HtmlPageBuilder builder, string sourcePath)
        {
            this.builder = builder;
            this.path = sourcePath;
            LoadHtml();

            adapter = new Adapter(builder.HtmlPage.Document);
        }


        /// <summary>
        /// Html taglarından kurtarılmış metni geri döner
        /// </summary>
        /// <returns></returns>
        public string GetHtmlPageInnerText()
        {
            return adapter.InnerTextAdapter();
        }


        /// <summary>
        /// Source içerisinde telefon numarası olabilecek verileri liste olarak döner
        /// </summary>
        /// <returns></returns>
        public List<string> GetPhoneList()
        {
            return adapter.PhoneListAdapter();
        }


        /// <summary>
        /// Source içerisinde fax no olabilecek verileri liste olarak döner
        /// </summary>
        /// <returns></returns>
        public List<string> GetFaxList()
        {
            return adapter.FaxListAdapter();
        }


        /// <summary>
        /// Source içerisinde mail olabilecek verileri liste olarak döner
        /// </summary>
        /// <returns></returns>
        public List<string> GetMailList()
        {
            return adapter.MailListAdapter();
        }


        /// <summary>
        /// Source içerisinde url olabilecek verileri liste olarak döner
        /// </summary>
        /// <returns></returns>
        public List<string> GetWebUrlList()
        {
            return adapter.WebAdresListAdapter();
        }

    }
}
