using HtmlObjects.BusinessOperations.CoreOperations;
using HtmlObjects.BusinessOperations.MappingOperations.FirmFields;
using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.MappingOperations
{
    class MailAddressMapping
    {
        private String MappingMailSource;

        private List<string> MailList;

        private List<Firm> FirmList;

        private List<MailAddress> AllMatchedMailObjectsList;





        public MailAddressMapping(String source)
        {
            MappingMailSource = source;
            MappingMailSource = RemoveForMappingMails(MappingMailSource);
            MailList = MailListAdapter(MappingMailSource);
        }

        public MailAddressMapping(List<string> mailList, String source)
        {
            MappingMailSource = source;
            MappingMailSource = RemoveForMappingMails(MappingMailSource);
            MailList = mailList;
        }

        public MailAddressMapping(List<Firm> firmList,String source)
        {
            MappingMailSource = source;
            MappingMailSource = RemoveForMappingMails(MappingMailSource);
            MailList = MailListAdapter(MappingMailSource);
            FirmList = firmList;
        }






        /// <summary>
        /// Global değişken MappingMailSource içerisinde arama yapar,eşleşyirsa gelen firm nesnesine mail ekler ve return eder
        /// Ayrıca eşleşme durumunda MappingMailSource içerisinden eşlenen değeri siler(performans nedeniyle)
        /// Ve son olarak MailList içerisinde eşleşen maili siler.
        /// </summary>
        /// <param name="firm"></param>
        /// <param name="Mails"></param>
        /// <returns></returns>
        public Firm mappingMailAddress(Firm firm)
        {
            String firmName = firm.firmName;
            Regex regex;
            String pattern;
            Match match;
            List<string> Mails = MailList;

            if (firmName.Length > 0)
            {
                foreach (var mail in Mails)
                {
                    pattern = firmName + "(.*?)" + mail;
                    regex = new Regex(pattern);
                    match = regex.Match(MappingMailSource); // mail source içerisinde eşleştirme aranıyor
                    if (match.Success) // eşleşme varsa
                    {
                        if (match.Length > 30) // eşleşen değerin uzunluğu en az 31 olmalı
                        {
                            if (match.Length < 300) // en çok 149 olmalı
                            {
                                firm.firmMail = mail; // gelen firm nesnesinin mail alanı set edilip geri döndürülecek
                                MappingMailSource = MappingMailSource.Replace(match.Value, "");
                                MailList.Remove(mail);

                            }
                            else // Eşleşme uzunluğu 150den büyükse doğru eşleşme sağlanmamıştır.
                            {
                                MappingMailSource = MappingMailSource.Replace(match.Value, "");
                                MailList.Remove(mail);
                            }

                        }
                        break; // eşleşirse döngüden çık
                    }
                }
            }

            return firm;
        }

        public List<Firm> mappingMailAddress()
        {
            if(FirmList != null)
            {
                List<MailAddress> mailAddressListLR = getMatchedMailListLefToRight();
                List<MailAddress> mailAddressListRL = getMatchedMailListRightToLeft();

                AllMatchedMailObjectsList = mailAddressListLR;
                foreach (var item in mailAddressListRL)
                {
                    AllMatchedMailObjectsList.Add(item);
                }

                List<MailAddress> distinctWebList = GetDistinctMailObjectList();

                List<Firm> resultFirmList = new List<Firm>();

                foreach (var firm in FirmList)
                {
                    Firm newFirm = firm;


                    foreach (var item in distinctWebList)
                    {

                        if (item.FirmNameValue.Equals(newFirm.firmName))
                        {
                            newFirm.firmMail = item.MailValue;
                        }

                    }

                    resultFirmList.Add(newFirm);
                }
                return resultFirmList;
            }
            else
            {
                return FirmList;
            }
        }
        


        private List<MailAddress> GetDistinctMailObjectList()
        {
            List<string> mailKeyList = new List<string>();
            List<MailAddress> distinctMailObjectList = new List<MailAddress>(); // distinct web url ve en küçük eşleşme uzunluğuna göre firma ismi tutacak liste
            try
            {
                foreach (var item in AllMatchedMailObjectsList)
                {
                    mailKeyList.Add(item.MailValue);
                }

                IEnumerable<string> distinctMailList = mailKeyList.Distinct(); // web url tutulan liste
                List<int> matchLengthList = new List<int>(); // eşleşme uzunluklarının tutulduğu liste

                foreach (var item in distinctMailList)
                {

                    foreach (var mailaddress in AllMatchedMailObjectsList)
                    {

                        if (mailaddress.MailValue.Equals(item))
                        {

                            matchLengthList.Add(mailaddress.matchedLength);
                         
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
                    foreach (var mailaddress in AllMatchedMailObjectsList)
                    {

                        if (mailaddress.matchedLength.Equals(minLength) && mailaddress.MailValue.Equals(item))
                        {
                            MailAddress mailValue = new MailAddress();
                            mailValue.MailValue = item;
                            mailValue.FirmNameValue = mailaddress.FirmNameValue;
                            distinctMailObjectList.Add(mailValue);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

            return distinctMailObjectList;
        }


        private List<MailAddress> getMatchedMailListLefToRight()
        {
            List<MailAddress> resultList = new List<MailAddress>();
            String MailSource = MappingMailSource;
            List<string> mailList = MailList;

            String pattern;
            Regex regex;
            Match match;

            foreach (var firm in FirmList)
            {
                String firmName = firm.firmName;
                foreach (var mail in mailList)
                {
                    pattern = firmName + "(.*?)" + mail;
                    regex = new Regex(pattern);
                    match = regex.Match(MailSource); // mail source içerisinde eşleştirme aranıyor
                    if (match.Success) // eşleşme varsa
                    {

                        if (match.Length < 300) 
                        {
                            MailAddress mailObj = new MailAddress();

                            mailObj.MailValue = mail; 
                            mailObj.FirmNameValue = firmName;
                            mailObj.matchedLength = match.Length;
                            resultList.Add(mailObj);

                            MailSource = MailSource.Replace(match.Value, "");
                            
                        }
                       
                    }
                }
            }
            return resultList;
        }
        private List<MailAddress> getMatchedMailListRightToLeft()
        {
            List<MailAddress> resultList = new List<MailAddress>();
            String MailSource = MappingMailSource;
            List<string> mailList = MailList;

            String pattern;
            Regex regex;
            Match match;
            int firmsCount = FirmList.Count;
            for(int i=firmsCount-1;i>=0;i--)
            {
                String firmName = FirmList[i].firmName;
                foreach (var mail in mailList)
                {
                    pattern = firmName + "(.*?)" + mail;
                    regex = new Regex(pattern);
                    match = regex.Match(MailSource); // mail source içerisinde eşleştirme aranıyor
                    if (match.Success) // eşleşme varsa
                    {

                        if (match.Length < 300)
                        {
                            MailAddress mailObj = new MailAddress();

                            mailObj.MailValue = mail;
                            mailObj.FirmNameValue = firmName;
                            mailObj.matchedLength = match.Length;
                            resultList.Add(mailObj);

                            MailSource = MailSource.Replace(match.Value, "");

                        }

                    }
                }
            }
            return resultList;
        }





        /// <summary>
        /// Mailleri eşleştirmeden önce verilen texti düzenler ve geri döner
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String RemoveForMappingMails(String text)
        {
            text = text.ToLower().RemoveForAllMappingOperations();

            string[] patterns = new string[]
         {
               @"( \.tr)",
                ":",
                " www.[a-z]*. ",
                " [0-9] ",

         };

            String sourceText = text;
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

        private List<string> MailListAdapter(string text)
        {
            List<string> resultList = text.SelectMails();
            return resultList;
        }
    }
}
