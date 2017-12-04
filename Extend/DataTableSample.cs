using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extend
{
    public class DataTableSample
    {
        public void TestSample()
        {
            //從資料庫拿出的資料轉成DataTable
            DataTable dt1 = new DataTable();
            //將DataTable轉成設定好的Class Model
            List<ProjectSettlement> dataList = dt1.ToList<ProjectSettlement>();
        }
        
    }

    /// <summary>
    /// 與資料庫欄位對應的 model，Attribute是對應到資料庫欄位名稱
    /// </summary>
    public class ProjectSettlement
    {
        [ReflectToColumn("NO")]
        public int no { get; set; }
        [ReflectToColumn("CONS_ID")]
        public string proId { get; set; }
        [ReflectToColumn("CONS_TTL")]
        public string ttl { get; set; }
        [ReflectToColumn("CONS_TYP")]
        public string typ { get; set; }
        [ReflectToColumn("CONS_ADD")]
        public string addr { get; set; }
        [ReflectToColumn("CONS_DSC")]
        public string dsc { get; set; }
        [ReflectToColumn("CT_ID")]
        public string ctid { get; set; }

        [ReflectToColumn("PLAN_FRM")]
        public string planFrm { get; set; }
        [ReflectToColumn("DSN_FRM")]
        public string dsnFrm { get; set; }

        [ReflectToColumn("CONS_FRM")]
        public string consFrm { get; set; }
        [ReflectToColumn("SV_FRM")]
        public string svFrm { get; set; }
        [ReflectToColumn("C_AMOUNT")]
        public decimal cAmount { get; set; }
        [ReflectToColumn("S_AMOUNT")]
        public decimal sAmount { get; set; }
        [ReflectToColumn("ACT_DATE")]
        public string actDate { get; set; }
        [ReflectToColumn("1RC_DATE")]
        public string rc1Date { get; set; }
        [ReflectToColumn("2RC_DATE")]
        public string rc2Date { get; set; }
        [ReflectToColumn("STR_DATE")]
        public string strDate { get; set; }
        [ReflectToColumn("END_DATE")]
        public string endDate { get; set; }
        [ReflectToColumn("AD_ORG")]
        public string adOrg { get; set; }
        [ReflectToColumn("AD_NAME")]
        public string adName { get; set; }
        [ReflectToColumn("CON_DUR")]
        public int conDur { get; set; }
        [ReflectToColumn("EXT_DUR")]
        public int extDur { get; set; }
        [ReflectToColumn("WRT_DATE")]
        public string wrtDate { get; set; }
        [ReflectToColumn("KEYIN_DATE")]
        public string keyinDate { get; set; }
        [ReflectToColumn("NOTE")]
        public string note { get; set; }
        [ReflectToColumn("BOROUGHS")]
        public string borough { get; set; }
        [ReflectToColumn("count")]
        public int count { set; get; }

        [ReflectToColumn("manhole")]
        public int manhole { set; get; }
        [ReflectToColumn("pipeline")]
        public int pipeline { set; get; }
    }
}
