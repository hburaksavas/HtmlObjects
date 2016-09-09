
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.CoreOperations
{
    public static class Selector
    {

        /// <summary>
        /// source : html sayfası, geriye innertexti döndürür
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SelectHtmlContent(this string source)
        {
            try
            {
                List<string> listData = source.SelectBodyInnerHtml();
                listData = listData.SelectBodyInnerText();
                source = listData.ClearTextFromInnerTexts();
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }


        /// <summary>
        /// html kaynağından,body içeriğini html tagları ile beraber döndürür
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <returns></returns>
        public static List<String> SelectBodyInnerHtml(this string htmlSource)
        {
            List<String> resultList = new List<String>();
            try
            {

                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(htmlSource);
                HtmlAgilityPack.HtmlNodeCollection nodeCollection = document.DocumentNode.SelectNodes("//body");
                foreach (var item in nodeCollection)
                {

                    String itemHtml = item.InnerHtml.ToString();
                    itemHtml = itemHtml.Trim();

                    if (!String.IsNullOrEmpty(itemHtml))
                    {

                        resultList.Add(itemHtml);

                    }
                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);

            }
            return resultList;
        }

        /// <summary>
        /// body innerHtml'den text içerikleri çeker 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string> SelectBodyInnerText(this List<string> source)
        {

            try
            {
                List<String> resultList = new List<String>();
                Regex regex = new Regex(">(.*?)<", RegexOptions.IgnoreCase);


                foreach (String item in source)
                {
                    foreach (Match match in regex.Matches(item))
                    {
                        resultList.Add(match.Value);
                    }
                }

                return resultList;



            }
            catch (Exception e)
            {

                PrintConsole.LOG(e.StackTrace, e.Message);
                return source;
            }
        }


        /// <summary>
        /// küçüktür ve büyüktür karakterlerini temizler
        /// </summary>
        /// <param name="innerTextList"></param>
        /// <returns></returns>
        public static string ClearTextFromInnerTexts(this List<string> innerTextList)
        {
            string response = "";
            try
            {

                foreach (String line in innerTextList)
                {
                    String lineFirst = line.Replace('<', ' ');
                    String lineSecond = lineFirst.Replace('>', ' ');
                    String lineTrim = lineSecond.Trim();
                    if (!String.IsNullOrEmpty(lineTrim))
                    {
                        response += lineTrim + " ";
                    }
                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return response;
        }

        /// <summary>
        /// Source içerisinde soldan sağa tüm rakamları birleştiren metot
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SelectNumbers(this string source)
        {
           
            string numbers = "";

            try
            {
                source = source.Trim();
                #region source içerisindeki rakamları soldan sağa doğru okuyarak birleştirir
                foreach (Match match in Regex.Matches(source, "[0-9]{1}"))
                {

                    if (match.Length == 1)
                    {
                        string substr = source.Substring(match.Index, match.Length);
                        numbers += substr;

                    }
                }
                #endregion

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

            return numbers;
        }


        /// <summary>
        /// source içerisinde mailleri listeleyen metot
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string> SelectMails(this string source)
        {
            // Regex regex = new Regex(" [a-z]*(([0-9]*)|[-_]|[üöçş]).([a-z]*|([0-9]*))@[a-z]*([.]|[-]|[üöçş]).[a-z]*.((tr)|(com)|(net)|(info)", RegexOptions.IgnoreCase); mailleri 1 2 ıskayla buluyor

            List<string> mailList = new List<string>();
            try
            {
                #region replacing
                try
                {
                    source = Regex.Replace(source, "-", "txrxe", RegexOptions.IgnoreCase);
                }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
                catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                {

                }
                try
                {
                    source = Regex.Replace(source, "_", "axlxt", RegexOptions.IgnoreCase);
                }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
                catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                {

                }
                #endregion

                source = source.RemoveWebSiteWords();


                Regex regex = new Regex(" [a-z]*(([0-9]*)|[üöçş]).([a-z]*|([0-9]*))@(.*?)((tr)|(com)|(net)|(org))", RegexOptions.IgnoreCase);
                foreach (Match match in regex.Matches(source))
                {

                    if (match.Value.Count() > 5)
                    {
                        string x = match.Value;

                        #region replaceBack
                        try
                        {
                            x = Regex.Replace(x, "txrxe", "-", RegexOptions.IgnoreCase);

                        }
                        catch (Exception e)
                        {
                            PrintConsole.LOG(e.StackTrace, e.Message);
                        }
                        try
                        {
                            x = Regex.Replace(x, "axlxt", "_", RegexOptions.IgnoreCase);
                        }
                        catch (Exception e)
                        {
                            PrintConsole.LOG(e.StackTrace, e.Message);
                        }
                        #endregion
                        x = Regex.Replace(x, "[0-9]{2} ", "");
                        mailList.Add(x);
                    }

                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

            return mailList;
        }


        /// <summary>
        /// source içerisindeki web adreslerini listeleyen metot
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string> SelectWebUrls(this string source)
        {

            List<string> webAdresList = new List<string>();
            #region replacing
            try
            {
                source = Regex.Replace(source, "-", "txrxe", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "_", "axlxt", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            #endregion
            try
            {

                Regex regex = new Regex("[w]{3}.([a-z]*|[0-9]*).[a-z]{3}.((tr)|(com)|(org)|(net))", RegexOptions.IgnoreCase);

                foreach (Match match in regex.Matches(source))
                {
                    if (match.Value.Count() > 8)
                    {
                        String x = match.Value;
                        #region replaceBack
                        try
                        {
                            x = Regex.Replace(x, "txrxe", "-", RegexOptions.IgnoreCase);

                        }
                        catch (Exception e)
                        {
                            PrintConsole.LOG(e.StackTrace, e.Message);
                        }
                        try
                        {
                            x = Regex.Replace(x, "axlxt", "_", RegexOptions.IgnoreCase);
                        }
                        catch (Exception e)
                        {
                            PrintConsole.LOG(e.StackTrace, e.Message);
                        }
                        #endregion
                        webAdresList.Add(x);
                    }
                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return webAdresList;
        }

        /// <summary>
        /// Source içerisindeki tel ve faks noları çekiyor
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string> SelectPhoneNumbers(this string source)
        {
            List<string> resultList = new List<string>();
            try
            {
                Regex regex = new Regex("[1-9][1-9][1-9].([0-9]{2,3}).([0-9]{2,3}).([0-9]{1,2})", RegexOptions.IgnoreCase);

                #region remove operations
                source = source.RemoveCountryPhoneCode();
                // source = source.RemoveLikeFaxWords();
                source = source.RemoveMailWords();

                source = source.RemoveWebWord();
                source = source.RemoveDefinitionWords();
                source = source.RemoveNoNeedCharacters();
                source = source.RemoveNoMeaningChars();
                #endregion

                string numbers = source.SelectNumbers();
                foreach (Match match in regex.Matches(source))
                {
                    resultList.Add(match.Value);
                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

            return resultList;
        }


        /// <summary>
        /// Return fax...no
        /// </summary>"(((fax)|(faks)))(.*?)[1-9][1-9][1-9].([0-9]{2,3}).([0-9]{2,3}).([0-9]{1,2})"
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string> SelectFaxNumbers(this string source)
        {
            List<string> resultList = new List<string>();
            try
            {
                Regex regex = new Regex("((fax)|(faks))(.*?)[1-9][1-9][1-9].([0-9]{2,3}).([0-9]{2,3}).([0-9]{1,2})", RegexOptions.IgnoreCase);
                source = source.ToLower();
                #region remove operations
                source = source.RemoveCountryPhoneCode();
                source = source.RemoveMailWords();
                source = source.RemoveWebWord();
                source = source.RemoveDefinitionWords();
                source = source.RemoveNoNeedCharacters();
                source = source.RemoveNoMeaningChars();
                #endregion

                foreach (Match match in regex.Matches(source))
                {
                    string item = match.Value;
                    item = Regex.Replace(item, "(fax.)|(faks.)", "", RegexOptions.IgnoreCase);
                    if (item.Length < 21)
                    {
                        item = Regex.Replace(item, ":", "");
                        resultList.Add(item);
                    }

                }

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

            return resultList;
        }
    }
}
