using HtmlObjects.BusinessOperations.CoreOperations;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.AdapterOperations
{
    class Adapter
    {
        private String source;

        /// <summary>
        /// Gelen source html taglarını barındırmalıdır.
        /// </summary>
        /// <param name="source"></param>
        public Adapter(String source)
        {
            this.source = source;
        }




        /// <summary>
        /// Html Sayfası içeriğindeki metini döndürme
        /// </summary>
        /// <returns></returns>
        public string InnerTextAdapter()
        {
            if (source != null)
            {
                return source.SelectHtmlContent();
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// Telefon numarası formatına yakın değerleri listeleme
        /// </summary>
        /// <returns></returns>
        public List<String> PhoneListAdapter()
        {
            List<String> phoneList = source.SelectPhoneNumbers();
            List<String> faxList = source.SelectFaxNumbers();

            for (int i = 0; i < faxList.Count; i++)
            {
                faxList[i] = faxList[i].RemoveLikeFaxWords();
                faxList[i] = faxList[i].Replace(":", "");
                faxList[i] = faxList[i].SelectPhoneNumbers()[0];
            }

            int phoneListCount = phoneList.Count;
            for (int i = 0; i < phoneListCount; i++)
            {
                foreach (var it in faxList)
                {
                    if (phoneList[i].Equals(it))
                    {
                        phoneList[i] = "";
                    }
                }
            }
            List<string> resultList = new List<string>();
            for (int i = 0; i < phoneListCount; i++)
            {

                Regex reg = new Regex("[a-z]{1}", RegexOptions.IgnoreCase);
                String item = phoneList[i];
                int count = 0;

                foreach (Match match in reg.Matches(item))
                {
                    count++;
                }

                if (count > 1)
                {
                    phoneList[i] = "";
                }

                if (!phoneList[i].Equals(""))
                {
                    resultList.Add(phoneList[i]);
                }
            }

            return resultList;

        }


        /// <summary>
        /// Fax numarası formatına yakın değerleri listeleme
        /// </summary>
        /// <returns></returns>
        public List<String> FaxListAdapter()
        {
            if (source != null)
            {
                List<string> faxList = source.SelectFaxNumbers();
                return faxList;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// Mail adres formatına yakın değerleri listeleme
        /// </summary>
        /// <returns></returns>
        public List<String> MailListAdapter()
        {
            if (source != null)
            {
                List<string> mailList = source.SelectMails();
                return mailList;

            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// Web Adres formatına yakın değerleri listeleme
        /// </summary>
        /// <returns></returns>
        public List<String> WebAdresListAdapter()
        {
            if (source != null)
            {
                List<string> mailList = source.SelectWebUrls();
                return mailList;
            }
            else
            {
                return null;
            }

        }

    }
}
