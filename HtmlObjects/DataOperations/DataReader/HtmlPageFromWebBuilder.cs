using System;
using System.IO;
using System.Net;
using System.Text;

namespace HtmlObjects.DataOperations.DataReader
{
    public class HtmlPageFromWebBuilder:HtmlPageBuilder
    {

        public HtmlPageFromWebBuilder()
        {
            htmlPage = new HtmlPage();
        }

        /// <summary>
        /// Verile source path ile html içeriğini okur ve HtmlPage.Document objesine atar
        /// </summary>
        /// <param name="sourcePath"></param>
        public override void ReadHtml(string sourcePath)
        {
            HttpWebResponse response = null;
            StreamReader reader = null;
            string responseFromServer = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(sourcePath);
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;

                response = (HttpWebResponse)request.GetResponse();
               

                string Charset = response.CharacterSet;
                Encoding encoding = Encoding.GetEncoding(Charset);

                if (encoding.Equals(Encoding.UTF8))
                {

                    using (StreamReader sr =
                                        new StreamReader(response.GetResponseStream()))
                    {

                        responseFromServer = sr.ReadToEnd();
                        
                        // Close and clean up the StreamReader

                        sr.Close();
                    }

                }
                else
                {
                    // read response

                    using (StreamReader sr =
                           new StreamReader(response.GetResponseStream(), encoding))
                    {
                        responseFromServer = sr.ReadToEnd();

                        // Close and clean up the StreamReader

                        sr.Close();
                    }

                    // Check real charset meta-tag in HTML
                    int CharsetStart = responseFromServer.IndexOf("charset=");

                    if (CharsetStart > 0)
                    {
                        CharsetStart += 8;
                        int CharsetEnd = responseFromServer.IndexOfAny(new[] { ' ', '\"', ';' }, CharsetStart);
                        string RealCharset = responseFromServer.Substring(CharsetStart, CharsetEnd - CharsetStart);
                        
                        if (RealCharset != Charset)
                        {
                            // get correct encoding

                            Encoding CorrectEncoding = Encoding.GetEncoding(RealCharset);

                            // read the web page again, but with correct encoding this time
                            //   create request

                            HttpWebRequest objRequest2 = (HttpWebRequest) HttpWebRequest.Create(sourcePath);
                            
                            //   get response

                            HttpWebResponse objResponse2;
                            objResponse2 = (HttpWebResponse)objRequest2.GetResponse();

                            //   read response

                            using (StreamReader sr =
                                   new StreamReader(objResponse2.GetResponseStream(), CorrectEncoding))
                            {
                                responseFromServer = sr.ReadToEnd();
                                // Close and clean up the StreamReader
                                sr.Close();
                            }
                        }
                    }
                }


                Console.WriteLine("{0} adresinden html kaynağı alındı", sourcePath);

            }                                                                                                                      
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();

                if (response != null) response.Close();

            }
          
            htmlPage.Document = responseFromServer;
        }

    }
}
