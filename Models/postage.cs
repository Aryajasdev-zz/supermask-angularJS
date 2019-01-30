using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sparsh.DataBaseConnectonCalss.Data;
using System.Data;
using System.Data.SqlClient;

namespace wigsboot.Models
{
    public class postage
    {
        public Int32 postageid { get; set; }
        public String mtype { get; set; }
        public String method { get; set; }
        public Decimal amt { get; set; }
        public String mobile { get; set; }
        public String message { get; set; }
        public Int32 isnd { get; set; }
        public Int32 issr { get; set; }
        public Int32 iseu { get; set; }
        public Int32 isww { get; set; }

        public static IEnumerable<postage> getpostage(Decimal totamt)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<postage> post = new List<postage>();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@totamt", SqlDbType.Decimal);
                arParams1[0].Value = totamt;
                sql = "usp_get_dis_postage";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    postage pd = new postage();
                    pd.postageid = Convert.ToInt32(dr[0]);
                    pd.mtype = dr[1].ToString();
                    pd.method = dr[2].ToString();
                    pd.amt = Convert.ToDecimal(dr[3]);
                    pd.mobile = dr[4].ToString();
                    pd.message = dr[5].ToString();
                    pd.isnd = Convert.ToInt32(dr[6]);
                    pd.issr = Convert.ToInt32(dr[7]);
                    pd.iseu = Convert.ToInt32(dr[8]);
                    pd.isww = Convert.ToInt32(dr[9]);
                    post.Add(pd);
                }
                return post;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getpostage model");
                return null;
            }
        }

        public static IEnumerable<postage> getpostage()
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<postage> post = new List<postage>();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@totamt", SqlDbType.Decimal);
                arParams1[0].Value = 5000;
                sql = "usp_get_dis_postage";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    postage pd = new postage();
                    pd.postageid = Convert.ToInt32(dr[0]);
                    pd.mtype = dr[1].ToString();
                    pd.method = dr[2].ToString();
                    pd.amt = Convert.ToDecimal(dr[3]);
                    pd.mobile = dr[4].ToString();
                    pd.message = dr[5].ToString();
                    pd.isnd = Convert.ToInt32(dr[6]);
                    pd.issr = Convert.ToInt32(dr[7]);
                    post.Add(pd);
                }
                return post;
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<postage> getppostage(Decimal totamt, String code)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<postage> post = new List<postage>();
                SqlParameter[] arParams1 = new SqlParameter[2];
                arParams1[0] = new SqlParameter("@code", SqlDbType.VarChar);
                arParams1[0].Value = code;
                arParams1[1] = new SqlParameter("@totamt", SqlDbType.Decimal);
                arParams1[1].Value = totamt;
                sql = "usp_get_promotion_postage";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    postage pd = new postage();
                    pd.postageid = Convert.ToInt32(dr[0]);
                    pd.mtype = dr[1].ToString();
                    pd.method = dr[2].ToString();
                    pd.amt = Convert.ToDecimal(dr[3]);
                    pd.mobile = dr[4].ToString();
                    pd.message = dr[5].ToString();
                    pd.isnd = Convert.ToInt32(dr[6]);
                    pd.issr = Convert.ToInt32(dr[7]);
                    post.Add(pd);
                }
                return post;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getpostage model");
                return null;
            }
        }

        public static IEnumerable<postage> getimppostage()
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<postage> post = new List<postage>();
                sql = "usp_get_imppostage";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    postage pd = new postage();
                    pd.postageid = Convert.ToInt32(dr[0]);
                    pd.mtype = dr[1].ToString();
                    pd.method = dr[2].ToString();
                    pd.amt = Convert.ToDecimal(dr[3]);
                    pd.mobile = dr[4].ToString();
                    post.Add(pd);
                }
                return post;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getpostage model");
                return null;
            }
        }

        public static Decimal getpostagediscount()
        {
            try
            {
                DataTable dt = null;
                Decimal amt = 0;
                String sql = "Select pdamount from postage_discount where status=1";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    amt = Convert.ToDecimal(dr[0]);
                }
                return amt;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getpostage model");
                return 0;
            }
        }

        public static Int32 getdiscountpostid()
        {
            try
            {
                DataTable dt = null;
                Int32 postageid = 0;
                String sql = "Select postageid from postage_master where status=1 and isdiscount=1";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    postageid = Convert.ToInt32(dr[0]);
                }
                return postageid;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getpostage model");
                return 3;
            }
        }
    }
}