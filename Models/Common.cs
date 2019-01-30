using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml;
using System.Diagnostics;
using Sparsh.DataBaseConnectonCalss.Data;
using System.Web.UI;
using System.Web;
using System.Security.Cryptography;

namespace wigsboot.Models
{
	public static class Common
	{
		public static Int32 siteid = 8;
		public static Int32 itemperpage = 12;
		public static String sitecode = "DC";
		public static String merchantid = "partysuperstores";
		public static String secret = "GU7QNTcCtF";
		public static String merchantidtest = "partysuperstorestest";
		public static String secrettest = "secret";
		public static String curr = "GBP";
		public static String telephone = "0208 643 7425";
		public static String site = ConfigurationManager.AppSettings["site"];
		public static String imgurl = ConfigurationManager.AppSettings["imgurl"];  // "/img/";
		public static String catimgurl = ConfigurationManager.AppSettings["catimg"];  // "/img/pc/";
		public static String blogimgurl = "http://www.partysuperstores.co.uk/img/blogs/";
		public static String sitename = ConfigurationManager.AppSettings["sitename"];
		public static String paypalemail = "duncan@partysuperstores.co.uk";
		public static Decimal vat = 16.66m;
		public static String emergencyemail = ConfigurationManager.AppSettings["emerg"];
		public static String orderemail = ConfigurationManager.AppSettings["orderemail"];
		public static String ordercc = ConfigurationManager.AppSettings["ordercc"];
		public static String salesemail = ConfigurationManager.AppSettings["salesemail"];
		public static String password = ConfigurationManager.AppSettings["emailpass"];
		public static String smtp = ConfigurationManager.AppSettings["smtp"];
		public static Int32 port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpport"]);
		public static String maintitle = "";
		public static Int32 maincatid = 0;
		public static Int32 ismob = 0;
		public static Int32 discount = 10;
		public static Int32 lvl = 0;
		public static Int32 code = 0;
		public static String lname = "";
		public static String lsection = "";
		public static Int32 catcode = 0;
		public static Int32[] steps = new Int32[4] {100000000,1000000,1000,1};
				 
		public static string getconnectionstring()
		{
			String constring = ConfigurationManager.AppSettings["constring"];
			return constring;
		}

		public static String getvat(Decimal Amt)
		{
			try
			{
				String rvat = "";               
				rvat = (Amt/6).ToString("F"); 
				return rvat;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getvat common");
				return "0";
			}
		}

		public static Boolean checkapi(String api)
		{
			String sql = "Select * from appkeys_details where appkey='" + api + "'";
			DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
			if (dt.Rows.Count > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        public static void setpaypal(String payid,String orderid)
        {
            try
            {
                String sql = "insert into paypal_response(paypalid, orderid) values('"+payid+"','"+orderid+"')";
                int i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(), CommandType.Text, sql);
            } catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
        }           

		public static Int32 nextcat(Int32 code)
		{
			Int32 catcode = 0;
			Int32 lvl = getlevel(code);
			if (lvl == 1) catcode = code + 1000000;
			else if (lvl == 2) catcode = code + 1000;
			else catcode = code + 1;
			return catcode;
		}

		public static Decimal getamt(String orderid)
		{
			try
			{
				Decimal Amount = 0;
				String sql = "Select amount from presale_details where orderid='" + orderid + "'";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					Amount = Convert.ToDecimal(dt.Rows[0][0].ToString());
				}
				return Amount;
			}
			catch {
				return 0;
			}
		}		

		public static void confirmorder(String timestamp,String result,String orderid,String message, String authcode, String sha1hash)
		{
			try
			{
				String sql = "insert into confirm_details(timestamp,result,orderid,message,authcode,MD5HASH) values('" + timestamp + "','" + result + "','" + orderid + "','" + message + "','" + authcode + "','" + sha1hash + "')";
				int i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(), CommandType.Text, sql);
			}
			catch { }
		}

