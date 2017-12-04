using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// DbInfo 的摘要描述
/// </summary>
public class DbInfo
{
    public DbInfo()
    {
    }

    public DataTable GetData(string sql , SqlParameter[] values)
    {   
        DataTable dt1;
        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBMainConnectionString"].ConnectionString))
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            if(values != null) command.Parameters.AddRange(values);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                dt1 = new DataTable();
                dt1.Load(reader);
            }   
        }
        return dt1;
    }

    //public int InsertData(string data, string dis)
    //{
    //    string sql = string.Format(@"INSERT INTO DataTable (data ,distance ,date) VALUES ('{0}','{1}','{2}')", data, dis, DateTime.Now.ToString("yyyyMMdd hh:mm:ss"));
    //    int result = InsertData(sql);
    //    return result;
    //}

    public int InsertData(string sqlCommand , SqlParameter[] values)
    {
        int count = 0;
        //List<DataInfo> datas = new List<DataInfo>();
        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBMainConnectionString"].ConnectionString))
        using (SqlCommand command = new SqlCommand(sqlCommand, connection))
        {
            command.Parameters.AddRange(values);
            connection.Open();
            command.CommandType = CommandType.Text;
            count = command.ExecuteNonQuery();
        }
        return count;
    }
    public DataTable InsertDataReturnDatatable(string sqlCommand, SqlParameter[] values)
    {
        DataTable dt1;
        //List<DataInfo> datas = new List<DataInfo>();
        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBMainConnectionString"].ConnectionString))
        using (SqlCommand command = new SqlCommand(sqlCommand, connection))
        {
            command.Parameters.AddRange(values);
            connection.Open();
            command.CommandType = CommandType.Text;            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                dt1 = new DataTable();
                dt1.Load(reader);
            }            
        }
        return dt1;
    }
}

//public class DataInfo
//{
//    public string Data { set; get; }
//    public string Distance { set; get; }
//    public string Date { set; get; }
//}