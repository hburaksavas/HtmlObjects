using HtmlObjects.BusinessOperations.CoreOperations;
using HtmlObjects.BusinessOperations.MappingOperations.FirmFields;
using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.MappingOperations
{
    class WebAddressMapping
    {
        private String MappingWebAddressSource;


        private List<string> WebUrls;

        private List<WebAddress> allMatchedWEBADDRESS;
       



        public WebAddressMapping(string source)
        {
            MappingWebAddressSource = source;
            MappingWebAddressSource = RemoveForMappingWebAdress(MappingWebAddressSource);
            WebUrls = WebAdressListAdapter(MappingWebAddressSource);
            
        }


        public List<Firm> mappingWebAddress(List<Firm> firmList)
        {
            string SourceCopy = MappingWebAddressSource;

            List<string> webAddressListCopy = WebUrls;
            List<Firm> firmListCopy = firmList;

            List<WebAddress> webAddressListLR = mappingLeftoRight(firmListCopy,webAddressListCopy,SourceCopy);
            List<WebAddress> webAddressListRL = mappingRightToLeft(firmListCopy, webAddressListCopy, SourceCopy);

            allMatchedWEBADDRESS = webAddressListLR;
            foreach(var item in webAddressListRL)
            {
                allMatchedWEBADDRESS.Add(item);
            }

            List<WebAddress> distinctWebList = GetDistinctWebAddres();

            List<Firm> resultFirmList = new List<Firm>();

                foreach(var firm in firmList)
                {
                     Firm newFirm = firm;


                foreach (var item in distinctWebList)
                {

                    if (item.firmNameKey.Equals(newFirm.firmName))
                    {
                        newFirm.firmWebSite = item.webKey;
                    }

                }
      
                     resultFirmList.Add(newFirm);
            }
            return resultFirmList;
        }



        private List<WebAddress> GetDistinctWebAddres()
        {
            List<string> webAddresKeyList = new List<string>();
            List<WebAddress> distinctWebAddressList = new List<WebAddress>(); // distinct web url ve en küçük eşleşme uzunluğuna göre firma ismi tutacak liste
            try
            {
                foreach (var item in allMatchedWEBADDRESS)
                {
                    webAddresKeyList.Add(item.webKey);
                }

                IEnumerable<string> distinctWebList = webAddresKeyList.Distinct(); // web url tutulan liste
                List<int> matchLengthList = new List<int>(); // eşleşme uzunluklarının tutulduğu liste
             
                foreach (var item in distinctWebList)
                {

                    foreach (var webAdres in allMatchedWEBADDRESS)
                    {

                        if (webAdres.webKey.Equals(item))
                        {
                            matchLengthList.Add(webAdres.matcLength);
                           
                        }
                    }
                    int minLength = 1001;
                    foreach (var count in matchLengthList)
                    {
                        if (count < minLength)
                        {
                            minLength = count;
                        }
                    }
                    matchLengthList.Clear();
                    foreach (var webAdres in allMatchedWEBADDRESS)
                    {

                        if (webAdres.matcLength.Equals(minLength) && webAdres.webKey.Equals(item))
                        {
                            WebAddress webValue = new WebAddress();
                            webValue.firmNameKey = webAdres.firmNameKey;
                            webValue.webKey = item;
                            distinctWebAddressList.Add(webValue);
                        }
                    }

                }
            }
            catch(Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
           
            return distinctWebAddressList;
        }
        private List<WebAddress> mappingLeftoRight(List<Firm> firmList,List<string> WebSiteList,String BaseSource) 
        {
            String Source = BaseSource;
            Regex regex;
            String pattern;
            Match match;
            WebAddress webAdresInfos = null;
            List<WebAddress> matchedWebAddress = new List<WebAddress>();
            if (firmList != null)
            {
                foreach(var firm in firmList)
                {
                    String firmName = firm.firmName;

                        foreach (var web in WebUrls)
                        {
                            pattern = firmName + "(.*?)" + web;
                            regex = new Regex(pattern);
                            match = regex.Match(Source); // web source içerisinde eşleştirme aranıyor
                            
                            if (match.Success) // eşleşme varsa
                            {
                                if (match.Length < 160)
                                {
                                    webAdresInfos = new WebAddress();
                                    webAdresInfos.matcLength = match.Length;
                                    webAdresInfos.webKey = web;
                                    webAdresInfos.firmNameKey = firmName;
                                    matchedWebAddress.Add(webAdresInfos);
                                    Source = Source.Replace(match.Value, "");
                                }
                            }
                        }
                }
               
            }
            return matchedWebAddress;

        }
        private List<WebAddress> mappingRightToLeft(List<Firm> firmList,List<string> WebSiteList,String BaseSource)
        {
            String Source = BaseSource;
            Regex regex;
            String pattern;
            Match match;
            WebAddress webAdresInfos = null;
            List<WebAddress> matchedWebAddress = new List<WebAddress>();
            if (firmList != null)
            {
                int firmsCount = firmList.Count;
                for (int i = firmsCount-1; i >= 0;i--)
                {
                    String firmName = firmList[i].firmName;

                    foreach (var web in WebUrls)
                    {
                        pattern = firmName + "(.*?)" + web;
                        regex = new Regex(pattern);
                        match = regex.Match(Source); // web source içerisinde eşleştirme aranıyor

                        if (match.Success) // eşleşme varsa
                        {
                            if (match.Length < 160)
                            {
                                webAdresInfos = new WebAddress();
                                webAdresInfos.matcLength = match.Length;
                                webAdresInfos.webKey = web;
                                webAdresInfos.firmNameKey = firmName;
                                matchedWebAddress.Add(webAdresInfos);
                                Source = Source.Replace(match.Value, "");
                            }
                        }
                    }
                }

            }
            return matchedWebAddress;
        }




        /// <summary>
        /// Web Adresleri eşleştirmeden önce source'u düzenler
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String RemoveForMappingWebAdress(String text)
        {
            text = text.ToLower().RemoveForAllMappingOperations();
            string[] patterns = new string[] {
                        ":",
                        "[ ]{2,5}",
                        "-",
                        "'",
                        "[0-9]",
                        @" \. ","[a-z]*@[a-z]....","[ ]{3,}"," cd."," no","fax","faks"

             };

            String sourceText = text.ToLower();
            foreach (var pattern in patterns)
            {
                try
                {
                    sourceText = Regex.Replace(sourceText, pattern, " ");
                }
                catch (Exception e)
                {
                    PrintConsole.LOG(e.StackTrace, e.Message);
                }

            }
            return sourceText;
        }

        private List<String> WebAdressListAdapter(String text)
        {
            List<string> webList = text.SelectWebUrls();
            return webList;
        }


    }
}
