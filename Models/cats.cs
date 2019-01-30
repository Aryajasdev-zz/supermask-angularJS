using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sparsh.DataBaseConnectonCalss.Data;

namespace wigsboot.Models
{
    public class cats
    {
        public Int32 code { get; set; }
        public String name { get; set; }
        public String catimg { get; set; }
        public String url { get; set; }
        public Int32 issale { get; set; }          
        public Int32 ismain { get; set; }  
           
         
        public static Int32 getlvl(string url)
        {
            try
            {
                Int32 lvl = 0;
                if (url != null)
                {
                    if (url != "")
                    {
                        SqlParameter[] arParams1 = new SqlParameter[1];
                        arParams1[0] = new SqlParameter("@url", SqlDbType.VarChar);
                        arParams1[0].Value = url;
                        String sql = "usp_get_url_level";
                        DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                lvl = Convert.ToInt32(row[0]);
                            }
                        }
                    }
                }
                return lvl;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getlvl model");
                return 0;
            }
        }

        public static IEnumerable<cats> getcats(string url, Int32 lv, Int32 siteid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<cats> cat = new List<cats>();
                if (!url.Equals(DBNull.Value) && url != "")
                {

                    SqlParameter[] arParams1 = new SqlParameter[2];
                    arParams1[0] = new SqlParameter("@url", SqlDbType.VarChar);
                    arParams1[0].Value = url;
                    arParams1[1] = new SqlParameter("@siteid", SqlDbType.BigInt);
                    arParams1[1].Value = siteid;
                    sql = "usp_get_url_data";
                    dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        cats ct = new cats();
                        ct.code = Convert.ToInt32(dr[0]);
                        ct.name = dr[1].ToString();
                        ct.catimg = "http://www.partysuperstores.co.uk/img/pc/"+dr[2].ToString();
                        ct.url = dr[3].ToString();                        
                        cat.Add(ct);
                    }
                }
                return cat;
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<categories> getallcats(string url, Int32 lv, Int32 siteid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<categories> cat = new List<categories>();
                if (!url.Equals(DBNull.Value) && url != "")
                {

                    SqlParameter[] arParams1 = new SqlParameter[2];
                    arParams1[0] = new SqlParameter("@url", SqlDbType.VarChar);
                    arParams1[0].Value = url;
                    arParams1[1] = new SqlParameter("@siteid", SqlDbType.BigInt);
                    arParams1[1].Value = siteid;
                    sql = "usp_get_url_data";
                    dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        categories ct = new categories();
                        ct.code = Convert.ToInt32(dr[0]);
                        ct.name = dr[1].ToString();
                        ct.catimg = "http://www.partysuperstores.co.uk/img/pc/" + dr[2].ToString();
                        ct.url = dr[3].ToString();
                        ct.icon = "ios-add-circle-outline";
                        ct.isshown = false;
                        lv = cats.getlvl(ct.url);
                        ct.subcats = getcats(ct.url, lv, siteid);
                        cat.Add(ct);
                    }
                }
                return cat;
            }
            catch
            {
                return null;
            }
        }  

        public static IEnumerable<cats> getprodcat(Int32 code)
        {
            try
            {
                List<cats> cat = new List<cats>();
                String sql = "Select code,name,'' as catimg,url from cats_master where parentcode="+code;
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    cats ct = new cats();
                    ct.code = Convert.ToInt32(dr[0]);
                    ct.name = dr[1].ToString();
                    ct.catimg = "http://www.partysuperstores.co.uk/img/pc/"+dr[2].ToString();
                    ct.url = dr[3].ToString();
                    cat.Add(ct);
                }
                return cat;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getprodcat model");
                return null;
            }

        }  
        
        public static Int32 getcatcode(String url)
        {
            try
            {
                String sql = "";
                Int32 lvl = 0;
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@url", SqlDbType.VarChar);
                arParams1[0].Value = url;
                sql = "usp_code_by_url";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                if (dt.Rows.Count > 0) lvl = Convert.ToInt32(dt.Rows[0][0]);
                return lvl;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getcatcode");
                return 0;
            }
        }  
    }
}