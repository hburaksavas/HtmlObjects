using System;
using System.Text.RegularExpressions;

namespace HtmlObjects.BusinessOperations.CoreOperations
{
    public static class Remover
    {
        /// <summary>
        /// Firma isimlerini taramadan önce çağırılır
        /// </summary>
        /// <param name="SourceText"></param>
        /// <returns></returns>
        public static String RemoveALLMeaningless(this String SourceText)
        {
            string[] patterns = new string[]
            {
                "hizmetler"," hizmetler ve","gerekli bilgiler","anasayfa","firma rehberi","iletişim bilgileri",
                "(yetkili).?.?.?[a-z]*.[a-z]* ","geri dön","yönetim kurulu","guvenlik",
                @"(\.tr)",
                "www(.*?).com",
                ":",
                "...com",
                "[0-9]",
                "-",
                " www.[a-z]*. ",
                "( üye ol)|( üye giri[sş]i.)",
                " tüm haklar[ıi] saklıdır",
                " kullanım koşulları",
                " organize sanayi bölgesi",
                "( haberler)|( duyurular)",
                " kullanım koşulları",
                " gizlilik sözleşmesi",
                "tıklayınız",
                "( itfaiye)|( elektrik arıza)|( su arıza)|( doğalgaz arıza)|( tıklayın)",
                "(foto[gğ]raf)|(türkiye)|(turkey)",
                "(ana.sayfa)|(linkler)|(firmalar)",
                "/",
                "[()]",
                "( fax.)|( faks.)|(pbx)",
                @"\+",
                "[a-z]*@[a-z]*",
                "copyright(.*?)",
                @"(harita)|(bilgisi)|(için)|(tıklayınız)|(\.\.\.)",
                "adres",
                "(e.posta)|(e.mail)|( mail)",
                "[a-z]*./.[a-z]*",
                "(tel.)|(telefon.)|(yetkili)",
                "( web)|( site)|( web.sitesi)",
                "&[a-z]*;",
                "&",
                " h ",
                " cad.",
                "( no )|( _no ) ",
                "bulv.no",
                "tembelova(.*?)gençlik",
                " gebze",
                ".org.san.böl",
                "bulv",
                "blv",
                "böl.",
                "iyikesici",
                "(başpınar)|(g.antep)",
                "gazi.antep",
                "koyuncu",
                "dede","plaza kat","kadıköy","tuzla"," mah.atatürk","mahalle","cadde"," cd. ","c barbaros","şahabettin bilgi"," cad. ",
                "tembelova alanı","şevket ersoy ahmet ulvi ersoy",
                "kurumsal","hakkımızda","vizyon","misyon","idari birimler","izin|ruhsat",
                "(üye ol)|(üye giri[sş]i.)",
                "tüm haklar[ıi] saklıdır",
                "kullanım koşulları",
                "organize sanayi bölgesi",
                "(haberler)|(duyurular)",
                "kullanım koşulları",
                "gizlilik sözleşmesi",
                "tıklayınız",
                "(itfaiye)|(elektrik arıza)|(su arıza)|(doğalgaz arıza)|(tıklayın)",
                "(galeri)|(foto[gğ]raf)|(türkiye)|(turkey)",
                "(ana.sayfa)|(linkler)|(firmalar)",
                "/",
                "[()]",
                @"\+",
                "copyright(.*?)","lisanssız(.*?)tablosu",
                "ana.sayfa",


            };

            foreach (var pattern in patterns)
            {
                try
                {
                    SourceText = Regex.Replace(SourceText, pattern, " ");
                }
                catch (Exception e)
                {
                    PrintConsole.LOG(e.StackTrace, e.Message);
                }

            }


            return SourceText;
        }


