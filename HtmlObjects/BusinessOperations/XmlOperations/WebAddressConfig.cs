using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HtmlObjects.BusinessOperations.XmlOperations
{
    public class WebAddressConfig
    {

        private List<OsbWebAddress> listOSB;
        public WebAddressConfig()
        {
            listOSB = new List<OsbWebAddress>();
        }

        public void addAll(List<OsbWebAddress> listOSB)
        {
            this.listOSB = listOSB;
        }

        public void add(String url,String htmlTag)
        {
            listOSB.Add(new OsbWebAddress() { Url = url, HtmlTag = htmlTag });
        }

        /// <summary>
        /// Veriler eklendikten sonra, xml dosyasına yazma işleminin tamamlanması için çağırılması gerekir.
        /// </summary>
        public void commit()
        {
            
            DataOperations.DataWriter.XmlFileWriter xmlWriter = new DataOperations.DataWriter.XmlFileWriter("OsbWebAddress");
            xmlWriter.Write(listOSB);

        }

        public List<OsbWebAddress> read()
        {
            HtmlObjects.DataOperations.DataReader.XmlReader read = new HtmlObjects.DataOperations.DataReader.XmlReader();
            List<OsbWebAddress> list = read.ReadWebAddressXml("OsbWebAddress");
            return list;
        }

    }
}
