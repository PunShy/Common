using System;
using System.Collections.Generic;
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


    /// <summary>
    /// DataTable轉成指定Class物件，簡易版本。
    /// EX: var dt1 = _dbWoker1.GetData(command).AsEnumerable();
    ///     List<StatisticsChart> svmList = GetDbData<StatisticsChart>(dt1);
    /// </summary>
    /// <typeparam name="T">轉換目標Class</typeparam>
    /// <param name="dt1">來源DB資料，dt1.AsEnumerable()</param>
    /// <returns>指定Calss物件的集合</returns>
    public List<T> GetDbData<T>(EnumerableRowCollection<DataRow> dt1) where T : class, new()
    {
        List<T> rcmList = new List<T>();
        string tempVal = null;
        foreach (var item in dt1)
        {
            T rcm1 = new T();
            var temp = rcm1.GetType().GetProperties();
            foreach (var pro in temp)
            {
                tempVal = item[pro.Name].ToString();
                pro.SetValue(rcm1, (string.IsNullOrEmpty(tempVal)) ? "" : tempVal);
            }
            rcmList.Add(rcm1);
        }
        return rcmList;
    }
}


#region DbWoker 調用範例
public class DbWorker_Sample
{
    public static string delete()
    {
        DbWoker dw1 = new DbWoker();
        dw1.ConectionStr = @"Provider=SQLOLEDB;Server=localhost;uid=sa;pwd=as;database=CPAMI";//System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnFormatString"].ConnectionString;
        string str = @"DELETE FROM [dbo].[ContentData]
                           WHERE kind=? and city=? and step=?";
        OleDbCommand command = new OleDbCommand(str);
        command.Parameters.Add("kind", OleDbType.VarChar).Value = "2";
        command.Parameters.Add("city", OleDbType.VarChar).Value = "T11";
        command.Parameters.Add("step", OleDbType.VarChar).Value = "1";

        int count = dw1.InsertData(command);
        return "";
    }
    public static string update()
    {
        DbWoker dw1 = new DbWoker();
        dw1.ConectionStr = @"Provider=SQLOLEDB;Server=localhost;uid=sa;pwd=as;database=CPAMI";//System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnFormatString"].ConnectionString;
        string str = @"UPDATE [dbo].[ContentData]
                           SET [textInfo]=?
                           WHERE kind=? and city=? and step=?";
        OleDbCommand command = new OleDbCommand(str);
        command.Parameters.Add("textInfo", OleDbType.VarWChar).Value = "ER只是測試看看內文物???";
        command.Parameters.Add("kind", OleDbType.VarChar).Value = "2";
        command.Parameters.Add("city", OleDbType.VarChar).Value = "T11";
        command.Parameters.Add("step", OleDbType.VarChar).Value = "1";

        int count = dw1.InsertData(command);
        return "";
    }
    public static string Insert()
    {
        DbWoker dw1 = new DbWoker();
        dw1.ConectionStr = @"Provider=SQLOLEDB;Server=localhost;uid=sa;pwd=as;database=CPAMI";//System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnFormatString"].ConnectionString;
        string str = @"INSERT INTO [dbo].[ContentData]
           ([kind]
           ,[city]
           ,[step]
           ,[planName]
           ,[textInfo]
           ,[date])
     VALUES
           (?,?,?,?,?,?)";
        OleDbCommand command = new OleDbCommand(str);
        command.Parameters.Add("kind", OleDbType.VarChar).Value = "2";
        command.Parameters.Add("city", OleDbType.VarChar).Value = "T11";
        command.Parameters.Add("step", OleDbType.VarChar).Value = "1";
        command.Parameters.Add("planName", OleDbType.VarWChar).Value = "只是測試看看";
        command.Parameters.Add("textInfo", OleDbType.VarWChar).Value = "只是測試看看內文物";
        command.Parameters.Add("date", OleDbType.Date).Value = DateTime.Now.ToString("yyyy/MM/dd HH:mm:sss");

        int count = dw1.InsertData(command);
        return "";
    }

    public static string Get()
    {
        DbWoker dw1 = new DbWoker();
        dw1.ConectionStr = @"Provider=SQLOLEDB;Server=localhost;uid=sa;pwd=as;database=CPAMI";//System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnFormatString"].ConnectionString;
        string str = @"SELECT distinct city,planName,textInfo
                       FROM ContentData
                       where kind = ? and city = ?";
        OleDbCommand command = new OleDbCommand(str);
        command.Parameters.Add("kind", OleDbType.VarChar).Value = "2";
        command.Parameters.Add("city", OleDbType.VarChar).Value = "T10";

        DataTable dt1 = dw1.GetData(command);
        return "";
    }
}
#endregion