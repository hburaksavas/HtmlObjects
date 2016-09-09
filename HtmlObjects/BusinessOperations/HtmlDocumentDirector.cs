using HtmlObjects.DataOperations.DataReader;
using System;

namespace HtmlObjects.BusinessOperations
{
    public class HtmlDocumentDirector
    {
        /// <summary>
        /// DataLayerAccessMethod, file veya websitesi pathine göre builder seçip, builder.HtmlPage.ToString() veya Document objesi ile içeriği elde edebilirsiniz.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sourcePath"></param>
        public static void Read(HtmlPageBuilder builder, String sourcePath)
        {
            try
            {
                builder.ReadHtml(sourcePath); // ilgili builder ile verilen path'den html sayfası okunuyor
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
        }

    }

}
