using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sparsh.DataBaseConnectonCalss.Data;
using System.Data;
using System.Data.SqlClient;

namespace wigsboot.Models
{
    public class maincats
    {
        public Int32 code { get; set; }
        public String name { get; set; }
        public String imgname { get; set; }
        public String url { get; set; }
        public String seocmt { get; set; }
        public String seotitle { get; set; }
        public String pagetitle { get; set; }
        public String seodes { get; set; }
        public String seokey { get; set; }
        public String seoh2 { get; set; }
        public Int32 parentcode { get; set; }
        public String seodes2 { get; set; }

        public static IEnumerable<maincats> getmaincats(Int32 siteid)
        {
            try
            {
                String sql = "";
                List<maincats> ctcode = new List<maincats>();
                sql = "usp_get_maincats";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    maincats frm = new maincats();
                    frm.code = Convert.ToInt32(dr[0]);
                    frm.name = dr[1].ToString();                    
                    frm.imgname = dr[2].ToString();
                    frm.url = dr[3].ToString();
                    frm.seotitle = dr[4].ToString();
                    frm.pagetitle = dr[5].ToString();
                    frm.seodes = dr[6].ToString();
                    frm.seokey = dr[7].ToString();
                    frm.seoh2 = dr[8].ToString();
                    frm.parentcode = Convert.ToInt32(dr[9]);
                    frm.seodes2 = dr[10].ToString();
                    ctcode.Add(frm);
                }                
                return ctcode;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getmaincats model");
                return null;
            }
        }

        public static Int32 getmaincat(Int32 prodid)
        {
            try
            {
                String sql = "";
                Int32 catid = 0;
                sql = "Select catcode from cats_products where ismain=1 and prodid="+prodid;
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    catid = Convert.ToInt32(dr[0]);
                }
                return catid;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " get maincats model");
                return 0;
            }
        }        
   }
}