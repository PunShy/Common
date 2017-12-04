using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extend
{
    /// <summary>
    /// 為了和資料庫欄位名稱對映
    /// </summary>
    public class ReflectToColumnAttribute : Attribute
    {
        public string ColumnName { set; get; }

        public ReflectToColumnAttribute(string ColumnName)
        {
            this.ColumnName = ColumnName;
        }
    }
    /// <summary>
    /// DataTable功能擴充
    /// </summary>
    public static class DataTableExtensions
    {
        public static string ToDbRuleValue(this string value)
        {
            return value == "" ? null : value;
        }

        /// <summary>
        /// 將DataTable轉換成指定class物件
        /// </summary>
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = MappingItem<T>((DataRow)row, properties);
                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 對應用
        /// </summary>
        private static T MappingItem<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            string propertyName;
            ReflectToColumnAttribute attribute;
            foreach (var property in properties)
            {
                attribute = property.GetCustomAttributes(false).Where(a => a.GetType() == typeof(ReflectToColumnAttribute)).FirstOrDefault() as ReflectToColumnAttribute;
                if (attribute != null) { propertyName = attribute.ColumnName; }
                else propertyName = property.Name;

                if (row.Table.Columns.Contains(propertyName))
                {
                    //針對欄位的型態去轉換
                    if (property.PropertyType == typeof(DateTime))
                    {
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(row[propertyName].ToString(), out dt))
                        {
                            property.SetValue(item, dt, null);
                        }
                        else
                        {
                            property.SetValue(item, null, null);
                        }
                    }
                    else if (property.PropertyType == typeof(decimal))
                    {
                        decimal val = new decimal();
                        decimal.TryParse(row[propertyName].ToString(), out val);
                        property.SetValue(item, val, null);
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        double val = new double();
                        double.TryParse(row[propertyName].ToString(), out val);
                        property.SetValue(item, val, null);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        int val = new int();
                        int.TryParse(row[propertyName].ToString(), out val);
                        property.SetValue(item, val, null);
                    }
                    else
                    {
                        if (row[propertyName] != DBNull.Value)
                        {
                            property.SetValue(item, row[propertyName], null);
                        }
                    }
                }
            }
            return item;
        }
    }
}
