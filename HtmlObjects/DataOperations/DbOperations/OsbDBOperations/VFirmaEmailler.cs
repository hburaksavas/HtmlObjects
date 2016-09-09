using HtmlObjects.DataOperations.DbOperations.ConnectOperations;
using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HtmlObjects.DataOperations.DbOperations.OsbDBOperations
{
    public class VFirmaEmailler
    {
        private DataManager manager;

        public VFirmaEmailler()
        {
            SqlServerConnection conn = new SqlServerConnection();
            manager = conn.GetManager();
        }

        /// <summary>
        /// [MusteriKod] [Email]
        /// </summary>
        /// <returns></returns>
        public List<Email> getAllInfos()
        {
            try
            {
                string query = "SELECT * FROM dbo.VFirmaEmailler";

                manager.Close();
                manager.Open();
                DataSet dataSet = manager.GetDataSet(query);
                DataTable dataTable = new DataTable();
               
                DataTableReader reader = dataSet.CreateDataReader();
                dataTable.Load(reader);

                manager.Close();
                var emailList = new List<Email>(dataTable.Rows.Count);
                foreach(DataRow row in dataTable.Rows)
                {
                    var values = row.ItemArray;
                    var email = new Email()
                    {
                        MusteriKod = Convert.ToDecimal( values[0] ),
                        MailAdresi = Convert.ToString( values[1] )
                    };
                    emailList.Add(email);
                }
                return emailList;
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return null;
            }

        }

        /// <summary>
        /// MüşteriKoduna göre mail listesi döner
        /// </summary>
        /// <param name="musteriKod"></param>
        /// <returns></returns>
        public List<Email> GetEmailByMusteriKod(Decimal musteriKod)
        {
            try
            {
                Decimal musteri_kod = Convert.ToDecimal(musteriKod);

                String query = String.Format("SELECT MusteriKod, Email FROM VFirmaEmailler WHERE MusteriKod = {0}", musteri_kod);

                DataSet dataSet = manager.GetDataSet(query);

                List<Email> listName = dataSet.Tables[0].AsEnumerable().Select(m => new Email()
                {
                    MusteriKod = m.Field<decimal>("MusteriKod"),
                    MailAdresi = m.Field<string>("Email")
                }).ToList();

                return listName;
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return null;
            }
        }
    }
}