		public static Boolean isholiday()
		{
			try
			{
				Boolean yes = false;
				String sql = "usp_get_holiday";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					yes = true;
				}
				return yes;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex); ;
				logs.ErrorLog(ex.Message + " Line number- " + linenumber.ToString(), "isholiday");
				return false;
			}
		}

		public static String toPounds(Decimal num,Boolean full=false)
		{
			try
			{
				if (num < 1)
				{
					return "&nbsp;" + Convert.ToInt32(num * 100) + "p";
				}
				else
				{
					return "&pound;" + num.ToString("F");
				}
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " topounds common");
				return num.ToString();
			}
		}

		public static String getdelday(Int32 et)
		{
			try
			{
				String curday = "";
				SqlParameter[] arParams1 = new SqlParameter[1];
				arParams1[0] = new SqlParameter("@et", SqlDbType.BigInt);
				arParams1[0].Value = et;
				String sql = "usp_get_next_delivery";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
				if (dt.Rows.Count > 0)
				{
					curday = dt.Rows[0][0].ToString();
				}
				return curday;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex); ;
				logs.ErrorLog(ex.Message + " Line number- " + linenumber.ToString(), " dmessage");
				return "Your Order will be dispatched Today";
			}
		}

        public static string SHA1Hash(string text)
        {
            try
            {
                SHA1 sha = new SHA1CryptoServiceProvider();
                sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
                byte[] result = sha.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }
                return strBuilder.ToString();
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " MD5Hash Common");
                return null;
            }
        }

        public static Decimal getdiscount(String code, Int32 siteid)
		{
			try
			{
				SqlParameter[] arParams1 = new SqlParameter[2];
				arParams1[0] = new SqlParameter("@code", SqlDbType.VarChar);
				arParams1[0].Value = code;
				arParams1[1] = new SqlParameter("@siteid", SqlDbType.BigInt);
				arParams1[1].Value = siteid;
				String sql = "usp_get_discount";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
				if (dt.Rows.Count > 0)
				{
					return Convert.ToDecimal(dt.Rows[0][0]);
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getdisocunt Common");
				return 0;
			}
		}

		public static void addvoucher(String vcode, String sessionid)
		{
			try
			{
				SqlParameter[] arParams1 = new SqlParameter[2];
				arParams1[0] = new SqlParameter("@sessionid", SqlDbType.VarChar);
				arParams1[0].Value = sessionid;
				arParams1[1] = new SqlParameter("@voucher", SqlDbType.VarChar);
				arParams1[1].Value = vcode;
				String sql = "usp_insert_voucher_session";
				int i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1);
				
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getdisocunt Common");                
			}
		}

		public static string getsitename(Int32 siteid)
		{
			try
			{
				if (siteid == 1)
				{
					return "Party Superstores Clapham";
				}
				if (siteid == 2)
				{
					return "Party Superstores Sutton";
				}
				if (siteid == 3)
				{
					return "Party Superstores Croydon";
				}
				else
				{
					return "Party Superstores Online";
				}                
			}
			catch {
				return "";
			}
		}

		public static String getfinaldelivery(Int32 et, Int32 postageid)
		{
			try
			{
				String curday = "";
				SqlParameter[] arParams1 = new SqlParameter[2];
				arParams1[0] = new SqlParameter("@et", SqlDbType.BigInt);
				arParams1[0].Value = et;
				arParams1[1] = new SqlParameter("@pid", SqlDbType.BigInt);
				arParams1[1].Value = postageid;
				String sql = "usp_get_estm_delivery";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
				if (dt.Rows.Count > 0)
				{
					curday = dt.Rows[0][0].ToString();
				}
				return curday;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex); ;
				logs.ErrorLog(ex.Message + " Line number- " + linenumber.ToString(), " dmessage");
				return "";
			}
		}

		public static String getfinaldeliverydate(Int32 et, Int32 postageid)
		{
			try
			{
				String curday = "";
				SqlParameter[] arParams1 = new SqlParameter[2];
				arParams1[0] = new SqlParameter("@et", SqlDbType.BigInt);
				arParams1[0].Value = et;
				arParams1[1] = new SqlParameter("@pid", SqlDbType.BigInt);
				arParams1[1].Value = postageid;
				String sql = "usp_get_estm_delivery";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
				if (dt.Rows.Count > 0)
				{
					curday = dt.Rows[0][1].ToString();
				}
				return curday;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex); ;
				logs.ErrorLog(ex.Message + " Line number- " + linenumber.ToString(), " dmessage");
				return "";
			}
		}

		public static string getemail(Int32 siteid)
		{
			try
			{
				String email = "online@partysuperstores.com";
				String sql = "Select email from site_master where siteid="+siteid;
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					email = dt.Rows[0][0].ToString();
				}
				return email;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " site model");
				return "";
			}
		}

		public static string RemoveSpecialCharacters(string str)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				foreach (char c in str)
				{
					if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == '@' || c == ' ' || c == '-' || c == '/')
					{
						sb.Append(c);
					}
				}
				return sb.ToString();
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " RemoveSpecialCharacters");
				return str;
			}
		}

		public static Decimal getfloatingdiscount(Decimal price)
		{
			try
			{
				SqlParameter[] arParams1 = new SqlParameter[1];
				arParams1[0] = new SqlParameter("@price", SqlDbType.Decimal);
				arParams1[0].Value = price;
				String sql = "usp_get_floating_discount";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
				if (dt.Rows.Count > 0)
				{
					return Convert.ToDecimal(dt.Rows[0][0]);
				}
				else
				{
					return 0;
				}
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getdisocunt Common");
				return 0;
			}
		}

		public static Decimal getdiscountamt()
		{
			try
			{
				Decimal discount = 0;
				String sql = "Select pdamount from postage_discount";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					discount = Convert.ToDecimal(dt.Rows[0][0]);
				}
				return discount;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getdiscountamt");
				return 0;
			}
		}

		public static String getdiscountpos()
		{
			try
			{
				String dispost = "Standard";
				String sql = "Select mtype from postage_master where isdiscount=1";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
				if (dt.Rows.Count > 0)
				{
					dispost = dt.Rows[0][0].ToString();
				}
				return dispost;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getdiscountamt");
				return "Standard";
			}
		}		

		public static int GetLineNumber(Exception ex)
		{
			var lineNumber = 0;
			const string lineSearch = ":line ";
			var index = ex.StackTrace.LastIndexOf(lineSearch);
			if (index != -1)
			{
				var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
				if (int.TryParse(lineNumberText, out lineNumber))
				{
				}
			}
			return lineNumber;
		}

		public static String dmessage()
		{
			try
			{
				String message = "";
				DateTime today = DateTime.Now;
				SqlParameter[] arParams1 = new SqlParameter[1];
				arParams1[0] = new SqlParameter("@siteid", SqlDbType.BigInt);
				arParams1[0].Value = Common.siteid;
				String sql = "usp_get_delivery_message";
				DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
				if (dt.Rows.Count > 0)
				{
					DateTime stdate = Convert.ToDateTime(dt.Rows[0][1]);
					DateTime endate = Convert.ToDateTime(dt.Rows[0][2]);
					Int32 st = DateTime.Compare(today,stdate);
					Int32 en = DateTime.Compare(today,endate);
					if ((st >= 0) && (en <= 0))
					{
						message = dt.Rows[0][0].ToString();
					}
				}
				return message;
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " dmessage");
				return "";
			}
		}
				
		public static string MD5Hash(string text)
		{
			try
			{
				MD5 md5 = new MD5CryptoServiceProvider();                
				md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
				byte[] result = md5.Hash;
				StringBuilder strBuilder = new StringBuilder();
				for (int i = 0; i < result.Length; i++)
				{
					strBuilder.Append(result[i].ToString("x2"));
				}
				return strBuilder.ToString();
			}
			catch (Exception ex)
			{
				Int32 linenumber = Common.GetLineNumber(ex);
				logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " MD5Hash Common");
				return null;
			}
		}	

		public static Int32 getlevel(Int32 code)
		{
			Int32 level=0;
			if ((code%1000000)==0) level=1; 
			else if(((code%1000000)%1000)==0) level=2;
			else level=3;
			return level;
		}	       
		
	}   
}
