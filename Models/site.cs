using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sparsh.DataBaseConnectonCalss.Data;

namespace wigsboot.Models
{
    public class site
    {
        public Int32 siteid { get; set; }
        public String sitename { get; set; }
        public String sitedesc { get; set; }
        public String siteadd1 { get; set; }
        public String siteadd2 { get; set; }
        public String sitephone { get; set; }
        public String siteemail { get; set; }
        public String sitecode { get; set; }
        public String realcode { get; set; }
        public Int32 isonline { get; set; }
        public Int32 priceid { get; set; }
        public String paypalemail { get; set; }
        public String maintitle { get; set; }
        public String metadesc { get; set; }
        public String metakeys { get; set; }

        public site()
        {
            try
            {
                String sql = "";
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[0].Value = Common.siteid;
                sql = "usp_get_site_details";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    this.siteid = Common.siteid;
                    this.sitename = dr[0].ToString();
                    this.sitedesc = dr[1].ToString();
                    this.siteadd1 = dr[2].ToString();
                    this.siteadd2 = dr[3].ToString();
                    this.sitephone = dr[4].ToString();
                    this.siteemail = dr[5].ToString();
                    this.sitecode = dr[6].ToString();
                    this.realcode = dr[7].ToString();
                    this.isonline = Convert.ToInt32(dr[8]);
                    this.priceid = Convert.ToInt32(dr[9]);
                    this.paypalemail = dr[10].ToString();
                    this.maintitle = dr[11].ToString();
                    this.metadesc = dr[12].ToString();
                    this.metakeys = dr[13].ToString();
                }                
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " site model");             
            }
        }        
    }
}