using HtmlObjects.BusinessOperations.CoreOperations;
using HtmlObjects.BusinessOperations.MappingOperations.FirmFields;
using HtmlObjects.BusinessOperations.POCO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.MappingOperations
{
    class PhoneMapping
    {
        

        private List<string> PhoneList = new List<string>(); // telefonların bulunduğu liste


        public List<Firm> mappingPhoneNo(List<String> Names,String Source)
        {
            Firm firm = null;

            List<Firm> firmList = new List<Firm>();

            List<Phone> lrPhoneList = mappingLeftToRightPhoneNo(Names, Source);
            List<Phone> rlPhoneList = mappingRightToLeftPhoneNo(Names, Source);
            if(lrPhoneList.Count == rlPhoneList.Count)
            {

                foreach (var item1 in lrPhoneList)
                {
                    foreach( var item2 in rlPhoneList)
                    {
                        if (item1.phoneKey.Equals(item2.phoneKey))
                        {
                            firm = new Firm();
                            if (item1.matchLength <= item2.matchLength)
                            {
                               
                                firm.firmName = item1.firmName;
                                firm.firmPhone = item1.phoneKey;
                                firmList.Add(firm);
                            }else
                            {
                                firm.firmName = item2.firmName;
                                firm.firmPhone = item2.phoneKey;
                                firmList.Add(firm);
                            }
                        }
                    }
                }

            }else if(lrPhoneList.Count > rlPhoneList.Count)
            {
                foreach (var item1 in lrPhoneList)
                {
                    firm = new Firm();

                    firm.firmName = item1.firmName;
                    firm.firmPhone = item1.phoneKey;

                    foreach (var item2 in rlPhoneList)
                    {
                        if (item1.phoneKey.Equals(item2.phoneKey))
                        {


                            if (item1.matchLength > item2.matchLength)
                            {

                                firm.firmName = item2.firmName;
                                firm.firmPhone = item2.phoneKey;
                                
                            }
                            else if (String.IsNullOrEmpty(firm.firmPhone))
                            {
                                firm.firmName = item2.firmName;
                                firm.firmPhone = item2.phoneKey;
                            }

                        }
                    }

                    firmList.Add(firm);
                }
            }else
            {
                foreach (var item1 in rlPhoneList)
                {
                    firm = new Firm();

                    firm.firmName = item1.firmName;
                    firm.firmPhone = item1.phoneKey;

                    foreach (var item2 in lrPhoneList)
                    {
                        if (item1.phoneKey.Equals(item2.phoneKey))
                        {
       

                            if (item1.matchLength > item2.matchLength)
                            {
                              
                                firm.firmName = item2.firmName;
                                firm.firmPhone = item2.phoneKey;
                                                                
                            }
                            else if (String.IsNullOrEmpty(firm.firmPhone))
                            {
                                firm.firmName = item2.firmName;
                                firm.firmPhone = item2.phoneKey;
                            }
                            
                        }
                    }
                    firmList.Add(firm);
                }
            }
            return firmList;

        }



        private List<Phone> mappingLeftToRightPhoneNo(List<String> Names,String Source)
        {
            String phoneSource = Source;

            phoneSource = RemoveForMappingPhone(phoneSource);

            Phone LR_phone = null;

            List<Phone> LRphoneList = new List<Phone>();

            List<string> strPhoneListLR = PhoneListAdapter(phoneSource);
            

            List<string> strPhones = strPhoneListLR; // telefon listesi kopyalanıyor çünkü içeride,kendi üzerinde değişiklik yapılması için

            int NamesCount = Names.Count;
            for (int i= 0;i<NamesCount;i++)
            {
                foreach (var phone in strPhones)
                {
                    String xname = Names[i].Trim();
                    String xphone = phone.Trim();
                    String pattern = "(" + xname + ")(.*?)(" + xphone + ")";

                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(phoneSource);
                    if (match.Success)
                    {

                        if (match.Length < 121)
                        {
                            LR_phone = new Phone();
                            LR_phone.firmName = xname;
                            LR_phone.matchLength = match.Length;
                            LR_phone.phoneKey = xphone;
                            LRphoneList.Add(LR_phone);
                            phoneSource = phoneSource.Replace(match.Value, "");


                            break;

                        }
                    }
                }
            }

            return LRphoneList;
        }
        private List<Phone> mappingRightToLeftPhoneNo(List<String>Names,String Source)
        {
            String phoneSource = Source;
            phoneSource = RemoveForMappingPhone(phoneSource);

            Phone RL_phone = null;

            List<Phone> RLphoneList = new List<Phone>();

            List<string> strPhoneListRL = PhoneListAdapter(phoneSource);
            List<string> strPhones = strPhoneListRL; // telefon listesi kopyalanıyor çünkü içeride,kendi üzerinde değişiklik yapılması için

            int NamesCount = Names.Count;
            for (int i = NamesCount-1; i >= 0; i--)
            {
                foreach (var phone in strPhones)
                {
                    String xname = Names[i].Trim();
                    String xphone = phone.Trim();
                    String pattern = "(" + xname + ")(.*?)(" + xphone + ")";

                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(phoneSource);
                    if (match.Success)
                    {

                        if (match.Length < 120)
                        {
                            RL_phone = new Phone();
                            RL_phone.firmName = xname;
                            RL_phone.matchLength = match.Length;
                            RL_phone.phoneKey = xphone;
                            RLphoneList.Add(RL_phone);
                            phoneSource = phoneSource.Replace(match.Value, "");
                            break;

                        }
                    }
                }
            }

            return RLphoneList;
        }



        /// <summary>
        /// Tel numaralarını eşleştirmeden önce source'u hazırlar
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String RemoveForMappingPhone(String text)
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
                " cd.",
                " no","(acil telefonlar)","itfaiye.?[0-9]* ","emniyet.?.?.?.?.?[0-9]* ","elektrik arıza.?.?.?.?.?[0-9]* ",
                 "su arıza.?.?.?.?.?[0-9]* ","(doğalgaz arıza).?.?.?.?.?.?[0-9]*"
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

        private List<String> PhoneListAdapter(String text)
        {
            List<String> phoneList = text.SelectPhoneNumbers();
            List<String> faxList = text.SelectFaxNumbers();

            for (int i = 0; i < faxList.Count; i++)
            {
                faxList[i] = faxList[i].RemoveLikeFaxWords(); //faks türevleri siliniyor
                faxList[i] = faxList[i].Replace(":", "");

                List<string> faxnumbers = faxList[i].SelectPhoneNumbers();
                if (faxnumbers != null && faxnumbers.Count > 0)
                    faxList[i] = faxnumbers[0]; //faks da telefon numarası ile aynı formatta olduğu için,dönen ilk list itemini faxliste eşitliyoruz

            }

            int phoneListCount = phoneList.Count; //phoneList Count hesabı

            for (int i = 0; i < phoneListCount; i++)
            {
                foreach (var it in faxList)
                {
                    if (phoneList[i].Equals(it)) // Eğer fax listesindeki kayıt telefon listesinde de varsa,telefon listesinde boşa çeviriliyor
                    {
                        phoneList[i] = "";
                    }
                }
            }

            List<string> resultList = new List<string>();

            for (int i = 0; i < phoneListCount; i++)
            {

                Regex reg = new Regex("[a-z]{1}", RegexOptions.IgnoreCase); //Tel no'lar içerisinde harf var mı kontrolü için
                String item = phoneList[i];
                int count = 0;

                foreach (Match match in reg.Matches(item))
                {
                    count++; //Eşleşme var mı
                }

                if (count > 1) //Varsa
                {
                    phoneList[i] = ""; //Bu tel no, istenen formatta değil,sıfırla
                }

                if (!phoneList[i].Equals("")) //Tel no: boş,dolu kontrolü
                {
                    resultList.Add(phoneList[i]); //Doluysa geri döndürülecek listeye ekle
                }
            }
            if (resultList.Count == 0)
            {// result list count 0 sa, verilen text içerisinde ya fax no'su , ya da tel no'su yoktur..
                return text.SelectPhoneNumbers(); 
            }
            else
            {
                return resultList; // Tam tersi durumda return resultList
            }


        }
    }
}
