using System;
using System.ComponentModel;

namespace HtmlObjects.BusinessOperations.POCO
{
    public class Firm
    {
        [DisplayName("Firma İsmi")]
        public String firmName { get; set; }


        [DisplayName("Telefon Numarası")]
        public String firmPhone { get; set; }


        [DisplayName("Fax Numarası")]
        public string firmFax { get; set; }


        [DisplayName("Mail Adresi")]
        public String firmMail { get; set; }


        [DisplayName("Web Sitesi")]
        public string firmWebSite { get; set; }
    }
}
