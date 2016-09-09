using HtmlObjects.DataOperations.DbOperations.ConnectOperations;
using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace HtmlObjects.DataOperations.DbOperations.OsbDBOperations
{
    public class MatchedFirmOperations : IFirmOperation<MatchedFirm>
    {
        private DataManager manager;

        public MatchedFirmOperations()
        {
            manager = new SqlServerConnection().GetManager();
        }

        public List<MatchedFirm> getList()
        {
            try
            {
                string query = "SELECT * FROM dbo.MatchedFirm";
                DataSet dataSet = manager.GetDataSet(query);
                DataTable dataTable = new DataTable();

                DataTableReader reader = dataSet.CreateDataReader();
                dataTable.Load(reader);

                var list = new List<MatchedFirm>(dataTable.Rows.Count);
                foreach (DataRow row in dataTable.Rows)
                {
                    var values = row.ItemArray;
                    var firm = new MatchedFirm()
                    {
                        Id = Convert.ToDecimal(values[0]),
                        MusteriKodu = Convert.ToDecimal(values[1]),
                        Unvan = Convert.ToString(values[2]),
                        Tel = Convert.ToString(values[3]),
                        Fax = Convert.ToString(values[4]),
                        Mail = Convert.ToString(values[5]),
                        Website = Convert.ToString(values[6]),
                        TelMailStatus = Convert.ToString(values[7])
                    };
                    list.Add(firm);
                }
                return list;
            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return null;
            }
        }


        public void insert( MatchedFirm t )
        {
            try
            {
                string query = String.Format("INSERT INTO dbo.MatchedFirm (MusteriKodu, Unvan, Tel, Fax, Mail, Website, TelMailStatus) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                                t.MusteriKodu,
                                                t.Unvan,
                                                t.Tel,
                                                t.Fax,
                                                t.Mail,
                                                t.Website,
                                                t.TelMailStatus
                                                );
                manager.Execute(query);


            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
        }

        public void update( MatchedFirm t )
        {
            throw new NotImplementedException();
        }

        public void delete( MatchedFirm t )
        {
            throw new NotImplementedException();
        }

        public bool isExists( MatchedFirm t )
        {
            try
            {
                string query = String.Format("SELECT Id FROM dbo.MatchedFirm WHERE MusteriKodu = '{0}' AND Unvan = '{1}' AND Tel = '{2}'",
                                             t.MusteriKodu,
                                             t.Unvan,
                                             t.Tel);
                DataSet dataSet = manager.GetDataSet(query);
                DataTable dataTable = new DataTable();

                DataTableReader reader = dataSet.CreateDataReader();
                dataTable.Load(reader);
                if(dataTable.Rows.Count > 0)
                {
                    return true;
                }else
                {
                    return false;
                }

            }
            catch(Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return false;
            }
        }
    }
}
