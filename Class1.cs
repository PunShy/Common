using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Common
{
    public static class SiteHelper
    {
        //public void aa()
        //{
        //    //string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();

        //    ////deserialize the object
        //    //LoginInfo objUsr = Deserialize<LoginInfo>(strJson);
        //}

        public static string Serialize(object o)
        {
            XmlSerializer ser = new XmlSerializer(o.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, o);
            return sb.ToString();
        }

        public static T Deserialize<T>(string s)
        {
            XmlDocument xdoc = new XmlDocument();

            try
            {
                xdoc.LoadXml(s);
                XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);

                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }
    }
}




