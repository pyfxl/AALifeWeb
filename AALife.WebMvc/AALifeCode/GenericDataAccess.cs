using System.Data;
using System.Data.Common;

public class GenericDataAccess
{
    static GenericDataAccess()
    {
    }

    public static DataTable ExecuteCommand(DbCommand command)
    {
        DataTable table;

        try
        {
            command.Connection.Open();
            DbDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            reader.Close();
        }
        catch
        {
            throw;
        }
        finally
        {
            command.Connection.Close();
        }

        return table;
    }

    public static int ExecuteNonQuery(DbCommand command)
    {
        int findRows = -1;

        try
        {
            command.Connection.Open();
            findRows = command.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            command.Connection.Close();
        }

        return findRows;
    }

    public static string ExecuteScalar(DbCommand command)
    {
        string value = "";

        try
        {
            command.Connection.Open();
            value = command.ExecuteScalar().ToString();
        }
        catch
        {
            throw;
        }
        finally
        {
            command.Connection.Close();
        }

        return value;
    }

    public static DbCommand CreateCommand()
    {
        string dbProviderName = WebConfiguration.DbProviderName;
        string dbConnectionString = WebConfiguration.DbConnectionString;
        DbProviderFactory factory = DbProviderFactories.GetFactory(dbProviderName);
        DbConnection conn = factory.CreateConnection();
        conn.ConnectionString = dbConnectionString;
        DbCommand comm = conn.CreateCommand();
        comm.CommandType = CommandType.StoredProcedure;
        return comm;
    }
}
