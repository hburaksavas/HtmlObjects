using HtmlObjects.DataOperations.DbOperations.ConnectOperations;
using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HtmlObjects.DataOperations.DbOperations.OsbDBOperations
{
    public class VFirmaTelefonlar
    {
        private ConnectOperations.DataManager manager;
        public VFirmaTelefonlar()
        {
            manager = new SqlServerConnection().GetManager();
        }
        

        /// <summary>
        /// [Musteri_kod] [ModTel]
        /// </summary>
        /// <returns></returns>
        public List<TelNo> getAllInfos()
        {
            try
            {
                string query = "SELECT * FROM dbo.VTelefonlarYeni";
                manager.Open();
                DataSet dataSet = manager.GetDataSet(query);
                manager.Close();

                DataTable dataTable = new DataTable();

                DataTableReader reader = dataSet.CreateDataReader();
                dataTable.Load(reader);

                var telNoList = new List<TelNo>(dataTable.Rows.Count);
                foreach (DataRow row in dataTable.Rows)
                {
                    var values = row.ItemArray;
                    var telNo = new TelNo()
                    {
                        MusteriKod = Convert.ToDecimal(values[0]),
                        TelefonNumarasi = Convert.ToString(values[1])
                    };
                    telNoList.Add(telNo);
                }
                return telNoList;
            } catch(Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return null;
            }

        }

      
        /// <summary>
        /// MusteriKoduna göre telefon numaraları listesi
        /// </summary>
        /// <param name="MusteriKod"></param>
        /// <returns></returns>
        public List<TelNo> GetTelNoByMusteriKod(Decimal MusteriKod)
        {
            try
            {
                String query = String.Format("SELECT Musteri_kod, ModTel FROM VTelefonlarYeni WHERE Musteri_kod = {0}", MusteriKod);

                DataSet dataSet = manager.GetDataSet(query);
             
                List<TelNo> listName = dataSet.Tables[0].AsEnumerable().Select(m => new TelNo()
                {
                    MusteriKod = m.Field<Decimal>("Musteri_kod"),
                    TelefonNumarasi = m.Field<string>("ModTel")
                }).ToList();

                return listName;
            }
            catch(Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return null;
            }

        }


        public string GetMusteriKod(string TelNo)
        {
            try
            {
                String query = String.Format("SELECT Musteri_kod FROM VTelefonlarYeni WHERE ModTel = '{0}'", TelNo);

                DataSet dataSet = manager.GetDataSet(query);

                object value = dataSet.Tables[0].Rows[0][0];

                return Convert.ToString(value);

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return String.Empty;
            }

        }
    }
}
