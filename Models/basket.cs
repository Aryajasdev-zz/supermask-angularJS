using Newtonsoft.Json.Linq;
using Sparsh.DataBaseConnectonCalss.Data;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wigsboot.Models
{
    public class realpay
    {
        public string realcode { get; set;}
        public string merchantid { get; set; }
        public string account { get; set; }
        public Int32 amount { get; set; }
        public string timestamp { get; set; }
        public string paypalemail { get; set; }
        public string orderid { get; set; }

    }
    public class basket
    {
        public Int32 prodid { get; set; }
        public string img { get; set; }
        public string prodname { get; set; }
        public string prodsize { get; set; }
        public Int32 qty { get; set; }
        public Decimal price { get; set; }
        public Int32 totqty { get; set; }
        public Decimal totamt { get; set; }
        public String url { get; set; }
        public String descr { get; set; }
        public Int32 zerovat { get; set; } 

        public static string addjsonbaskets(String json, String billing, String delivery, String type, Int32 siteid)
        {
            try
            {
                String name = "";
                Int32 prodid = 0;
                Int32 sizeid = 0;
                Decimal price = 0;
                Decimal total = 0;
                Int32 qty = 0;
                String sql = "";               
                Random rnd = new Random();
                String timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                String orderid = timestamp + "-" + rnd.Next(100, 999);
                SqlParameter[] arParams1;
                var mainObjects = JObject.Parse(json);
                var bill = JObject.Parse(billing);
                var del = JObject.Parse(delivery);
                var resultObjects = AllChildren(JObject.Parse(json)).First(c => c.Type == JTokenType.Array && c.Path.Contains("items")).Children<JObject>();
                try
                {
                    foreach (JObject basketdata in resultObjects)
                    {
                        prodid = Convert.ToInt32(basketdata["id"]);
                        name = basketdata["name"].ToString();
                        price = Convert.ToDecimal(basketdata["price"]);
                        qty = Convert.ToInt32(basketdata["quantity"]);
                        sizeid = Convert.ToInt32(basketdata["sizeid"]);
                        total = Convert.ToDecimal(basketdata["total"]);
                        arParams1 = new SqlParameter[6];
                        arParams1[0] = new SqlParameter("@bsession", SqlDbType.VarChar);
                        arParams1[0].Value = orderid;
                        arParams1[1] = new SqlParameter("@prodid", SqlDbType.BigInt);
                        arParams1[1].Value = prodid;
                        arParams1[2] = new SqlParameter("@price", SqlDbType.Decimal);
                        arParams1[2].Value = price;
                        arParams1[3] = new SqlParameter("@qty", SqlDbType.BigInt);
                        arParams1[3].Value = qty;
                        arParams1[4] = new SqlParameter("@sizeid", SqlDbType.BigInt);
                        arParams1[4].Value = (sizeid == 0) ? 1000000 : sizeid;
                        arParams1[5] = new SqlParameter("@siteid", SqlDbType.BigInt);
                        arParams1[5].Value = siteid;
                        sql = "usp_addprod_to_baskets";
                        DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                    }
                }
                catch { }
                Decimal postage = Convert.ToDecimal(mainObjects["postage"]);
                Int32 postageid = Convert.ToInt32(mainObjects["postageid"]);
                Decimal subtotal = Convert.ToDecimal(mainObjects["subTotal"]);
                Decimal totalcost = Convert.ToDecimal(mainObjects["totalCost"]);
                String cardname = "na";
                String cardHno = "";
                String cardadd = "";
                String cardCity = "";
                String cardCounty = "";               
                String cardpcode = "";
                String email = "";
                String phone = "";

                String delname = "";
                String delHno = "";
                String deladd = "";
                String delCity = "";
                String delCounty = "";                
                String delpcode = "";
                String delphone = "";

                if (bill.Count > 0)
                {
                    cardname = bill["cardName"].ToString();
                    cardHno = bill["cardHno"].ToString();
                    cardadd = bill["cardAddress"].ToString();
                    cardCity = bill["cardCity"].ToString();
                    cardCounty = bill["cardCounty"].ToString();                   
                    cardpcode = bill["cardPostcode"].ToString();
                    email = bill["email"].ToString();
                    phone = bill["phone"].ToString();
                }
                if (del.Count > 0)
                {
                    delname = del["cardName"].ToString();
                    delHno = del["cardHno"].ToString();
                    deladd = del["cardAddress"].ToString();
                    delCity = del["cardCity"].ToString();
                    delCounty = del["cardCounty"].ToString();                  
                    delpcode = del["cardPostcode"].ToString();
                    delphone = del["phone"].ToString();
                }               

                arParams1 = new SqlParameter[18];
                arParams1[0] = new SqlParameter("@orderid", SqlDbType.VarChar);
                arParams1[0].Value = orderid;
                arParams1[1] = new SqlParameter("@name", SqlDbType.VarChar);
                arParams1[1].Value = cardname;
                arParams1[2] = new SqlParameter("@custadd", SqlDbType.VarChar);
                arParams1[2].Value = cardname + "\r\n<br/>"+ cardHno + "\r\n<br/>" + cardadd + "\r\n<br/>" + cardCity + "\r\n<br/>"+cardCounty + "\r\n<br/>"+ cardpcode;
                arParams1[3] = new SqlParameter("@deladd", SqlDbType.VarChar);
                arParams1[3].Value = cardname + "\r\n<br/>" + delHno + "\r\n<br/>" + deladd + "\r\n<br/>" + delCity + "\r\n<br/>" + delCounty + "\r\n<br/>" + delpcode;
                arParams1[4] = new SqlParameter("@code", SqlDbType.VarChar);
                arParams1[4].Value = cardpcode;
                arParams1[5] = new SqlParameter("@custemail", SqlDbType.VarChar);
                arParams1[5].Value = email;
                arParams1[6] = new SqlParameter("@custphone", SqlDbType.VarChar);
                arParams1[6].Value = phone;
                arParams1[7] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[7].Value = siteid;
                arParams1[8] = new SqlParameter("@discountcode", SqlDbType.VarChar);
                arParams1[8].Value = "";
                arParams1[9] = new SqlParameter("@discount", SqlDbType.Decimal);
                arParams1[9].Value = 0;
                arParams1[10] = new SqlParameter("@donation", SqlDbType.Decimal);
                arParams1[10].Value = 0;
                arParams1[11] = new SqlParameter("@amount", SqlDbType.Decimal);
                arParams1[11].Value = totalcost;
                arParams1[12] = new SqlParameter("@postage", SqlDbType.Decimal);
                arParams1[12].Value = postage;
                arParams1[13] = new SqlParameter("@postageid", SqlDbType.BigInt);
                arParams1[13].Value = postageid;
                arParams1[14] = new SqlParameter("@ptype", SqlDbType.VarChar);
                arParams1[14].Value = type;
                arParams1[15] = new SqlParameter("@istele", SqlDbType.BigInt);
                arParams1[15].Value = 0;
                arParams1[16] = new SqlParameter("@session", SqlDbType.VarChar);
                arParams1[16].Value = orderid;
                arParams1[17] = new SqlParameter("@deviceid", SqlDbType.BigInt);
                arParams1[17].Value = 0;
                sql = "usp_insert_presale";
                Int32 i = DataBaseConnectionClass.ExecuteNonQuery(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1);
                return orderid;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null; 
            }
        }

        public static realpay getrealinfo(String orderid)
        {
            try
            {
                realpay rxp = new realpay();
                rxp.account = "internet";
                rxp.paypalemail = "arya2k7@gmail.com";
                String timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                Decimal totalcost = Common.getamt(orderid);
                rxp.amount = Convert.ToInt32(totalcost * 100);
                rxp.merchantid = Common.merchantidtest;
                String tmp = timestamp + '.' + Common.merchantidtest + '.' + orderid + '.' + rxp.amount + '.' + Common.curr;
                String md5hash = Common.SHA1Hash(tmp);
                tmp = md5hash + '.' + Common.secrettest;
                rxp.realcode = Common.SHA1Hash(tmp);
                rxp.timestamp = timestamp;
                rxp.orderid = orderid;
                return rxp;
            }
            catch {
                return null;
            }
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }  
       
        public static IEnumerable<basket> getbasketSuccess(String orderid)
        {
            try
            {
                String sql = "";
                DataTable dt = new DataTable();
                List<basket> bask = new List<basket>();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@orderid", SqlDbType.VarChar);
                arParams1[0].Value = orderid;
                sql = "usp_get_baskets_order_service";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    basket ct = new basket();
                    ct.prodid = Convert.ToInt32(dr[0]);
                    ct.img = "http://www.partysuperstores.co.uk/img/" + dr[1].ToString();
                    ct.prodname = dr[2].ToString();
                    ct.prodsize = dr[3].ToString();
                    ct.qty = Convert.ToInt32(dr[4]);
                    ct.price = Convert.ToDecimal(dr[5]);
                    ct.totqty = Convert.ToInt32(dr[7]);
                    ct.totamt = Convert.ToDecimal(dr[8]);
                    ct.url = dr[9].ToString();
                    ct.descr = dr[10].ToString();
                    ct.zerovat = Convert.ToInt32(dr[11]);
                    bask.Add(ct);
                }
                return bask;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getbasketSuccess model");
                return null;
            }
        }

        public static IEnumerable<basket> getbasketinfo(Int32 infoid)
        {
            try
            {
                String sql = "";
                DataTable dt = new DataTable();
                List<basket> bask = new List<basket>();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@infoid", SqlDbType.BigInt);
                arParams1[0].Value = infoid;
                sql = "usp_get_baskets_order_info";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    basket ct = new basket();
                    ct.prodid = Convert.ToInt32(dr[0]);
                    ct.img = "http://www.partysuperstores.co.uk/img/" + dr[1].ToString();
                    ct.prodname = dr[2].ToString();
                    ct.prodsize = dr[3].ToString();
                    ct.qty = Convert.ToInt32(dr[4]);
                    ct.price = Convert.ToDecimal(dr[5]);
                    ct.totqty = Convert.ToInt32(dr[7]);
                    ct.totamt = Convert.ToDecimal(dr[8]);
                    ct.url = dr[9].ToString();
                    ct.descr = dr[10].ToString();
                    ct.zerovat = Convert.ToInt32(dr[11]);
                    bask.Add(ct);
                }
                return bask;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getbasketinfo model");
                return null;
            }
        }      

    }
}