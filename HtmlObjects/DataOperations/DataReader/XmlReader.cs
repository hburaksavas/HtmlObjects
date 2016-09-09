using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace HtmlObjects.DataOperations.DataReader
{
    public class XmlReader
    {
        public List<OsbWebAddress> ReadWebAddressXml(String FileName){
            FileName = FileName + ".xml";
            List<OsbWebAddress> resultList = new List<OsbWebAddress>();
            try {
                if ( !File.Exists(FileName) ) {

                    throw new Exception("Belirtilen Dosya Bulunamadı");

                }
                else {
                    XmlTextReader reader = new XmlTextReader(FileName);
                    String url = String.Empty;
                    String tag = String.Empty;
                    while ( reader.Read() ) {
                        
                        if ( reader.NodeType == XmlNodeType.Element && reader.Name == "url" ) {
                            //NodeType ile şuanda okunan elemanın tipi kontrol edilir.
                            reader.Read();                       

                             url = reader.Value;
                        }
                        else if ( reader.NodeType == XmlNodeType.Element && reader.Name == "htmlTag" ) {
                            reader.Read();
                            tag = reader.Value;
                           
                        }
                        //Eğer url ve tag verileri okunduysa listeye ekleniyor
                        if( !String.IsNullOrEmpty(url) && !String.IsNullOrEmpty(tag) ) {
                            resultList.Add(new OsbWebAddress() {
                                Url = url,
                                HtmlTag = tag
                            });
                            url = String.Empty;
                            tag = String.Empty;
                        }

                    }

                    reader.Close();

                }
            
            }catch(Exception e ) {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }



            return resultList;
        }

    }
}
