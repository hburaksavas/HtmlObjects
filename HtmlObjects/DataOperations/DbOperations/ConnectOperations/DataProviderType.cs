using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;


public enum DataProviderType
{
        SQLSERVER,
        OLEDB,
        ODBC
}


internal class DBFactory
{
    private static DbProviderFactory objFactory = null;
    
    public static DbProviderFactory GetDataProvider(DataProviderType providerType)
    {
        switch (providerType)
        {
            case DataProviderType.SQLSERVER:
                objFactory = SqlClientFactory.Instance;
                break;
            case DataProviderType.OLEDB:
                objFactory = OleDbFactory.Instance;
                break;           
            case DataProviderType.ODBC:
                objFactory = OdbcFactory.Instance;
                break;
        }
        return objFactory;
    }

    public static DbConnection GetConnection(DataProviderType providerType)
    {
        switch (providerType)
        {
            case DataProviderType.SQLSERVER:
                return new SqlConnection();
            case DataProviderType.OLEDB:
                return new OleDbConnection();
            case DataProviderType.ODBC:
                return new OdbcConnection();
            default:
                return null;
        }
    }

    public static DbCommand GetCommand(DataProviderType providerType)
    {
        switch (providerType)
        {

            case DataProviderType.SQLSERVER:
                return new SqlCommand();
            case DataProviderType.OLEDB:
                return new OleDbCommand();
            case DataProviderType.ODBC:
                return new OdbcCommand();
            default: return null;
        }
    }

    public static DbDataAdapter GetDataAdapter(DataProviderType providerType)
    {
        switch (providerType)
        {
            case DataProviderType.SQLSERVER:
                return new SqlDataAdapter();
            case DataProviderType.OLEDB:
                return new OleDbDataAdapter();
            case DataProviderType.ODBC:
                return new OdbcDataAdapter();
            default:
                return null;
        }
    }

}    

