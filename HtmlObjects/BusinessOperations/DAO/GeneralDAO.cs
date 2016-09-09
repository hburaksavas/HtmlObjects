using HtmlObjects.DataOperations.DbOperations.ConnectOperations;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HtmlObjects.BusinessOperations.DAO
{
    public class GeneralDAO
    {
        /// <summary>
        /// Eşleşen ve eşleşmeyen firmaları bulan proc'ları çalıştırmak için kullanılır
        /// </summary>
        /// <param name="table"></param>
        /// <param name="procedureName"></param>
        public void executeProcedureWithTableParam(DataTable table, string procedureName)
        {
            try
            {
                SqlServerConnection con = new SqlServerConnection();

                if (table != null && !String.IsNullOrEmpty(procedureName))
                {

                    con.executeProcedureWithTableParam(table, procedureName);

                }
                else
                {

                    throw new Exception("DataTable veya procedureName nesnelerinden biri bos veya null deger sahip");

                }

            }catch(Exception e)
            {
                PrintConsole.LOG(e.StackTrace, e.Message);
            }
        }


    }
}
