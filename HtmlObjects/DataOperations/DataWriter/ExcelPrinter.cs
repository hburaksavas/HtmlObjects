using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;

namespace HtmlObjects.DataOperations.DataWriter
{
    public class ExcelPrinter
    {
        private Application ExcelUygulama;
        private Workbook ExcelProje;
        private Worksheet ExcelSayfa;
        private Range ExcelRange;
        private object Missing = System.Reflection.Missing.Value;

        private bool OpenExelWork()
        {
            try
            {
                //Yeni bir Excel Uygulaması Oluşturalım.
                ExcelUygulama = new Application();
                //Yeni bir Excel Projesi oluşturalım.  
                ExcelProje = ExcelUygulama.Workbooks.Add(Missing);
                //Yeni bir Sayfa oluşturalım. (Worksheet1, Worksheet2 dediğimiz olay...) 
                ExcelSayfa = (Worksheet)ExcelProje.Worksheets.get_Item(1);
                //Excelde kullanacağımız aralığı seçelim. (Hemen üstte ExcelSayfada Worksheet1'i seçtiğimizi görmelisiniz.)
                ExcelRange = ExcelSayfa.UsedRange;
                //Kullanacağımız Sayfayı (Worksheet1'i) ExcelSayfa değişkenine atayalım .
                ExcelSayfa = (Worksheet)ExcelUygulama.ActiveSheet;
                //Excel uygulamamızı gizleyelim.
                ExcelUygulama.Visible = false;
                //Uygulamamıza veri girmeden önce verilen uyarıyı gizleyelim. (bunları fazla sallamayın siz kodları kopyalayıp yapıştırın yeter.)
                ExcelUygulama.AlertBeforeOverwriting = false;
                return true;
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return false;
            }


        }

        public void Print(List<string> sourceList, String[] titles, String fileName)
        {
            try
            {
                Range title;
                if (OpenExelWork())
                {
                    int columnCount = titles.Length;
                    int sourceListCount = sourceList.Count;

                    if (columnCount > 0 && sourceListCount > 0)
                    {
                        if (columnCount > 0)
                        {

                            for (int i = 0; i < columnCount; i++)
                            {
                                title = (Range)ExcelSayfa.Cells[1, i + 1];
                                title.Value2 = titles[i];
                            }

                            int rowCounter = sourceListCount / columnCount;

                            int itemCounter = 0;
                            for (int i = 1; i <= rowCounter; i++) //rows
                            {
                                for (int j = 1; j <= columnCount; j++) //columns
                                {
                                    Range range = (Range)ExcelSayfa.Cells[i + 1, j]; //i=1 j=1 
                                    if (itemCounter < sourceListCount)
                                        range.Value2 = sourceList[itemCounter];
                                    itemCounter++;
                                } //1. döngü sonu itemCounter = 5
                            }

                        }

                    }
                    else
                    {

                        for (int i = 0; i < sourceListCount; i++)
                        {
                            title = (Range)ExcelSayfa.Cells[i + 1, 1];
                        }

                    }


                }


                fileName = fileName + ".xlsx";

                if (!File.Exists(fileName))
                {
                    FileStream fs = File.Create(@fileName);
                    fs.Close();
                }

                ExcelProje.SaveAs(fileName, XlFileFormat.xlWorkbookDefault, Missing, Missing, false, Missing, XlSaveAsAccessMode.xlNoChange);


            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            finally
            {
                ExcelProje.Close(true, Missing, Missing);
                ExcelUygulama.Quit();
            }
        }

    }
}
