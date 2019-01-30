using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sparsh.DataBaseConnectonCalss.Data;

namespace wigsboot.Models
{
    public class supplyprods
    {
        public String name { get; set;}
        public String code { get; set; }

        public static IEnumerable<supplyprods> getsupplyprods(Int32 prodid)
        {
            try
            {
                String sql = "";
                List<supplyprods> spcode = new List<supplyprods>();
                if (prodid > 0)
                {
                    SqlParameter[] arParams1 = new SqlParameter[1];
                    arParams1[0] = new SqlParameter("@prodid", SqlDbType.BigInt);
                    arParams1[0].Value = prodid;
                    sql = "usp_get_supplier_codes";
                    DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        supplyprods sp = new supplyprods();
                        sp.name = dr[0].ToString();
                        sp.code = dr[1].ToString();
                        spcode.Add(sp);
                    }
                }
                return spcode;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getsupplyprods model");
                return null;
            }
        }
    }
}