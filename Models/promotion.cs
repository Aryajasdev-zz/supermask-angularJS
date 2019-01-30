using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sparsh.DataBaseConnectonCalss.Data;

namespace wigsboot.Models
{
    public class promotion
    {
        public Int32 dcid { get; set; }
        public String code { get; set; }
        public Int32 dtype { get; set; }
        public Int32 siteid { get; set; }
        public Decimal dis { get; set; }
        public Decimal amt { get; set; }
        public String img { get; set; }
        public String name { get; set; }
        public Int32 postageid { get; set; }
        public DateTime sdate { get; set; }
        public DateTime edate { get; set; }

        public promotion(String code)
        {
            try
            {
                DataTable dt = null;
                String sql = "";             
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@code", SqlDbType.VarChar);
                arParams1[0].Value = code;
                sql = "usp_get_promotion_details";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {                    
                    this.dcid = Convert.ToInt32(dr[0]);
                    this.code = dr[1].ToString();
                    this.dtype = Convert.ToInt32(dr[2]);
                    this.siteid = Convert.ToInt32(dr[3]);
                    this.dis = Convert.ToDecimal(dr[4]);
                    this.amt = Convert.ToDecimal(dr[5]);
                    this.img = dr[6].ToString();
                    this.name = dr[7].ToString();
                    this.postageid = Convert.ToInt32(dr[8]);
                    this.sdate = Convert.ToDateTime(dr[9]);
                    this.edate = Convert.ToDateTime(dr[10]);                    
                }                
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getpromotion model");
            }
        }

        public static IEnumerable<promotion> getpromotion(String code)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<promotion> post = new List<promotion>();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@code", SqlDbType.VarChar);
                arParams1[0].Value = code;
                sql = "usp_get_promotion_details";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    promotion pd = new promotion(code);
                    pd.dcid = Convert.ToInt32(dr[0]);
                    pd.code = dr[1].ToString();
                    pd.dtype = Convert.ToInt32(dr[2]);
                    pd.siteid = Convert.ToInt32(dr[3]);
                    pd.dis = Convert.ToDecimal(dr[4]);
                    pd.amt = Convert.ToDecimal(dr[5]);
                    pd.img = dr[6].ToString();
                    pd.name = dr[7].ToString();
                    pd.postageid = Convert.ToInt32(dr[8]);
                    pd.sdate = Convert.ToDateTime(dr[9]);
                    pd.edate = Convert.ToDateTime(dr[10]);
                    post.Add(pd);
                }
                return post;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getpromotion model");
                return null;
            }
        }

     }
}