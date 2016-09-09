

using System;

using System.Data;
using System.Data.Common;


namespace HtmlObjects.DataOperations.DbOperations.ConnectOperations
{
    public sealed class DataManager
    {
        private DbConnection dbConnection;
        private String strConnectionString;
        private DataProviderType dataProviderType;

        public DataManager(DataProviderType providerType,string connectionString)
        {

            strConnectionString = connectionString;

            dataProviderType = providerType;

            dbConnection = DBFactory.GetConnection(providerType);

            dbConnection.ConnectionString = connectionString;
        }

        public void Open()
        {
            if(dbConnection.State != System.Data.ConnectionState.Open)
            {
                dbConnection.Open();
            }
        }

        public void Close()
        {
            if(dbConnection.State != System.Data.ConnectionState.Closed)
            {
                dbConnection.Close();
            }
        }

        public DbConnection Connection
        {
            get
            {
                return dbConnection;
            }
        }

        public String ConnectionString
        {
            get
            {
                return strConnectionString;
            }
        }

        public DataProviderType DBProvider
        {
            get
            {
                return dataProviderType;
            }
        }


        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public  DataSet GetDataSet(String sqlString)
        {
            using(DbDataAdapter dbDataAdapter = DBFactory.GetDataAdapter(this.DBProvider))
            {
                    try
                    {

                 
                        dbDataAdapter.SelectCommand = DBFactory.GetCommand(this.DBProvider);
                        dbDataAdapter.SelectCommand.CommandText = sqlString;
                        dbDataAdapter.SelectCommand.Connection = this.Connection;
                        dbDataAdapter.SelectCommand.CommandTimeout = 120;

                        DataSet dataSet = new DataSet();
                        DataTable dataTable = new DataTable();

                        dataTable.BeginLoadData();
                        dbDataAdapter.Fill(dataTable);
                        dataTable.EndLoadData();

                        dataSet.EnforceConstraints = false;
                        dataSet.Tables.Add(dataTable);
                        return dataSet;
                  
                        
                    }
                    catch(Exception ex)
                    {
                        PrintConsole.LOG(ex.StackTrace, ex.Message);
                        return null;
                }
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public bool Execute( String sqlQuery )
        {
            using (DbDataAdapter adapter = DBFactory.GetDataAdapter(this.DBProvider))
            {

                    try {
                        
                        adapter.InsertCommand = DBFactory.GetCommand(this.DBProvider);

                        adapter.InsertCommand.CommandText = sqlQuery;

                        adapter.InsertCommand.Connection = this.Connection;

                        adapter.InsertCommand.Connection.Open();
                        
                        adapter.InsertCommand.ExecuteNonQuery();

                        adapter.InsertCommand.Connection.Close();
                        return true;
                    }
                    catch(Exception e) {

                        PrintConsole.LOG(e.StackTrace, e.Message);
                        return false;
                }
                finally
                {
                    Close();
                }
            }
        }

       
    }
}
