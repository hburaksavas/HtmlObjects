using HtmlObjects.BusinessOperations.DAO;
using HtmlObjects.BusinessOperations.POCO;
using HtmlObjects.DataOperations.DbOperations.ConnectOperations;
using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlObjects.ServiceOperations
{

    /// <summary>
    /// Karsilastirma islemlerini veritabanina birakildigi sinif
    /// </summary>
    public class CompareService
    {

        public delegate List<Firm> GetFirmsDelegate();

        private List<Firm> FirmList = new List<Firm>();

        DataTable table = new DataTable();

        /// <summary>
        /// FirmList'i asenkron olarak doldurma isleminin basladigi metot,
        /// ReaderService'i kullanır
        /// </summary>
        /// <returns></returns>
        public int Start()
        {
            try
            {
                

                GetFirmsDelegate getFirms = ReaderService.getFirmListByXML;
               

                IAsyncResult firmResult = getFirms.BeginInvoke(null, null);
            


                FirmList = getFirms.EndInvoke(firmResult);
               

                if (FirmList != null )
                {
                    Mapping();

                }else
                {
                    throw new Exception("FirmListCount are null");
                }

            }
            catch (Exception e)
            {

                PrintConsole.LOG(e.StackTrace, e.Message);

            }
            return 1;
        }


        /// <summary>
        /// Start metodu tarafından çağrılır ve mapping işlemi başlatılır
        /// </summary>
        private void Mapping()
        {

            Console.WriteLine("Mapping işlemi başlatılıyor, Telefon listesi çekildi, web üzerinden firmalar çekildi");

           
            compareOperation();


        }


        /// <summary>
        /// Karşılaştırma, Eşleştirme işlemleri bu metot içerisinde yapılıyor
        /// </summary>
        private void compareOperation()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
               
            try
            {

                int FirmListCount = FirmList.Count;
                             
                #region web'ten alınan firm listesindeki veriler karsilastirma islemi icin duzenleniyor

                Parallel.For(0, FirmListCount, i =>
                {
                    String telNo = FirmList[i].firmPhone;
                    String mail = FirmList[i].firmMail;

                    if (!String.IsNullOrEmpty(mail))
                    {
                        mail = Regex.Replace(mail, " ", "");
                        FirmList[i].firmMail = mail;
                    }
                    
                    telNo = Regex.Replace(telNo, " ", "");
                    FirmList[i].firmPhone = telNo;
                    
                });

                #endregion

                //Datatable column ayarlari
                configDataTable();

                //FirmList'ten distinct bir Enumerable collection elde ediliyor
                IEnumerable<Firm> firmList = FirmList.AsParallel().GroupBy(o => new { o.firmName, o.firmPhone, o.firmFax, o.firmMail, o.firmWebSite })
                                                                    .Select(o => o.FirstOrDefault());

                FirmList = null; //Distinct olmayan liste alanı serbest bırakılıyor

                if ( firmList!= null)
                {


                    foreach (var item in firmList)
                    {
                        var row = table.NewRow();

                        row[0] = item.firmName;
                        row[1] = item.firmPhone;
                        row[2] = item.firmFax;
                        row[3] = item.firmMail;
                        row[4] = item.firmWebSite;
                      
                        table.Rows.Add(row);

                    }

                    Console.WriteLine("DataTable'a veriler başarıyla yüklendi");

                    Console.WriteLine("DataTable'da bulunan kayiy sayisi {0}", table.Rows.Count);

                   

                    //Firmaların tel ve maillerini eşleştiren proc
                    string mappingFirmsProc = "dbo.sp_mappingfirms";
                    
                    //Eşleşmeyenleri bulan proc
                    string unmatchedFirmsProc = "dbo.sp_unmatchedfirms";

                    GeneralDAO cmd = new GeneralDAO();
                    cmd.executeProcedureWithTableParam(table, mappingFirmsProc);
                    cmd.executeProcedureWithTableParam(table, unmatchedFirmsProc);

                }

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            finally
            {
                watch.Stop();
                Console.WriteLine("Karsilastirma islemi {0} saniyede tamamlandi", watch.Elapsed.TotalSeconds.ToString());
          
            }
        }


        /// <summary>
        /// [firmname][telno][fax][mail][webadresi] sutunlari table'a ekleniyor
        /// </summary>
        private void configDataTable()
        {
            table.Columns.Add("firmname", typeof(string));            
            table.Columns.Add("telno", typeof(string));
            table.Columns.Add("fax", typeof(string));
            table.Columns.Add("mail", typeof(string));
            table.Columns.Add("webadresi", typeof(string));
        }

    }
}
