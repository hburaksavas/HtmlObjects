using HtmlObjects.BusinessOperations.CoreOperations;
using HtmlObjects.BusinessOperations.MappingOperations.FirmFields;
using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.MappingOperations
{
    class FaxMapping
    {

        private String MappingFaxSource;


        private List<string> FaxList;

        private List<Firm> resultFirmList;

        private List<Fax> matchedFaxList;



        public FaxMapping(String source)
        {
            MappingFaxSource = source;
            MappingFaxSource = RemoveForMappingFax(MappingFaxSource);
            FaxList = FaxListAdapter(MappingFaxSource);
        }

        public FaxMapping(List<string> faxList, String source)
        {
            MappingFaxSource = source;
            MappingFaxSource = RemoveForMappingFax(MappingFaxSource);
            FaxList = faxList;
        }

        public FaxMapping(String source,List<Firm> FirmList)
        {
            MappingFaxSource = source;
            MappingFaxSource = RemoveForMappingFax(MappingFaxSource);
            foreach(var item in FirmList)
            {
                MappingFaxSource = MappingFaxSource.Replace(item.firmPhone, "");
            }
            FaxList = FaxListAdapter(MappingFaxSource);           
            resultFirmList = FirmList;
        }





        /// <summary>
        /// Global değişken MappingFaxSource içerisinde arama yapar,eşleşiyorsa gelen firm nesnesine faxnoyu ekler ve return eder,
        /// ayrıca MappingFaxSource içerisinde eşleşen değeri siler
        /// ve FaxList içerisinden de eşleşen fax numarasını siler
        /// </summary>
        /// <param name="firm"></param>
        /// <param name="Faxes"></param>
        /// <returns></returns>
        public Firm mappingFaxNo(Firm firm)
        {
            List<string> Faxes = FaxList;
            String firmName = firm.firmName;
            Regex regex;
            String pattern;
            Match match;
            if (firmName.Length > 0)
            {
                foreach (var faxNo in Faxes)
                {
                    pattern = firmName + "(.*?)" + faxNo;
                    regex = new Regex(pattern);
                    match = regex.Match(MappingFaxSource);
                    if (match.Success)
                    {
                        if (match.Value.Length > 30)
                        {
                            if (match.Length < 250)
                            {
                                firm.firmFax = faxNo; // gelen firm nesnesinin fimFax alanı set edilip geri döndürülecek
                                MappingFaxSource = MappingFaxSource.Replace(match.Value, "");
                                FaxList.Remove(faxNo);
                            }
                        }
                        break;
                    }
                }
            }


            return firm;
        }

        public List<Firm> mappingFaxNo()
        {
            List<Firm> copyList = resultFirmList;
            String SourceCopy = MappingFaxSource;
            List<Fax> matchedFaxList_LR = getMatchedFaxesLR(copyList, SourceCopy);
            List<Fax> matchedFaxList_RL = getMatchedFaxesRL(copyList, SourceCopy);

            matchedFaxList = matchedFaxList_LR;
            foreach (var item in matchedFaxList_RL)
            {
                matchedFaxList.Add(item);
            }

            List<Fax> distinctFaxList = GetDistinctFaxList();

            List<Firm> resultList = new List<Firm>();

            foreach (var firm in resultFirmList)
            {
                Firm newFirm = firm;


                foreach (var item in distinctFaxList)
                {

                    if (item.matchedFirmName.Equals(newFirm.firmName))
                    {
                        newFirm.firmFax = item.matchedFaxNo;
                    }

                }

                resultList.Add(newFirm);
            }

            return resultList;
        }





        private List<Fax> GetDistinctFaxList()
        {
            List<string> faxNoKeyList = new List<string>();
            List<Fax> distinctFaxList = new List<Fax>(); // distinct fax list
            try
            {
                foreach (var item in matchedFaxList)
                {
                    faxNoKeyList.Add(item.matchedFaxNo);
                }

                IEnumerable<string> distinctFaxNoKeyList = faxNoKeyList.Distinct(); // fax no tutulan liste
                List<int> matchLengthList = new List<int>(); // eşleşme uzunluklarının tutulduğu liste

                foreach (var item in faxNoKeyList)
                {

                    foreach (var faxObj in matchedFaxList)
                    {

                        if (faxObj.matchedFaxNo.Equals(item))
                        {
                            matchLengthList.Add(faxObj.matchedLength);
                            
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

                    foreach (var faxObj in matchedFaxList)
                    {

                        if (faxObj.matchedLength.Equals(minLength) && faxObj.matchedFaxNo.Equals(item))
                        {
                            Fax faxValue = new Fax();
                            faxValue.matchedFirmName = faxObj.matchedFirmName;
                            faxValue.matchedFaxNo = item;
                            distinctFaxList.Add(faxValue);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

            return distinctFaxList;
        }




        private List<Fax> getMatchedFaxesLR(List<Firm> FirmList,String Source)
        {
            String sourceCopy = Source;
            List<Fax> resultFaxList = new List<Fax>();
            List<string> Faxes = FaxList;
            foreach(var item in FirmList)
            {
                Firm firm = item;

                if(!String.IsNullOrEmpty(firm.firmName))
                {
                    foreach(var faxNo in Faxes)
                    {
                        String pattern = firm.firmName + "(.*?)" + faxNo;
                        Regex regex = new Regex(pattern);
                        Match match = regex.Match(sourceCopy);
                        if (match.Success)
                        {
                            if (match.Value.Length > 30)
                            {
                                if (match.Length < 180)
                                {
                                    Fax fax = new Fax();
                                    fax.matchedFaxNo = faxNo;
                                    fax.matchedFirmName = firm.firmName;
                                    fax.matchedLength = match.Length;
                                    resultFaxList.Add(fax);
                                    sourceCopy = sourceCopy.Replace(match.Value, "");                                    
                                }
                            }
                        }
                    }
                }

            }
            return resultFaxList;
        }

        private List<Fax> getMatchedFaxesRL(List<Firm> FirmList,String Source)
        {
            String sourceCopy = Source;
            List<Fax> resultFaxList = new List<Fax>();
            List<string> Faxes = FaxList;

            int firmCount = FirmList.Count;
            for (int i=firmCount-1; i>=0;i--)
            {
                Firm firm = FirmList[i];

                if (!String.IsNullOrEmpty(firm.firmName))
                {
                    foreach (var faxNo in Faxes)
                    {
                        String pattern = firm.firmName + "(.*?)" + faxNo;
                        Regex regex = new Regex(pattern);
                        Match match = regex.Match(sourceCopy);
                        if (match.Success)
                        {
                            if (match.Value.Length > 30)
                            {
                                if (match.Length < 180)
                                {
                                    Fax fax = new Fax();
                                    fax.matchedFaxNo = faxNo;
                                    fax.matchedFirmName = firm.firmName;
                                    fax.matchedLength = match.Length;
                                    resultFaxList.Add(fax);
                                    sourceCopy = sourceCopy.Replace(match.Value, "");
                                }
                            }
                        }
                    }
                }

            }
            return resultFaxList;
        }




        private List<String> FaxListAdapter(String text)
        {
            if (text != null)
            {
                List<string> faxList = text.SelectPhoneNumbers(); //SelectFax yerine kullanılmasının sebebi eşleşen tel no'ların source içerisinden çıkarılmış olması
                return faxList;
            }
            else
            {
                return null;
            }

        }

        private String RemoveForMappingFax(String text)
        {
            text = text.ToLower().RemoveForAllMappingOperations();
            string[] patterns = new string[]
          {
                "-",
                "www(.*?).com",
                ":",
                "[ ]{2,5}",
                "'",
                " [0-9] ",
                "[a-z]*@[a-z]*",
                @" \. ",
                @" \.tr",
                " cd.",
                " no",
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
    }
}
