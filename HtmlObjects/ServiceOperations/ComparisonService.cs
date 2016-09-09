using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlObjects.BusinessOperations.POCO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using HtmlObjects.DataOperations.DbOperations.Entities;

namespace HtmlObjects.ServiceOperations
{

    /// <summary>
    /// Karsilastirma ve eslestirme islemlerini yapan  matchedfirm ve unmatchedfirm listelerini adapte sinif
    /// </summary>
    public class ComparisonService
    {
        #region Delegate Tanımlari

        public delegate List<Firm> GetFirmsDelegate();
        public delegate List<TelNo> GetTelsDelegate();
        public delegate List<Email> GetMailsDelegate();

        #endregion

        #region List nesneleri

        private List<UnmatchedFirm> list_UnmatchedFirms = new List<UnmatchedFirm>();
        private List<MatchedFirm> list_MatchedFirms = new List<MatchedFirm>();

        private List<Firm> FirmList = new List<Firm>();

        private List<TelNo> TelNumberListFromDB = new List<TelNo>();      
        private List<Email> EmailListFromDB = new List<Email>();
        
        #endregion


        /// <summary>
        /// Listelerin yüklenme işlemine başlanıp başlanmadığı kontrolü
        /// </summary>
        private bool isStarted = false;


        public ComparisonService()
        {
        }




