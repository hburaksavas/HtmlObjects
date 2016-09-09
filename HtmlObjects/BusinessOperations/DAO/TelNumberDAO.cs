using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HtmlObjects.BusinessOperations.DAO
{
    public class TelNumberDAO
    {
        private DataOperations.DbOperations.OsbDBOperations.VFirmaTelefonlar vTelConnection;


        public TelNumberDAO()
        {
            vTelConnection = new DataOperations.DbOperations.OsbDBOperations.VFirmaTelefonlar();

        }

        public List<TelNo> GetTelNumberList()
        {
            Console.WriteLine("Veritabanından telefon listesi çekiliyor");

           
            return vTelConnection.getAllInfos();

        }
        public List<string> GetOnlyTelNumberList()
        {

            List<TelNo> dataSet = vTelConnection.getAllInfos();
          
            List<string> list = dataSet.AsEnumerable().Select(r =>("ModTel")).ToList();

            return list;
        }

        public List<TelNo> GetTelNumbersByMusteriKod(Decimal MusteriKod)
        {
            if ( MusteriKod != -1)
            {
                return vTelConnection.GetTelNoByMusteriKod(MusteriKod);

            }else
            {
                return null;
            }
        }
    }
}
