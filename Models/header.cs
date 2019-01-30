using Sparsh.DataBaseConnectonCalss.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wigsboot.Models
{
    public class header
    {
        public String name { get; set; }
        public String longname { get; set; }
        public String title { get; set; }
        public String header1 { get; set; }
        public String header2 { get; set; }
        public String himg { get; set; }
        public String meta { get; set; }
        public String keys { get; set; }
        public Int32 code { get; set; }
        public Int32 ccode { get; set; }
        public Int32 ctcode { get; set; }

        public static IEnumerable<header> getheader(Int32 code, Int32 siteid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<header> cat = new List<header>();
                SqlParameter[] arParams1 = new SqlParameter[2];
                arParams1[0] = new SqlParameter("@code", SqlDbType.BigInt);
                arParams1[0].Value = code;
                arParams1[1] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[1].Value = siteid;
                sql = "usp_get_headers_by_code";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    header ct = new header();
                    ct.name = dr[0].ToString();
                    ct.longname = dr[1].ToString();
                    ct.title = dr[2].ToString();
                    ct.header1 = dr[3].ToString();
                    ct.header2 = dr[4].ToString();
                    ct.himg = dr[5].ToString();
                    ct.meta = dr[6].ToString();
                    ct.keys = dr[7].ToString();
                    ct.code = Convert.ToInt32(dr[8]);
                    ct.ccode = Convert.ToInt32(dr[9]);
                    ct.ctcode = Convert.ToInt32(dr[10]);
                    cat.Add(ct);
                }
                return cat;
            }
            catch
            {
                return null;
            }
        }
    }
}