        /// <summary>
        /// Listeleri asenkron olarak doldurma işleminin başladığı metot,
        /// ReaderService'i kullanır
        /// </summary>
        /// <returns></returns>
        private int Start()
        {
            try
            {
                isStarted = true;

                GetFirmsDelegate getFirms = ReaderService.getFirmListByXML;
                GetTelsDelegate getTels = ReaderService.getTelListFromDB;
                GetMailsDelegate getMails = ReaderService.getMailListFromDB;

                IAsyncResult firmResult = getFirms.BeginInvoke(null, null);
                IAsyncResult telResult = getTels.BeginInvoke(null, null);              
                IAsyncResult mailResult = getMails.BeginInvoke(null, null);


                FirmList = getFirms.EndInvoke(firmResult);

                TelNumberListFromDB = getTels.EndInvoke(telResult);
                
                EmailListFromDB = getMails.EndInvoke(mailResult);


                if (FirmList != null && TelNumberListFromDB != null)
                {                 
                    Mapping();
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

            // Loading matchedList and unMatchedList
            UnmatchedPhonesAndSaveMatches();


         
 

            ReleaseBigData();

        }

        /// <summary>
        /// EmailListFromDB,TelNumberListFromDB ve FirmList listelerini boşaltma işlemi
        /// </summary>
        private void ReleaseBigData()
        {
            TelNumberListFromDB = null;
            FirmList = null;
            EmailListFromDB = null;
        }



        /// <summary>
        /// OSB DB'deki firmalar ile web'den çekilen firmalardan eşleşenleri geri döndürür
        /// </summary>
        /// <returns></returns>
        public List<MatchedFirm> GetMatched_FirmList()
        {
            if (!isStarted)
            {
                int response = Start();

            }
            return list_MatchedFirms;

        }

        /// <summary>
        /// Web'den çekilen firmalardan OSB DB'deki firmalar ile eşleşmeyenleri geri döndürür
        /// </summary>
        /// <returns></returns>
        public List<UnmatchedFirm> GetUnmatched_FirmList()
        {
            if (!isStarted)
            {
                int response = Start();

            }

            return list_UnmatchedFirms;
        }




        /// <summary>
        /// Karşılaştırma, Eşleştirme işlemleri bu metot içerisinde yapılıyor
        /// </summary>
        private void UnmatchedPhonesAndSaveMatches()
        {
            Stopwatch watch = new Stopwatch();
            try
            {

                watch.Start();
                int FirmListCount = FirmList.Count;
                List<string> telListFromWeb = new List<string>();

                #region web'ten alınan firm listesindeki telefon numaraları listeye alınıyor, unique list
                Parallel.For(0, FirmListCount, i =>
                {
                    String telNo = FirmList[i].firmPhone;
                    telNo = Regex.Replace(telNo, " ", "");
                    telListFromWeb.Add(telNo);
                    FirmList[i].firmPhone = telNo;
                });
                #endregion

                Console.WriteLine("Web adreslerinden cekilen Firma sayisi {0}", telListFromWeb.Count);
                Console.WriteLine("OSB-DB'den cekilen Telefon Numarasi sayisi {0}", TelNumberListFromDB.Count);
                Console.WriteLine("OSB-DB'den cekilen Email sayisi {0}", EmailListFromDB.Count);

                BusinessOperations.CompareOperations.CompareFirmInfos compObj = new BusinessOperations.CompareOperations.CompareFirmInfos();

                #region Karşılaştırma işlemi                
                Parallel.For(0, telListFromWeb.Count, i =>
                  {
                      String item_FirmTel = telListFromWeb[i];

                      Console.WriteLine("{0} Firma Karşılaştırma İşlemi", i);

                      if (!String.IsNullOrEmpty(item_FirmTel))
                      {
                          //webten gelen telNo ile DB'den gelen telNo eşleşirse,geriye müşteriKodu döner.
                          decimal _MusteriKodu_matchedWithPhoneNumber = compObj.ComparePhoneNumber(item_FirmTel, ref TelNumberListFromDB);



                          //-1 değilse müşteriKodu'dur ve telNolar eşleşmiştir.
                          if (_MusteriKodu_matchedWithPhoneNumber != -1)
                          {

                              //Webten alınan Firma listesinden mail adres
                              string itemMail = GetMailByTelFromFirmList(item_FirmTel);

                              itemMail = Regex.Replace(itemMail, " ", "");


                              Firm firm = GetFirmByTelNumber(item_FirmTel);
                              MatchedFirm _matchedFirm = new MatchedFirm();

                              if (firm != null)
                              {

                                  _matchedFirm = new MatchedFirm
                                  {
                                      Unvan = firm.firmName,
                                      Tel = firm.firmPhone,
                                      Fax = firm.firmFax,
                                      Mail = firm.firmMail,
                                      Website = firm.firmWebSite,
                                      MusteriKodu = _MusteriKodu_matchedWithPhoneNumber
                                  };

                              }


                              //Mail adres alanı boş değilse,eşleşme var mı diye bak

                              if (!String.IsNullOrEmpty(itemMail))
                              {
                                  if (isExistsInEmailList(itemMail, _MusteriKodu_matchedWithPhoneNumber))
                                  {
                                      //tel ve mail eşleşmişse status=11
                                      _matchedFirm.TelMailStatus = "11";

                                  }
                                  else
                                  {
                                      //sadece tel eşleşmişse statsu = 10
                                      _matchedFirm.TelMailStatus = "10";
                                  }

                              }
                              else
                              {
                                  //mail yok ve tel ile eşleşmişse status=10
                                  _matchedFirm.TelMailStatus = "10";


                              }

                              AddMatchedFirm(_matchedFirm);

                          }
                          else
                          {

                              Firm firm = GetFirmByTelNumber(item_FirmTel);

                              AddUnmatchedFirm(new UnmatchedFirm
                              {
                                  Unvan = firm.firmName,
                                  Tel = firm.firmPhone,
                                  Mail = firm.firmMail,
                                  Fax = firm.firmFax,
                                  Website = firm.firmWebSite

                              });
                          }
                      }

                  });
                #endregion

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            finally
            {
                watch.Stop();
                Console.WriteLine("Karsilastirma islemi {0} dkda tamamlandi sn {1}", watch.Elapsed.TotalMinutes.ToString(),watch.Elapsed.TotalSeconds.ToString());
            }
        }

        private Boolean isExistsInEmailList(string mailAdres, decimal musteriKodu)
        {
            foreach (var item in EmailListFromDB)
            {
                if (item.MusteriKod.Equals(musteriKodu))
                {

                    if (item.MailAdresi.Equals(mailAdres))
                    {
                        return true;
                    }
                }
            }

            return false;

        }




        private string GetMailByTelFromFirmList(string telNo)
        {

            foreach (var item in FirmList)
            {
                if (!String.IsNullOrEmpty(item.firmMail))
                {
                    if (item.firmPhone.Equals(telNo))
                    {
                        return item.firmMail;
                    }
                }
            }
            return String.Empty;

        }

        private Firm GetFirmByTelNumber(string telNo)
        {
            foreach (var item in FirmList)
            {

                if (item.firmPhone.Equals(telNo))
                {
                    return item;
                }

            }

            return null;
        }




        /// <summary>
        /// MatchedFirm Listeye Ekle
        /// </summary>
        /// <param name="m_Firm"></param>
        /// <returns></returns>
        private void AddMatchedFirm(MatchedFirm m_Firm)
        {
            if (m_Firm != null)
            {

                list_MatchedFirms.Add(m_Firm);

            }

        }

        /// <summary>
        /// UnmatchedFirm Listeye Ekle
        /// </summary>
        /// <param name="u_Firm"></param>
        /// <returns></returns>
        private void AddUnmatchedFirm(UnmatchedFirm u_Firm)
        {

            if (u_Firm != null)
            {

                list_UnmatchedFirms.Add(u_Firm);

            }

        }


    }
}