        public static String RemoveForAllMappingOperations(this String text)
        {
            string[] patterns = new string[]
         {
                "kurumsal","hakkımızda","vizyon","misyon","idari birimler","izin|ruhsat","tuzla",
                 "dede","plaza kat","kadıköy"," mah.atatürk","mahalle","cadde"," cd. ","c barbaros","şahabettin bilgi"," cad. ",
                "(üye ol)|(üye giri[sş]i.)","(itfaiye)|(elektrik arıza)|(su arıza)|(doğalgaz arıza)|(tıklayın)",
                "tüm haklar[ıi] saklıdır","tembelova alanı","şevket ersoy ahmet ulvi ersoy",
                "kullanım koşulları",
                "organize sanayi bölgesi",
                "(haberler)|(duyurular)",
                "kullanım koşulları",
                "gizlilik sözleşmesi",
                "tıklayın..",
                "(galeri)|(foto[gğ]raf)|(türkiye)|(turkey)",
                "(ana.sayfa)|(linkler)|(firmalar)",
                "/",
                "[()]",
                @"\+",
                "copyright(.*?)",
                @"(harita)|(bilgisi)|(için)|(tıklayınız)|(\.\.\.)",
                "adres",
                "(e.posta)|(e.mail)|( mail)",
                "[a-z]*./.[a-z]*",
                "( telefon.)|( tel.)|( yetkili)",
                "(web)|(site)|(web.sitesi)",
                "&(.*?);",
                "&",
                " h ",
                "cad.",
                " ( no)|(_no) ",
                "tekilova alanı gençlik",
                "(gebze)",
                "(sosyal medya)(.*?)(maıl)",
                ".org.san.böl",
                "(bulv)|(blv)",
                "iyikesici",
                "(başpınar)|(g.antep)",
                "gazi.antep",
                " [0-9] ",

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


        /// <summary>
        /// clear wrods like websitesi,website
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveWebWord(this string source)
        {
            try
            {
                source = Regex.Replace(source, "web[a-z]*((si)|.)", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }


        /// <summary>
        /// remove words like e-mail,email,eposta,e-posta
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveMailWords(this string source)
        {
            try
            {
                source = Regex.Replace(source, "e.posta", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "e.mail", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }


        /// <summary>
        /// Remove web site words like anasayfa,tıklayınız vs
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveWebSiteWords(this string source)
        {
            try
            {
                source = Regex.Replace(source, "(harita)|(bilgisi)|(bilgileri)|(i[cç]in)|(tıklayınız)", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "(aboneleri)|(acil)|(numaraları)|(su)|(do[gğ]al.gaz)", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "(english)|(firmalar)|(arama)|(katılımcı)", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }

            try
            {
                source = Regex.Replace(source, "(üretimdeki)|(kiracı)|(yabancı)|(sermayeli)", "", RegexOptions.IgnoreCase);

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "( mail.)|(mailt..)", " ", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "(ilk)(.*?)(sözleşmesi)", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, @"(\.\.\.)", "");
                source = Regex.Replace(source.ToLower(), " adres(.*?)[a-z]*/[a-z].[a-z]* ", "", RegexOptions.IgnoreCase);
                source = Regex.Replace(source, "gebze(.*?)-", " ");
                source = Regex.Replace(source, "ar agco", "agco ");
                source = Regex.Replace(source, " h ", " ");
                string SourceText = source;
                SourceText = Regex.Replace(SourceText, " sokak(.*?)kocaeli ", " ");
                SourceText = Regex.Replace(SourceText, " cad(.*?)kocaeli ", " ");
                SourceText = Regex.Replace(SourceText, " gosb(.*?)agency", " ");
                SourceText = Regex.Replace(SourceText, " pbx", " ");
                SourceText = Regex.Replace(SourceText, "site sı", " ");
                SourceText = Regex.Replace(SourceText, "http///", "");
                SourceText = Regex.Replace(SourceText, "[a-z]*/[a-z]", "");
                SourceText = Regex.Replace(SourceText, "/", "");
                SourceText = Regex.Replace(SourceText, "anasayfa(.*?)osb sı", " ");
                SourceText = Regex.Replace(SourceText, "sosyal(.*?)rehberi", " ");
                SourceText = Regex.Replace(SourceText, "copy(.*?)saklıdır", " ");
                SourceText = Regex.Replace(SourceText, "reklam", " ");
                SourceText = Regex.Replace(SourceText, "tasarım(.*?)teknoloji", " ");
                SourceText = Regex.Replace(SourceText, "ana sayfa(.*?)listesi adı", " ");
                SourceText = Regex.Replace(SourceText, "ana sayfa(.*?)üye listesi", " ");
                SourceText = Regex.Replace(SourceText, "_no|no", " ");
                source = SourceText;
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "ara(.*?)euro [0-9]*", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "firmalar(.*?)firmalar ", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "firma((sı)|.)", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }


        /// <summary>
        /// remove label words like Tel,Telefon,Adres
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveDefinitionWords(this string source)
        {
            try
            {
                source = Regex.Replace(source, "((adres)|(address))(.*?)((telefon)|(tel))", "   ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = source.Replace("no", "");
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }


        /// <summary>
        /// Remove characters like ' ( ) 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveNoNeedCharacters(this string source)
        {
            try
            {
                source = Regex.Replace(source, "'", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "[()]", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }


        /// <summary>
        /// remove fax or faks word
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveLikeFaxWords(this string source)
        {
            try
            {
                source = Regex.Replace(source, "((fax.)|(faks.))", "_", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message); ;
            }
            return source;
        }


        /// <summary>
        /// Remove Turkey countrycode +90
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveCountryPhoneCode(this string source)
        {
            try
            {
                source = Regex.Replace(source, "[+].[9][0].", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }

        /// <summary>
        /// Remove like &nbsp; codes and tabs
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns> 
        public static string RemoveNoMeaningChars(this string source)
        {
            try
            {
                source = Regex.Replace(source, "&nbsp;", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "&quuo;", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "&", "", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "      ", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = Regex.Replace(source, "   ", " ", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            try
            {
                source = source.Replace("\t", " ");
                while (source.IndexOf("  ") > 0)
                {
                    source = source.Replace("  ", " ");
                }
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            return source;
        }
    }
}
