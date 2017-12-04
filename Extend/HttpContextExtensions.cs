using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace Common.Extend
{
    public static class HttpContextExtensions
    {
        public static string GetDataValue(this HttpContext obj, string name)
        {
            return obj.Request[name].ToString();
        }

        public static List<T> ToJsonData<T>(this HttpContext obj, string name)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var jsonString = String.Empty;
            obj.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(obj.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }
            return jsonSerializer.Deserialize<List<T>>(jsonString);
        }
        
        private static T ToModel<T>(this HttpRequest input) where T : new()
        {
            T target = new T();
            Type typeVal = target.GetType();
            FieldInfo[] myPropInfo = typeVal.GetFields();
            foreach (var item in myPropInfo)
            {
                item.SetValue(target, input[item.Name]);
            }
            return target;
        }
    }
}
