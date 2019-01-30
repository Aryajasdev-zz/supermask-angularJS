using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Sparsh.DataBaseConnectonCalss.Data;
using System.Data;

namespace wigsboot.Models
{
    public class recentprods
    {
        public Int32 prodid { get; set; }
        public String name { get; set; }        
        public String img { get; set; }
        public String url { get; set; }

        public static void recentitems(string sessionid, Int32 prodid)
        {
            try
            {
                String sql = "";
                if (prodid > 0)
                {
                    SqlParameter[] arParams1 = new SqlParameter[2];
                    arParams1[0] = new SqlParameter("@session", SqlDbType.VarChar);
                    arParams1[0].Value = sessionid;
                    arParams1[1] = new SqlParameter("@prodid", SqlDbType.BigInt);
                    arParams1[1].Value = prodid;                    
                    sql = "usp_insert_recent_prods";
                    Int32 i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1);
                }
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " recentitems model");             
            }
        }

        public static IEnumerable<recentprods> getrecentitems(string sessionid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<recentprods> prod = new List<recentprods>();
                if (sessionid != "")
                {
                    SqlParameter[] arParams1 = new SqlParameter[1];
                    arParams1[0] = new SqlParameter("@session", SqlDbType.VarChar);
                    arParams1[0].Value = sessionid;
                    sql = "usp_get_recent_prods";
                    dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        recentprods pd = new recentprods();
                        pd.prodid = Convert.ToInt32(dr[0]);
                        pd.name = dr[1].ToString();
                        pd.img = dr[2].ToString();
                        pd.url = dr[3].ToString();
                        prod.Add(pd);
                    }
                }
                return prod;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getcreateitem model");
                return null;
            }
        }
    }
}