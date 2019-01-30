using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Sparsh.DataBaseConnectonCalss.Data;

namespace wigsboot.Models
{
    public class message
    {
        public message(String name, String email, String message, Int32 siteid)
        {
            try
            {
                String sql = "";
                SqlParameter[] arParams1 = new SqlParameter[4];
                arParams1[0] = new SqlParameter("@name", SqlDbType.VarChar);
                arParams1[0].Value = name;
                arParams1[1] = new SqlParameter("@email", SqlDbType.VarChar);
                arParams1[1].Value = email;
                arParams1[2] = new SqlParameter("@message", SqlDbType.VarChar);
                arParams1[2].Value = message;
                arParams1[3] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[3].Value = siteid;
                sql = "usp_insert_message";
                int i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1);
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex); ;
                logs.ErrorLog(ex.Message + " Line number- " + linenumber.ToString(), " message model");
            }
        }
    }
}