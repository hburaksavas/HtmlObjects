using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlObjects.DataOperations.DbOperations.ConnectOperations
{
    public class SqlServerConnection
    {
        private  string connectionString = "Data Source=trinity;Initial Catalog=OSB;Integrated Security=True;Connect Timeout=180;" +
                                           "Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static DataProviderType providerType = DataProviderType.SQLSERVER;

        public SqlServerConnection() { }

        public  DataManager GetManager()
        {

            return new DataManager(providerType, connectionString);

        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Eşleşen ve eşleşmeyen firmaları bulan proc'ları çalıştırmak için kullanılır
        /// </summary>
        /// <param name="table"></param>
        /// <param name="procedureName"></param>
        public void executeProcedureWithTableParam(DataTable table,string procedureName)
        {
           
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    Console.WriteLine("Db connection is opened");
                    SqlCommand cmnd = new SqlCommand(procedureName, connection);

                    cmnd.CommandType = CommandType.StoredProcedure;
                    cmnd.CommandTimeout = 180;
                    SqlParameter param = new SqlParameter("@tableVar", SqlDbType.Structured)
                    {

                        TypeName = "dbo.FoundedFirm",

                        Value = table

                    };
                    cmnd.Parameters.Add(param);

                    Console.WriteLine("Query is ready to execute");
                    cmnd.ExecuteNonQuery();
                    Console.WriteLine("Successfully!");
                }

            }
            catch (Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
        }

    }
}
