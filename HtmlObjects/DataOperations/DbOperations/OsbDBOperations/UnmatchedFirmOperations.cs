using HtmlObjects.DataOperations.DbOperations.ConnectOperations;
using HtmlObjects.DataOperations.DbOperations.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace HtmlObjects.DataOperations.DbOperations.OsbDBOperations
{
    public class UnmatchedFirmOperations : IFirmOperation<UnmatchedFirm>
    {
        private DataManager manager;
        
        public UnmatchedFirmOperations()
        {
            manager = new SqlServerConnection().GetManager();
        }

        public List<UnmatchedFirm> getList()
        {
            try
            {
                string query = "SELECT * FROM dbo.UnmatchedFirm";
                DataSet dataSet = manager.GetDataSet(query);
                DataTable dataTable = new DataTable();

                DataTableReader reader = dataSet.CreateDataReader();
                dataTable.Load(reader);

                var list = new List<UnmatchedFirm>(dataTable.Rows.Count);
                foreach (DataRow row in dataTable.Rows)
                {
                    var values = row.ItemArray;
                    var firm = new UnmatchedFirm()
                    {
                            Id     =Convert.ToDecimal(values[0]),
                            Unvan  =Convert.ToString(values[1]),
                            Tel    =Convert.ToString(values[2]),
                            Fax    =Convert.ToString(values[3]),
                            Mail   =Convert.ToString(values[4]),
                            Website=Convert.ToString(values[5])
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

        /// <summary>
        /// Normalizasyon uygulanacak
        /// </summary>
        /// <param name="t"></param>
        public void insert( UnmatchedFirm t )
        {
            try
            {
                string query = String.Format("INSERT INTO dbo.UnmatchedFirm (Unvan, Tel, Fax, Mail, Website) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                                                t.Unvan,
                                                t.Tel,
                                                t.Fax,
                                                t.Mail,
                                                t.Website);
                manager.Execute(query);


            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
            
        }

        public void update( UnmatchedFirm t )
        {
            throw new NotImplementedException();
        }

        public void delete( UnmatchedFirm t )
        {
            throw new NotImplementedException();
        }

        public bool isExists(UnmatchedFirm t)
        {
            try
            {
                string query = String.Format("SELECT Id FROM dbo.UnmatchedFirm WHERE Unvan = '{0}' AND Tel = '{1}' ",
                                             t.Unvan,
                                             t.Tel);

                DataSet dataSet = manager.GetDataSet(query);
                DataTable dataTable = new DataTable();

                DataTableReader reader = dataSet.CreateDataReader();
                dataTable.Load(reader);
                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
                return false;
            }
        }
    }
}
