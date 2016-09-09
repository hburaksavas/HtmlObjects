using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlObjects.DataOperations.DataWriter
{
    public sealed class XmlFileWriter
    {
        private string FileName;

        public XmlFileWriter(String FileName)
        {
            this.FileName = FileName+".xml";
        }

        public void Write(List<OsbWebAddress> listOSB)
        {
            try {
                if ( isExists() ) {
                    XDocument document = XDocument.Load(FileName);

                    foreach ( var item in listOSB ) {
                        document.Element("WebAddresses").Add(new XElement("webaddress",
                                                                 new XElement("url", item.Url),
                                                                 new XElement("htmlTag", item.HtmlTag)
                                                             ));


                    }
                    document.Save(FileName);
                }
               
            }
            catch ( Exception e ) {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
        }

        private Boolean isExists()
        {
            System.Xml.XmlWriter writer = null;
            try
            {

                if ( !File.Exists(FileName)){

                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
                    settings.Indent = true;

                    writer = System.Xml.XmlWriter.Create(FileName, settings);
                    writer.WriteStartDocument();

                    writer.WriteStartElement("WebAddresses");
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }              

            }
            catch(Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return false;
            }
            finally
            {
                if(writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
            return true;
        }

    }
}
