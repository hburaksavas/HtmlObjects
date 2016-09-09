using HtmlObjects.BusinessOperations.CoreOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.MappingOperations
{
    public class FirmNameMapping
    {
        private String HtmlTag;

        private String Source;

        public FirmNameMapping(String source,String HtmlTag)
        {
            this.HtmlTag = HtmlTag; //firma isimlerinin aranacağı html tagı
            this.Source = source; //body içeriği html tagları ile beraber
        }

        public List<String> GetNameList()
        {
            List<string> FirmNameList = new List<string>();

            if (!String.IsNullOrEmpty(HtmlTag)) {

                HtmlTag = HtmlTag.Trim();
                HtmlTag = HtmlTag.ToLower();

                Source =  Source.Trim();
                Source = Source.ToLower();

                String pattern = "<.?.?" + HtmlTag + "(.*?)>(.*?)</"+HtmlTag+"(.*?)>";

                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

               

                foreach(Match match in regex.Matches(Source))
                {
                    FirmNameList.Add(match.Value);
                  

                }

                FirmNameList = EditFirmNameList(FirmNameList);
                FirmNameList = Remove_If_Its_Not_FirmName(FirmNameList);
                FirmNameList = ValidationFirmNames(FirmNameList);
            }
            return FirmNameList;
        }

        /// <summary>
        /// Html Taglarından Kurtarma Metodu
        /// </summary>
        /// <param name="nameList"></param>
        /// <returns></returns>
        private List<string> EditFirmNameList(List<string> nameList)
        {
            List<string> editList = nameList.SelectBodyInnerText();
            List<string> resultList = new List<string>();
            foreach (String line in editList)
            {
                String lineFirst = line.Replace('<', ' ');
                String lineSecond = lineFirst.Replace('>', ' ');
                String lineTrim = lineSecond.Trim();
                if (!String.IsNullOrEmpty(lineTrim))
                {
                    resultList.Add(lineTrim);
                }
            }
            return resultList;
        }

        /// <summary>
        /// Html Tagları içerisinde firma ismi olmayabilir
        /// Anasayfa gibi kelimeleri, firma isimleri listesinden kaldırmak için
        /// çağırılan metot
        /// </summary>
        /// <param name="nameList"></param>
        /// <returns></returns>
        private List<string> Remove_If_Its_Not_FirmName(List<string> nameList)
        {
            List<string> resultList = new List<string>();

            foreach(var item in nameList)
            {
                String name = item;
                name = name.RemoveALLMeaningless();
                name = name.Trim();
                if (!String.IsNullOrEmpty(name))
                {
                    if (name.Length > 6) {

                        resultList.Add(name);

                    }


                }
            }

            return resultList;
        }

        private List<string> ValidationFirmNames(List<string> nameList)
        {
            List<string> resultList = new List<string>();

            IEnumerable<string> numerableList = nameList.Distinct();

            foreach(var item in numerableList)
            {
                resultList.Add(item);
            }

            return resultList;
        }

        /// <summary>
        /// Firma isimlerini taramadan önce kullanılır
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String RemoveForGettingFirmNames(String text)
        {

            text = text.ToLower();
            text = text.RemoveALLMeaningless();

            string[] patterns = new string[] {
                        "[0-9]",
                        ":",
                         "-",
                         "(sayaç)(.*?)(çıkar)",
                         "kolkılıç.adil.hıdıroğlu",
                         "başakşehir(.*?)agency",
                         @" \. ",
                         "(kısıklı)|(levent)|(beşiktaş)|(üsküdar)"
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
    }

}
