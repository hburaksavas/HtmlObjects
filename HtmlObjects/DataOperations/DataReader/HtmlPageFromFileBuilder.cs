using System;
using System.IO;

namespace HtmlObjects.DataOperations.DataReader
{
    public class HtmlPageFromFileBuilder : HtmlPageBuilder
    {
        public HtmlPageFromFileBuilder()
        {
            htmlPage = new HtmlPage();
        }

        /// <summary>
        /// Verilen source path ile html içeriğini okur ve HtmlPage.Document nesnesine atar..
        /// </summary>
        /// <param name="sourcePath"></param>
        public override void ReadHtml(string sourcePath)
        {
            StreamReader streamReader = null;
            try
            {
                string fileText = "";
                string txt;
                streamReader = File.OpenText(sourcePath);
                txt = streamReader.ReadLine();
                fileText = txt;
                while (txt != null)
                {
                   
                    txt = streamReader.ReadLine();
                    fileText += txt;
                }

                htmlPage.Document = fileText;
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
        }
    }
}
