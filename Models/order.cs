using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sparsh.DataBaseConnectonCalss.Data;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace wigsboot.Models
{
    public class order
    {
        public String orderid { get; set; }
        public String name { get; set; }
        public String custadd { get; set; }
        public String deladd { get; set; }
        public String code { get; set; }
        public String custemail { get; set; }
        public String custphone { get; set; }
        public Int32 siteid { get; set; }
        public String discountcode { get; set; }
        public Decimal discount { get; set; }
        public Decimal donation { get; set; }
        public Decimal totamt { get; set; }
        public Decimal amount { get; set; }
        public Decimal postage { get; set; }
        public Int32 postageid { get; set; }
        public String ptype { get; set; }
        public Int32 istele { get; set; }
        public Int32 ismob { get; set; }
        
        public String sitename { get; set; }
        public String postname { get; set; }
        public String sitecode { get; set; }
        public String website { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public Int32 infoid { get; set; }

        public IEnumerable<basket> items { get; set; }
        
        public void generateorder(String Session)
        {
            try
            {
                String sql = "";                
                SqlParameter[] arParams1 = new SqlParameter[18];
                arParams1[0] = new SqlParameter("@orderid", SqlDbType.VarChar);
                arParams1[0].Value = this.orderid;
                arParams1[1] = new SqlParameter("@name", SqlDbType.VarChar);
                arParams1[1].Value = this.name;
                arParams1[2] = new SqlParameter("@custadd", SqlDbType.VarChar);
                arParams1[2].Value = this.custadd;
                arParams1[3] = new SqlParameter("@deladd", SqlDbType.VarChar);
                arParams1[3].Value = this.deladd;
                arParams1[4] = new SqlParameter("@code", SqlDbType.VarChar);
                arParams1[4].Value = this.code;
                arParams1[5] = new SqlParameter("@custemail", SqlDbType.VarChar);
                arParams1[5].Value = this.custemail;
                arParams1[6] = new SqlParameter("@custphone", SqlDbType.VarChar);
                arParams1[6].Value = this.custphone;
                arParams1[7] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[7].Value = this.siteid;
                arParams1[8] = new SqlParameter("@discountcode", SqlDbType.VarChar);
                arParams1[8].Value = this.discountcode;
                arParams1[9] = new SqlParameter("@discount", SqlDbType.Decimal);
                arParams1[9].Value = this.discount;
                arParams1[10] = new SqlParameter("@donation", SqlDbType.Decimal);
                arParams1[10].Value = this.donation;
                arParams1[11] = new SqlParameter("@amount", SqlDbType.Decimal);
                arParams1[11].Value = this.amount;
                arParams1[12] = new SqlParameter("@postage", SqlDbType.Decimal);
                arParams1[12].Value = this.postage;
                arParams1[13] = new SqlParameter("@postageid", SqlDbType.BigInt);
                arParams1[13].Value = this.postageid;
                arParams1[14] = new SqlParameter("@ptype", SqlDbType.VarChar);
                arParams1[14].Value = this.ptype;
                arParams1[15] = new SqlParameter("@istele", SqlDbType.BigInt);
                arParams1[15].Value = this.istele;
                arParams1[16] = new SqlParameter("@session", SqlDbType.VarChar);
                arParams1[16].Value = Session;
                arParams1[17] = new SqlParameter("@deviceid", SqlDbType.BigInt);
                arParams1[17].Value = this.ismob;
                sql = "usp_insert_presale";
                Int32 i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(),CommandType.StoredProcedure,sql, arParams1);
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " generateorder model" + Session);                
            }
        }

        public static order getorder(String Orderid, Int32 type)
        {
            try
            {
                String sql = "";
                order ord = new order();
                SqlParameter[] arParams1 = new SqlParameter[2];
                arParams1[0] = new SqlParameter("@orderid", SqlDbType.VarChar);
                arParams1[0].Value = Orderid;
                arParams1[1] = new SqlParameter("@type", SqlDbType.BigInt);
                arParams1[1].Value = type;
                sql = "usp_get_order_details";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ord.orderid = Convert.ToString(dr[0]);
                        ord.name = Convert.ToString(dr[1]);
                        ord.custadd = Convert.ToString(dr[2]);
                        ord.deladd = Convert.ToString(dr[3]);
                        ord.code = Convert.ToString(dr[4]);
                        ord.custemail = Convert.ToString(dr[5]);
                        ord.custphone = Convert.ToString(dr[6]);
                        ord.siteid = Convert.ToInt32(dr[7]);
                        ord.sitename = Convert.ToString(dr[8]);
                        ord.discountcode = Convert.ToString(dr[9]);
                        ord.discount = Convert.ToDecimal(dr[10]);
                        ord.donation = Convert.ToDecimal(dr[11]);
                        ord.amount = Convert.ToDecimal(dr[12]);
                        ord.postage = Convert.ToDecimal(dr[13]);
                        ord.postageid = Convert.ToInt32(dr[14]);
                        ord.ptype = Convert.ToString(dr[15]);
                        ord.postname = Convert.ToString(dr[16]);
                        ord.sitecode = Convert.ToString(dr[17]);
                        ord.website = Convert.ToString(dr[18]);
                        ord.phone = Convert.ToString(dr[19]);
                        ord.email = Convert.ToString(dr[20]);
                        ord.infoid = Convert.ToInt32(dr[21]);
                        ord.items = basket.getbasketSuccess(ord.orderid);
                    }
                }
                return ord;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getorder model ");
                return null;
            }
        }
    }
}