using HtmlObjects.BusinessOperations;
using HtmlObjects.DataOperations.DataReader;
using HtmlObjects.DataOperations.DataWriter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlObjects.ServiceOperations
{
    public abstract class Service
    {
        public HtmlPageBuilder builder { get; set; }

        public String path { get; set; }

        /// <summary>
        /// FirmService ve GeneralService tarafından kullanılarak,Adapter sınıflarına gönderilen Html Dökümanı
        /// </summary>
        public string HtmlDocument { get; set; }

        /// <summary>
        /// Builder ve path nesnelerini sağladıktan sonra çağrılır ve HtmlDocument nesnesini aktif hale getirir.
        /// </summary>
        public void LoadHtml()
        {
            try
            {
                //director read metodu içerisinde gönderilen builder'a göre html sayfasının nasıl build edileceği belirleniyor
                HtmlDocumentDirector.Read(builder, path);

                // build edilen html dökümanını,htmldocument'e ata
                HtmlDocument = builder.HtmlPage.Document;

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

        }

        public void PrintExcel(List<string> dataList, string[] titles, string fileName)
        {
            if (dataList != null)
            {
                if (dataList.Count > 0)
                {
                    //verilen başlık sayısına göre excel satır sayısı hesapla
                    int rowCount = dataList.Count() / titles.Length;

                    //Satır sayısı ile başlıkların sayısını çarpımı,doğrulama için
                    int Size = rowCount * titles.Length;

                    //Verilen liste ve dizi elemanları excel sayfasına düzgün yerleşiyor mu diye kontrol et, Örneğin listede 21 eleman varken,titles 4 elemanlı durumu hatalı
                    if (Size.Equals(dataList.Count))
                    {
                        ExcelPrinter printer = new ExcelPrinter();

                        //Verilen nesneler örtüşüyorsa, excele yazdıracak metodu çağır
                        printer.Print(dataList, titles, fileName);
                    }
                    else
                    {
                       PrintConsole._INFO("Eksik başlık bilgisi veya liste eleman sayısı istenen sayıda değil.");
                    }
                }
            }
        }

    }
}
