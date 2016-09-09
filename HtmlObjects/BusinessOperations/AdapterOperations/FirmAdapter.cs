using HtmlObjects.BusinessOperations.CoreOperations;
using HtmlObjects.BusinessOperations.MappingOperations;
using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlObjects.BusinessOperations.AdapterOperations
{
    class FirmAdapter
    {
        string source;

        private List<Firm> firmList;
        private String HtmlTag;


        public FirmAdapter(string source, string firmNameHtmlTag)
        {
            this.source = source;
            this.HtmlTag = firmNameHtmlTag;
            firmList = new List<Firm>();
        }


        /// <summary>
        /// FirmService tarafından çağırılacak metot
        /// </summary>
        /// <returns></returns>
        public List<Firm> GetFirmList()
        {
            if (!String.IsNullOrEmpty(HtmlTag)&&!String.IsNullOrEmpty(source))
            {
                FirmNameMapping mapping = new FirmNameMapping(source, HtmlTag);
                List<string> FirmNameList = mapping.GetNameList();
                //Firma isimleri formatına uyan data liste olarak alınıyor

                String htmlContent = source.SelectHtmlContent();
                htmlContent = htmlContent.Trim();
                htmlContent = htmlContent.ToLower();
                //Firma isimlerini de barındıran içerik
                if (!String.IsNullOrEmpty(htmlContent))
                {
                    List<Firm> FirmListWithPhones = GetFirmListAfterPhoneMapping(FirmNameList, htmlContent);
                    FirmListWithPhones = SortFirmList(FirmListWithPhones);

                    if(FirmListWithPhones != null)
                    {
                        firmList = mappingFirmInfos(htmlContent, FirmListWithPhones);
                        EditFirmInfos(ref firmList);
                        firmList = NormalizationList(firmList);

                    }
                  
                }
                
            }
           
            return firmList;
        }


        private List<Firm> GetFirmListAfterPhoneMapping(List<string> firmNameList,String textSource)
        {

            PhoneMapping mapOperation = new PhoneMapping();

            List<Firm> firmList = mapOperation.mappingPhoneNo(firmNameList, textSource);

            return firmList;

        }

        public List<Firm> SortFirmList(List<Firm> firmList)
        {


            var orderedList = from firm in firmList
                              orderby firm.firmName ascending
                              select new { firm };



            List<Firm> FirmList = new List<Firm>();
            foreach (var item in orderedList)
            {
                FirmList.Add(item.firm);
            }

            return FirmList;
        }

        public void EditFirmInfos(ref List<Firm> firmList)
        {
            try {

                foreach ( var firm in firmList ) {
                    string tel = " " + firm.firmPhone + " ";
                    string fax = firm.firmFax + " ";
                    tel = Regex.Replace(tel, " [0-9] ", "");

                    if ( tel.StartsWith("0") ) { //tel numarasının başındaki 0 atılıyor
                        string[] telDigits = Regex.Split(tel, "");
                        tel = "";
                        int telDigitsCount = telDigits.Length;
                        for ( int i = 1; i < telDigitsCount; i++ ) {
                            tel += telDigits[ i ];
                        }
                    }
                    fax = Regex.Replace(fax, " [0-9] ", "");
                    firm.firmPhone = tel;
                    firm.firmFax = fax;
                }
            }
            catch(Exception e ) {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
        }
       
        public List<Firm> NormalizationList(List<Firm> firmList)
        {
            IEnumerable<Firm> distnctList = firmList.Distinct();
            firmList = null;
            firmList = new List<Firm>();
            foreach(var item in distnctList)
            {
                firmList.Add(item);
            }
            return firmList;
        }
        
        
        /// <summary>
        /// Firma isimleri bulunur,Telefon Listesi ile eşleştirme yapılır
        /// Eşleşme sağlanarak oluşan firm nesneleri ile fax no,
        /// web adres ve mail eşleştirmeleri yapılır
        /// </summary>
        /// <returns></returns>
        private List<Firm> mappingFirmInfos(String BasicSource,List<Firm> FirmList)
        {

            #region 1- Fax eşleştirme

            String BasicSourceCopy = BasicSource;

            FaxMapping faxMapping = new FaxMapping(BasicSourceCopy, FirmList);

            List<Firm> tempList = new List<Firm>();

            FirmList = faxMapping.mappingFaxNo();

            #endregion

            #region 2- Web adress eşleştirme

            BasicSourceCopy = BasicSource;

            tempList = new List<Firm>();

            WebAddressMapping webMapping = new WebAddressMapping(BasicSourceCopy);

            FirmList = webMapping.mappingWebAddress(FirmList);


            #endregion

            #region 3- Mail eşleştirme

            BasicSourceCopy = BasicSource;

            MailAddressMapping mailMapping = new MailAddressMapping(FirmList,BasicSourceCopy);

            FirmList = mailMapping.mappingMailAddress();

            #endregion

            return FirmList;

        }


    }

}
