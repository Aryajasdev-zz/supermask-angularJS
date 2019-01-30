using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sparsh.DataBaseConnectonCalss.Data;

namespace wigsboot.Models
{
    public class paypalcheck
    {
        public Decimal sAmountPaid {get;set;}
        public String paymentStatus {get;set;}
        public String orderid {get;set;}
        public String buyerEmail { get; set; }
        public String transactionID { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String address_name { get; set; }
        public String custstreet { get; set; }
        public String custcity { get; set; }
        public String custcountry { get; set; }
        public String custphone { get; set; }
        public String custpcode { get; set; }

        public Int32 createorder()
        {
            try
            {
                Int32 infoid = 0;
                SqlParameter[] arParams1 = new SqlParameter[10];
                arParams1[0] = new SqlParameter("@info", SqlDbType.VarChar);
                arParams1[0].Value = this.firstName + ' ' + this.lastName;
                arParams1[1] = new SqlParameter("@pcode", SqlDbType.VarChar);
                arParams1[1].Value = this.custpcode;
                arParams1[2] = new SqlParameter("@orderid", SqlDbType.VarChar);
                arParams1[2].Value = this.orderid;
                arParams1[3] = new SqlParameter("@amount", SqlDbType.Decimal);
                arParams1[3].Value = this.sAmountPaid;
                arParams1[4] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[4].Value = Common.siteid;
                arParams1[5] = new SqlParameter("@phone", SqlDbType.VarChar);
                arParams1[5].Value = this.custphone;
                arParams1[6] = new SqlParameter("@email", SqlDbType.VarChar);
                arParams1[6].Value = this.buyerEmail;
                arParams1[7] = new SqlParameter("@billing", SqlDbType.VarChar);
                arParams1[7].Value = this.address_name + "\r\n<br/>" + this.custstreet + "\r\n<br/>" + this.custcity + "\r\n<br/>" + this.custpcode + "\r\n<br/>" + this.custcountry;
                arParams1[8] = new SqlParameter("@delivery", SqlDbType.VarChar);
                arParams1[8].Value = this.address_name + "\r\n<br/>" + this.custstreet + "\r\n<br/>" + this.custcity + "\r\n<br/>" + this.custpcode + "\r\n<br/>" + this.custcountry;
                arParams1[9] = new SqlParameter("@paypalid", SqlDbType.VarChar);
                arParams1[9].Value = this.transactionID;
                String sql = "usp_create_sales_master_paypal";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        infoid = Convert.ToInt32(dr[0]);
                    }
                }
                return infoid;
            }
            catch(Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " createorder model");
                return 0;
            }
        }

        public static Int32 createorderreal(String orderid)
        {
            try
            {
                Int32 infoid = 0;
                SqlParameter[] arParams1 = new SqlParameter[4];
                arParams1[0] = new SqlParameter("@orderid", SqlDbType.VarChar);
                arParams1[0].Value = orderid;
                arParams1[1] = new SqlParameter("@userid", SqlDbType.BigInt);
                arParams1[1].Value = 0;
                arParams1[2] = new SqlParameter("@paytype", SqlDbType.VarChar);
                arParams1[2].Value = "realex";
                arParams1[3] = new SqlParameter("@istele", SqlDbType.TinyInt);
                arParams1[3].Value = 0;
                String sql = "usp_create_sales_master";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        infoid = Convert.ToInt32(dr[0]);
                    }
                }
                return infoid;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " createorderreal model "+ orderid);
                return 0;
            }

        }
    }
}