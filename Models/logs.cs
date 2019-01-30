using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Sparsh.DataBaseConnectonCalss.Data;

namespace wigsboot.Models
{
    public class logs
    {
        public static void ErrorLog(String msg, String mfunction)
        {
            try
            {
                SqlParameter[] arParams1;
                arParams1 = new SqlParameter[5];
                arParams1[0] = new SqlParameter("@logval", SqlDbType.VarChar);
                arParams1[0].Value = msg;
                arParams1[1] = new SqlParameter("@appcode", SqlDbType.VarChar);
                arParams1[1].Value = Common.sitecode;
                arParams1[2] = new SqlParameter("@moduleid", SqlDbType.BigInt);
                arParams1[2].Value = 0;
                arParams1[3] = new SqlParameter("@mfunction", SqlDbType.VarChar);
                arParams1[3].Value = mfunction;
                arParams1[4] = new SqlParameter("@userid", SqlDbType.BigInt);
                arParams1[4].Value = 0;
                String sql = "usp_save_error_log";
                Int32 i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1);
            }
            catch { }
        }        
    }
}