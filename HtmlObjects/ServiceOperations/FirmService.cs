using HtmlObjects.BusinessOperations.AdapterOperations;
using HtmlObjects.BusinessOperations.POCO;
using HtmlObjects.DataOperations.DataReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HtmlObjects.ServiceOperations
{
    /// <summary>
    /// 
    ///  default constructor ile olusturulan firmservice nesnesi araciligiyla, getfirmlist(hashtable) metodunu kullanarak geriye firma listesi alinir.
    ///  Hashtable key = url adresi, value ise firma isimlerini barindiran html tagi olmalidir....
    ///  
    ///  FirmService source,htmltagforfirmname parametreleri ile olusturulan servis ile default getFirmList() metodu kullanmaniz yeterlidir....
    ///  
    ///  FirmService builder,sourcepath,htmltag parametrelerinden builder nesnesi web ise sourcepath url, file ise dosya yolu olmalidir. Bu sekilde
    ///  olusturulan servis nesnesinden default getfirmlist diyerek firma listesini elde edebilirsiniz..
    ///  
    /// </summary>
    public class FirmService:Service
    {
       

        FirmAdapter adapter;

        String HtmlTag;

       

        /// <summary>
        /// Source'u dışarıdan da alır
        /// </summary>
        /// <param name="source"></param>
        public FirmService(String source, String htmlTagForFirmName)
        {
            HtmlTag = htmlTagForFirmName;
            adapter = new FirmAdapter(source, HtmlTag);
        }


        /// <summary>
        /// Html Sayfası okunuyor
        /// </summary>
        /// <param name="pageBuilder"></param>
        /// <param name="sourcePath"></param>
        public FirmService(HtmlPageBuilder pageBuilder, String sourcePath, String HtmlTagForFirmName)
        {
            this.builder = pageBuilder;
            this.path = sourcePath;
            this.HtmlTag = HtmlTagForFirmName;

            LoadHtml();

            adapter = new FirmAdapter(HtmlDocument, HtmlTag);

        }

        /// <summary>
        /// Kendi paketi dışarısından kullanıma kapalı,ComparisonService tarafından kullanılacak
        /// </summary>
        public FirmService() { }

        /// <summary>
        /// hashTable Key = url , Value = tag , verilen hashtable'daki url ve taglar ile bulduğu firma listelerini birleştirerek geri döndürür.
        /// </summary>
        /// <param name="hashTable"></param>
        /// <returns></returns>
        public List<Firm> getFirmList(Hashtable hashTable)
        {
            builder = new HtmlPageFromWebBuilder();

            FirmAdapter adapter;

            List<Firm> firmList = new List<Firm>();

            foreach(DictionaryEntry item in hashTable)
            {
                path = Convert.ToString(item.Key);
                HtmlTag = Convert.ToString(item.Value);
                LoadHtml();

                adapter = new FirmAdapter(HtmlDocument,HtmlTag);

                List<Firm> list = adapter.GetFirmList();

                foreach(var firma in list)
                {
                    firmList.Add(firma);
                }

            }
            return firmList;
        }


        /// <summary>
        /// Firma Listesi
        /// </summary>
        /// <returns></returns>
        public List<Firm> getFirmList()
        {
            return adapter.GetFirmList();
        }


        /// <summary>
        /// Firmalarla eşleştirilmiş olan telefonları listeler
        /// </summary>
        /// <returns></returns>
        public List<string> getFirmPhoneList()
        {
            if(adapter != null)
            {
                List<Firm> firmList = adapter.GetFirmList();

                List<string> phoneList = new List<string>();

                foreach (var firm in firmList)
                {

                    if (firm.firmPhone != null)
                    {
                        if (firm.firmPhone.Length > 0)
                        {

                            phoneList.Add(firm.firmPhone);

                        }

                    }

                }

                return phoneList;
            }
            else
            {
                return null;
            }
        } //GetFirmList metodu tekrar çağırılıyor

        /// <summary>
        /// Firmalarla eşleştirilmiş olan fax noları listeler
        /// </summary>
        /// <returns></returns>
        public List<string> getFirmFaxList()
        {
            if(adapter != null)
            {
                List<Firm> firmList = adapter.GetFirmList();

                List<string> faxList = new List<string>();

                foreach (var firm in firmList)
                {

                    if (firm.firmFax != null)
                    {
                        if (firm.firmFax.Length > 0)
                        {

                            faxList.Add(firm.firmFax);

                        }

                    }

                }

                return faxList;
            }
            else
            {
                return null;
            }


        } //GetFirmList metodu tekrar çağırılıyor

        /// <summary>
        /// Firmalar ile eşleşen mail listesi
        /// </summary>
        /// <returns></returns>
        public List<string> getFirmMailList()
        {
            if(adapter != null)
            {
                List<Firm> firmList = adapter.GetFirmList();

                List<string> mailList = new List<string>();

                foreach (var firm in firmList)
                {

                    if (firm.firmMail != null)
                    {
                        if (firm.firmMail.Length > 0)
                        {

                            mailList.Add(firm.firmMail);

                        }

                    }

                }

                return mailList;
            }
            else
            {
                return null;
            }
        } //GetFirmList metodu tekrar çağırılıyor



        //Data Writer'a Service aracılığı ile ulaşan metotlar

        public void SaveAsExcelFile(List<Firm> firmList, String fileName)
        {
            String[] titles = { "Firma İsmi", "Telefon Numarası", "Faks Numarası", "Web Adresi", "Mail Adresi" };
            List<string> dataList = new List<string>();
            if (firmList != null)
            {
                foreach (var item in firmList)
                {
                    dataList.Add(item.firmName);
                    dataList.Add(item.firmPhone);
                    dataList.Add(item.firmFax);
                    dataList.Add(item.firmWebSite);
                    dataList.Add(item.firmMail);
                }
            }
            PrintExcel(dataList, titles, fileName);

        }


    }
}
