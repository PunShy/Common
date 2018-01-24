using System;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Test 的摘要描述
/// </summary>
public class DbWoker
{
    //System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnFormatString"].ConnectionString;
    private string _conectionStr = "";

    public string ConectionStr
    {
        get
        {
            return _conectionStr;
        }

        set
        {
            _conectionStr = value;
        }
    }

    public DbWoker()
    {

    }
    public DbWoker(string conection)
    {
        _conectionStr = conection;
    }
    
    public DataTable GetData(OleDbCommand command)
    {
        DataTable dt1 = new DataTable();
        using (OleDbConnection connection = new OleDbConnection(_conectionStr))
        {  
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            command.Connection = connection;

            adapter.SelectCommand = command;
            adapter.Fill(dt1);
        }
        return dt1;
    }
    public int InsertData(OleDbCommand command)
    {
        int result = 0;
        using (OleDbConnection connection = new OleDbConnection(_conectionStr))
        {
            connection.Close();
            connection.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            command.Connection = connection;

            adapter.InsertCommand = command;
            result = command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();
        }
        return result;
    }
    public int UpdateData(OleDbCommand command)
    {
        int result = 0;
        using (OleDbConnection connection = new OleDbConnection(_conectionStr))
        {
            connection.Close();
            connection.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            command.Connection = connection;

            adapter.UpdateCommand = command;
            result = adapter.UpdateCommand.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }
        return result;
    }
    public int DeleteData(OleDbCommand command)
    {
        int result = 0;
        using (OleDbConnection connection = new OleDbConnection(_conectionStr))
        {
            connection.Close();
            connection.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            command.Connection = connection;

            adapter.DeleteCommand = command;
            result = adapter.DeleteCommand.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }
        return result;
    }
    
}
