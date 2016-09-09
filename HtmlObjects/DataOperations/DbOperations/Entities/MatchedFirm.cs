using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjects.DataOperations.DbOperations.Entities
{
    public class MatchedFirm
    {
        public decimal Id { get; set; }
        public decimal MusteriKodu { get; set; }
        public string Unvan { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string Website { get; set; }
        public string TelMailStatus { get; set; }
    }
}
