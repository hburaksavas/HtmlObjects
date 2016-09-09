using HtmlObjects.BusinessOperations.DAO;
using HtmlObjects.BusinessOperations.POCO;
using HtmlObjects.DataOperations.DataReader;
using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlObjects.ServiceOperations
{
    public class ReaderService
    {
        private List<OsbWebAddress> Osb_UrlList = new List<OsbWebAddress>();

        private List<Firm> FirmList = new List<Firm>();

        private List<TelNo> TelNumberListFromDB = new List<TelNo>();

        private List<Email> MailListFromDB = new List<Email>();

        private ReaderService() { }

        /// <summary>
        /// delegate ile kullanılması tavsiye edilir
        /// </summary>
        /// <returns></returns>
        public static List<Firm> getFirmListByXML()
        {
            ReaderService reader = new ReaderService();

            reader.PullWebSiteFromXML();
            reader.PullFirmList();

            return reader.FirmList;
        }

        /// <summary>
        /// delegate ile kullanılması tavsiye edilir
        /// </summary>
        /// <returns></returns>
        public static List<TelNo> getTelListFromDB()
        {

            ReaderService reader = new ReaderService();
            reader.PullPhoneListFromDB();
            return  reader.TelNumberListFromDB;

        }
        
        /// <summary>
        /// delegate ile kullanılması tavsiye edilir
        /// </summary>
        /// <returns></returns>
        public static List<Email> getMailListFromDB()
        {
            ReaderService reader = new ReaderService();
            reader.PullMailListFromDB();
            return reader.MailListFromDB;
        }

        private void PullWebSiteFromXML()
        {
            try {
                BusinessOperations.XmlOperations.WebAddressConfig configWeb = new BusinessOperations.XmlOperations.WebAddressConfig();
                Osb_UrlList = configWeb.read();

            }
            catch ( Exception e ) {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
        }
        private void PullFirmList()
        {
            if ( Osb_UrlList != null ) {
                try {
                    FirmService firmService = null;
                    HtmlPageBuilder builder = new HtmlPageFromWebBuilder();

                     Parallel.For(0, Osb_UrlList.Count, i =>
                     {

                         firmService = new FirmService(builder, Osb_UrlList[i].Url, Osb_UrlList[i].HtmlTag);

                         IEnumerable<Firm> enumerableFirms= firmService.getFirmList();               
                         FirmList.AddRange( enumerableFirms );

                         Console.WriteLine("{0} adresinden bulunan firmalar listeye eklendi", Osb_UrlList[i].Url);
                     });

                }
                catch ( Exception e ) {
                    PrintConsole.LOG(e.StackTrace, e.Message);
                }
                finally {


                    Console.WriteLine("Bütün Web Adreslerinden Tarama İşlemi Sonlandı");
                }

            }

        }
        private void PullPhoneListFromDB()
        {
            try {
                
                TelNumberDAO telDAO = new TelNumberDAO();
                TelNumberListFromDB = telDAO.GetTelNumberList();

                int TelNumberListFromDbCount = TelNumberListFromDB.Count;

                 Parallel.For(0, TelNumberListFromDbCount, i =>
                 {
                     String telNo = TelNumberListFromDB[i].TelefonNumarasi;
                     telNo = Regex.Replace(telNo, " ", "");
                     telNo = " " + telNo + " ";
                     telNo = Regex.Replace(telNo, " [0-9] ", "");
                     telNo = telNo.Trim();
                     TelNumberListFromDB[i].TelefonNumarasi = telNo;

                 });
          
            }
            catch ( Exception e ) {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            finally {
             
                Console.WriteLine("OSB-DB'den Telefon Numaraları Çekme İşlemi Sonlandı");              
            }

        }
        private void PullMailListFromDB()
        {
            try
            {

                MailAdresDAO dao = new MailAdresDAO();
                MailListFromDB = dao.GetAllMails();

            }catch(Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            finally
            {

                Console.WriteLine("OSB-DB'den Mailleri Çekme İşlemi Sonlandı"+MailListFromDB.Count);
            }

        }

    }
}
