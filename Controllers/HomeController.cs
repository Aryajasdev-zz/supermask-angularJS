using System;
using System.Web.Mvc;
using wigsboot.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;
using RealexPayments.RealAuth;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wigsboot.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String url)
        {
            try
            {                
                return View();
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - " + url + " - Line number " + linenumber.ToString(), "Index");
                return View();
            }
        }

        public JsonResult GetAllproducts(String url, String apikey, Int32? siteid)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    Int32 catcode = cats.getcatcode(url);
                    Int32 ncatcode = Common.nextcat(catcode);
                    List<products> prod = products.getprods(catcode, ncatcode, 0, Convert.ToInt32(siteid)).ToList();
                    return new JsonResult()
                    {
                        ContentEncoding = Encoding.Default,
                        ContentType = "application/json",
                        Data = prod,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        MaxJsonLength = int.MaxValue
                    };
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult searchProducts(String url, String apikey, String q)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    Int32 catcode = cats.getcatcode(url);
                    Int32 ncatcode = Common.nextcat(catcode);
                    if (catcode > 0)
                    {
                        List<products> prod = products.searchitems(q, catcode, ncatcode).ToList();
                        return new JsonResult()
                        {
                            ContentEncoding = Encoding.Default,
                            ContentType = "application/json",
                            Data = prod,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                            MaxJsonLength = int.MaxValue
                        };
                    }
                    else
                    {
                        List<products> prod = products.searchitems(q).ToList();
                        return new JsonResult()
                        {
                            ContentEncoding = Encoding.Default,
                            ContentType = "application/json",
                            Data = prod,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                            MaxJsonLength = int.MaxValue
                        };
                    }
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Getproductbyid(String prodid, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    products prod = products.getproduct(Convert.ToInt32(prodid));
                    return Json(prod, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {

                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetproductbyGUID(String GUID, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    products prod = products.getproduct(GUID);
                    return Json(prod, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {

                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getproductbyurl(String url, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    products prod = products.getprod(url);
                    return Json(prod, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getcategories(String url, String siteid, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    Int32 lv = cats.getlvl(url);
                    List<cats> cat = cats.getcats(url, lv, Convert.ToInt32(siteid)).ToList();
                    return Json(cat, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getallcategories(String url, String siteid, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    Int32 lv = cats.getlvl(url);
                    List<categories> cat = cats.getallcats(url, lv, Convert.ToInt32(siteid)).ToList();
                    return Json(cat, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getmaincategories(String apikey, Int32? siteid)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    List<maincats> cat = maincats.getmaincats(Convert.ToInt32(siteid)).ToList();
                    return Json(cat, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getpostage(String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    List<postage> post = postage.getpostage().ToList();
                    return Json(post, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getheaders(String code, String siteid, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    List<header> head = header.getheader(Convert.ToInt32(code), Convert.ToInt32(siteid)).ToList();
                    return Json(head, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult posttobasket(string itemdata, string apikey, String billdata, String deldata, String type, Int32 siteid)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    String orderid = basket.addjsonbaskets(itemdata, billdata, deldata, type, siteid);
                    return Json(orderid, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult postpaypal(string payid, String orderid, string apikey, Int32 siteid)
        {
            try
            {
                response res = new response();
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    Common.setpaypal(payid, orderid);
                    Int32 infoid = paypalcheck.createorderreal(orderid);
                    res.resultcode = 1;
                    res.infoid = infoid.ToString();
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    res.resultcode = 0;
                    res.infoid = "Fail";
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getRealcode(string apikey, String orderid)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    realpay rxp = basket.getrealinfo(orderid);
                    return Json(rxp, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getInvoice(String orderid, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    order ord = order.getorder(orderid, 1);
                    return Json(ord, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult getbasketSuccess(String orderid, String apikey)
        {
            try
            {
                Boolean isapi = Common.checkapi(apikey);
                if (isapi)
                {
                    List<basket> bask = basket.getbasketSuccess(orderid).ToList();
                    return Json(bask, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getOrderID()
        {
            Random rnd = new Random();
            String timestamp = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + rnd.Next(100, 999);
            return Json(timestamp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getRealexsecret(Decimal amt, String orderid)
        {
            try
            {
                String timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                Int32 amount = Convert.ToInt32(amt * 100);
                String tmp = timestamp + '.' + Common.merchantid + '.' + orderid + '.' + amount + '.' + Common.curr;
                String md5hash = Common.SHA1Hash(tmp);
                tmp = md5hash + '.' + Common.secret;
                md5hash = Common.SHA1Hash(tmp);
                return Json(md5hash, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }

        public ActionResult getdiscount(String code, Int32? siteid)
        {
            try
            {
                Decimal disc = Common.getdiscount(code, Convert.ToInt32(siteid));
                return Json(disc, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(0, JsonRequestBehavior.AllowGet); ;
            }
        }

        [HttpPost]
        public ActionResult getppostage(Decimal totamt, String code)
        {
            try
            {
                IEnumerable<postage> post = postage.getppostage(totamt, code).ToList();
                return Json(post, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "getpostage");
                return View();
            }
        }

        [HttpPost]
        public ActionResult getpromotion(String code)
        {
            try
            {
                IEnumerable<promotion> post = promotion.getpromotion(code).ToList();
                return Json(post, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "getpromotion");
                return View();
            }
        }

        //Response and Generation of Invoice for RealEx Api       
        public ActionResult realpayment(String carddata, String orderID, String total)
        {
            try
            {
                String err = "";
                response res = new response();
                bool islive = Convert.ToBoolean(ConfigurationManager.AppSettings["islive"]);
                site st = new site();
                String realcode = st.realcode;
                if (islive == false)
                {
                    realcode = "internettest";
                }
                ViewBag.Title = "Success";
                var card = JObject.Parse(carddata);
                String cardno = card["cardNo"].ToString();
                String ctype = "VISA";
                String scode = card["cardCvv"].ToString();
                String expires = card["cardExpires"].ToString();
                String smonth = expires.Split('/')[0];
                String syear = expires.Split('/')[1];
                String name = card["cardName"].ToString();
                Int32 Amount = Convert.ToInt32(Convert.ToDecimal(total) * 100);
                UInt32 amount = Convert.ToUInt32(Amount.ToString());
                CreditCard cReq = new CreditCard(ctype, cardno, smonth + syear, name, scode, CreditCard.CVN_PRESENT);
                TransactionRequest tReq = new TransactionRequest("partysuperstores", "GU7QNTcCtF", "GU7QNTcCtF", "GU7QNTcCtF");
                TransactionResponse tResp = tReq.Authorize(realcode, orderID, "GBP", amount, cReq);
                if (tResp.ResultCode == 0)
                {
                    String orderid = tResp.ResultOrderID;
                    Int32 infoid = paypalcheck.createorderreal(orderid);
                    res.resultcode = 1;
                    res.infoid = infoid.ToString();
                    return Json(res, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    Int32 reslt = tResp.ResultCode;
                    if (reslt >= 101 && reslt < 108)
                    {	//possible fraud
                        err = "You payment was not authorised by your bank.</br>This could be due to incorrect card details, or insufficient funds.";
                    }
                    else if (reslt < 301 || reslt < 101)
                    { // comms failure
                        err = "We could not connect to your bank</br>Please try again in a few minutes' time.</br>If you have further problems with the payment system, you can place your order over the telephone instead by calling us on</br>" + Common.telephone;
                    }
                    else if (reslt < 306)
                    {
                        err = "We could not process your order because our payment gateway service was experiencing difficulties.</br>You can place the order over the telephone instead by calling us on </br>" + Common.telephone;
                    }
                    else if (reslt == 501)
                    {
                        err = "A transaction with this unique number has already been processed.";
                    }
                    else if (reslt < 600)
                    {
                        err = "Some data sent to Realex/PayAndShop could not be validated.</br>Please try again in a few minutes' time.</br>If this problem persists, contact us on</br>." + Common.telephone;
                    }
                    else
                    {
                        err = "The credit card transaction didn't complete properly.</br>Please contact us on" + st.sitephone + ", with the date and time of your order and we will investigate the matter.";
                    }
                }
                err = err + "<br><br>You can always return to " + st.sitename + " to try an alternative method of payment.";
                res.resultcode = 0;
                res.infoid = err;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "realpayment");
                return null;
            }
        }

        //form Submission for RealEx HPP
        [HttpPost]
        public ActionResult realexpay(FormCollection fc)
        {
            try
            {
                order ppp = new order();
                Random rnd = new Random();
                String timestamp = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + rnd.Next(100, 999);
                String ctitle = "";
                String issame = "Yes";
                String delname = "";
                String deladd = "";
                String delcon = "";
                try
                {
                    ctitle = fc["cardTitle"].ToString();
                    issame = fc["deliverySame"].ToString();
                    delname = fc["deliveryName"].ToString();
                    delcon = fc["deliveryContact"].ToString();
                    deladd = fc["deliveryAddress"].ToString() + "\r\n<br/>" + fc["deliveryPostcode"].ToString();
                    deladd = delname + "\r\n<br/>" + "\r\n<br/>" + deladd;
                }
                catch { }
                ppp.orderid = timestamp;
                ppp.name = fc["cardName"].ToString();
                ppp.postageid = Convert.ToInt32(Session["pid"]);
                ppp.postage = Convert.ToDecimal(Session["pamt"]);
                ppp.discountcode = fc["dtxtcode"].ToString();
                try
                {
                    ppp.discount = Convert.ToDecimal(fc["txtdis"].ToString()); ;
                }
                catch
                {
                    ppp.discount = 0;
                }
                ppp.donation = 0;
                ppp.totamt = Convert.ToDecimal(Session["totamt"]);
                ppp.amount = decimal.Round((Convert.ToDecimal(Session["totamt"]) + Convert.ToDecimal(ppp.postage)) - (Convert.ToDecimal(ppp.discount)), 2);
                ppp.siteid = Common.siteid;
                ppp.ptype = "realex";
                ppp.istele = 0;
                ppp.custadd = ctitle + " " + ppp.name + "\r\n<br/>" + fc["cardAddress"].ToString() + "\r\n<br/>" + fc["cardPostcode"].ToString(); ;
                ppp.custemail = fc["email"].ToString();
                ppp.custphone = fc["phone"].ToString();
                ppp.code = fc["cardPostcode"].ToString();
                ppp.ismob = Convert.ToInt32(fc["ismob"]);
                if (issame == "Yes")
                {
                    ppp.deladd = ppp.custadd;
                }
                else
                {
                    ppp.deladd = deladd;
                }
                if (Convert.ToDecimal(ppp.amount) <= ppp.postage)
                {
                    return View("Index");
                }
                else
                {
                    ppp.generateorder(Session.SessionID.ToString());
                    return View(ppp);
                }
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "realpay");
                return View("checkout");
            }
        }

        //Receiving Response and Generating Invoice for RealEx HPP
        public void Confirm()
        {
            try
            {
                String timestamp = Request["TIMESTAMP"];
                String result = Request["RESULT"];
                String orderid = Request["ORDER_ID"];
                String message = Request["MESSAGE"];
                String authcode = Request["AUTHCODE"];
                String pasref = Request["PASREF"];
                String sha1hash = Request["SHA1HASH"];
                String siteid = Request["SITE"];
                Common.confirmorder(timestamp, result, orderid, message, authcode, sha1hash);
                String temp = timestamp + "." + Common.merchantidtest + "." + orderid + "." + result + "." + message + "." + pasref + "." + authcode;
                String temp1 = Common.SHA1Hash(temp);
                String temp2 = temp1 + "." + Common.secrettest;
                String hash = Common.SHA1Hash(temp2);
                if (hash != sha1hash)
                {
                    var err = "<h1>" + Common.sitename + "<br><br>Order " + orderid + "</h1><br>There was a problem connecting to our order processing server ('Hash Mismatch').";
                    result = "noAuth";
                    logs.ErrorLog(orderid + " hash mismatch error occured", "Confirm");
                }
                else if (result == "00")
                {
                    Int32 infoid = paypalcheck.createorderreal(orderid);
                    if (infoid > 0)
                    {
                        order ord = order.getorder(orderid, 1);
                        var viewstring = RenderViewToString("Success", ord);
                        /*try
                        {
                            new MailController().Invoice(ord).DeliverAsync();
                        }
                        catch (Exception ex)
                        {
                            logs.ErrorLog(ex.Message, "Confirm");
                        }
                        try
                        {
                            new MailController().StaffInvoice(ord).DeliverAsync();
                        }
                        catch (Exception ex)
                        {
                            logs.ErrorLog(ex.Message, "Confirm");
                        }*/
                        Response.Write(viewstring);
                    }
                }
                else
                {
                    site st = new site();
                    String err = "";
                    if (result != "noAuth")
                    {
                        Int32 reslt = Convert.ToInt32(result);
                        if (reslt >= 101 && reslt < 108)
                        {	//possible fraud
                            err = "<b>You payment was not authorised by your bank.</b><br><br>This could be due to incorrect card details, or insufficient funds.";
                        }
                        else if (reslt < 301 || reslt < 101)
                        { // comms failure
                            err = "<b>We could not connect to your bank</b><br><br>Please try again in a few minutes' time.<br><br>If you have further problems with the payment system, you can place your order over the telephone instead by calling us on<br><br>." + Common.telephone;
                        }
                        else if (reslt < 306)
                        {
                            err = "<b>We could not process your order because our payment gateway service was experiencing difficulties.</b><br><br>You can place the order over the telephone instead by calling us on <br><br>" + Common.telephone; ;
                        }
                        else if (reslt == 501)
                        {
                            err = "<b>A transaction with this unique number has already been processed.</b>.";
                        }
                        else if (reslt < 600)
                        {
                            err = "<b>Some data sent to Realex/PayAndShop could not be validated.</b><br><br>Please try again in a few minutes' time.<br><br>If this problem persists, contact us on<br><br>." + Common.telephone; ;
                        }
                        else
                        {
                            err = "<b>The credit card transaction didn't complete properly.</b><br><br>Please contact us on" + st.sitephone + ", with the date and time of your order and we will investigate the matter.";
                        }
                    }
                    err = err + "<br><br>You can always return to " + st.sitename + " to try an alternative method of payment.";
                    Response.Write(err);
                }
            }
            catch (Exception ex)
            {
                logs.ErrorLog(ex.Message + " - Confirm Session" + Session.SessionID, "Confirm");
                Response.Write("Some Error occured in Confirming order");
            }
        }

        //Receiving IPN from Paypal
        [HttpPost]
        public void IPN()
        {
            try
            {
                bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
                var formVals = new Dictionary<string, string>();
                formVals.Add("cmd", "_notify-validate");
                string response = GetPayPalResponse(formVals, useSandbox);
                if (response == "VERIFIED")
                {
                    paypalcheck pay = new paypalcheck();
                    pay.sAmountPaid = Convert.ToDecimal(Request["mc_gross"]);
                    pay.paymentStatus = Request["payment_status"];
                    pay.orderid = Request["custom"];
                    if (pay.paymentStatus == "Completed")
                    {
                        pay.buyerEmail = Request["payer_email"];
                        pay.transactionID = Request["txn_id"];
                        pay.firstName = Request["first_name"];
                        pay.lastName = Request["last_name"];
                        pay.address_name = Request["address_name"];
                        pay.custstreet = Request["address_street"];
                        pay.custcity = Request["address_city"];
                        pay.custcountry = Request["address_country"];
                        pay.custphone = Request["contact_phone"];
                        pay.custpcode = Request["address_zip"];
                        Int32 infoid = pay.createorder();
                        if (infoid > 0)
                        {
                            order ord = order.getorder(pay.orderid, 1);
                            /*                           
                            try
                            {
                                new MailController().Invoice(ord).DeliverAsync();
                            }
                            catch (Exception ex)
                            {
                                logs.ErrorLog(ex.Message, "IPN");
                            }
                            try
                            {
                                new MailController().StaffInvoice(ord).DeliverAsync();
                            }
                            catch (Exception ex)
                            {
                                logs.ErrorLog(ex.Message, "IPN");
                            }*/
                        }
                    }
                    else
                    {
                        logs.ErrorLog(pay.orderid + " has not been completed, Please check", "IPN");
                        this.sendmail("Partysuperstores", Common.emergencyemail, "Error in generating invoice " + pay.orderid + " - " + pay.paymentStatus, "Error in generating invoice " + pay.orderid + " - " + pay.paymentStatus);
                    }
                }
                else if (response == "INVALID")
                {
                    this.sendmail("Partysuperstores", Common.emergencyemail, "Error in generating paypal invoice " + response, "Error in generating paypal invoice " + response);
                    logs.ErrorLog("Invalid Response of paypal order in IPN", "IPN");
                }
                else
                {
                    this.sendmail("Partysuperstores", Common.emergencyemail, "Error in generating paypal invoice check paypal transaction", "Error in generating paypal invoice check paypal transaction");
                    logs.ErrorLog("There is some error generating paypal order IPN", "IPN");
                }
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "IPN");
            }
        }

        //Generate Invoice       
        public ActionResult Generateinvoice(String orderid)
        {
            try
            {
                ViewBag.Title = "Success";
                order ord = order.getorder(orderid, 1);
                try
                {
                    new MailController().Invoice(ord).DeliverAsync();
                }
                catch (Exception ex)
                {
                    logs.ErrorLog(ex.Message, "GenerateInvoice");
                }
                try
                {
                    new MailController().StaffInvoice(ord).DeliverAsync();
                }
                catch (Exception ex)
                {
                    logs.ErrorLog(ex.Message, "GenerateInvoice");
                }
                return View("invoice", ord);
            }
            catch
            {
                return View("Error");
            }
        }

        //Generate Return Invoice
        public ActionResult GenerateReturn(String orderid)
        {
            try
            {
                ViewBag.Title = "Success";
                order ord = order.getorder(orderid, 1);
                try
                {
                    new MailController().ReturnInvoice(ord).DeliverAsync();
                }
                catch (Exception ex)
                {
                    logs.ErrorLog(ex.Message, "GenerateInvoice");
                }
                return View("return", ord);
            }
            catch
            {
                return View("Error");
            }
        }

        //Generate Return Invoice View
        public ActionResult returninv(String orderid)
        {
            try
            {
                ViewBag.Title = "Success";
                order ord = order.getorder(orderid, 1);
                return View("return", ord);
            }
            catch
            {
                return View("Error");
            }
        }

        //Generate Staff Invoice
        public ActionResult staffinvoice(String orderid)
        {
            try
            {
                ViewBag.Title = "Success";
                order ord = order.getorder(orderid, 1);
                return View("staffinvoice", ord);
            }
            catch
            {
                return View("Error");
            }
        }

        //Generate Customer Invoice
        public ActionResult invoice(String orderid)
        {
            try
            {
                ViewBag.Title = "Success";
                order ord = order.getorder(orderid, 1);
                return View("invoice", ord);
            }
            catch
            {
                return View("Error");
            }
        }

        // Render any View from string with view name and model
        private string RenderViewToString(string viewName, object model)
        {
            try
            {
                ViewData.Model = model;
                using (var sw = new StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                logs.ErrorLog(ex.Message + " - renderviewtostring Session" + Session.SessionID, "Renderviewtostring");
                return "";
            }
        }

        // Generating Success page after every sale
        public PartialViewResult Success(String cm)
        {
            try
            {
                ViewBag.orderid = cm;
                order ord = order.getorder(cm, 0);
                return PartialView("Success", ord);
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex); ;
                logs.ErrorLog(ex.Message + " Line number- " + linenumber.ToString(), "Success");
                return PartialView("Error");
            }
        }

        // Receiving Paypal response and decoding that for IPN
        private string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {
            try
            {
                string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);
                // Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = Request.BinaryRead(Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);

                StringBuilder sb = new StringBuilder();
                sb.Append(strRequest);

                foreach (string key in formVals.Keys)
                {
                    sb.AppendFormat("&{0}={1}", key, formVals[key]);
                }
                strRequest += sb.ToString();
                req.ContentLength = strRequest.Length;
                string response = "";
                using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
                {
                    streamOut.Write(strRequest);
                    streamOut.Close();
                    using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        response = streamIn.ReadToEnd();
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                logs.ErrorLog(ex.Message + " - GetPayPalResponse Session" + Session.SessionID, "Getpaypalresponse");
                return null;
            }
        }

        // Cancel page if payment is cancelled by customer
        public ActionResult Cancel()
        {
            try
            {
                ViewBag.orderid = Request["cm"];
                return PartialView("Cancel");
            }
            catch (Exception ex)
            {
                logs.ErrorLog(ex.Message + " - Cancel Session" + Session.SessionID, "Cancel");
                return PartialView("Error");
            }
        }

        //Send Comment to us by customer by email
        public void sendcomment(String to, String subject, String message)
        {
            try
            {
                sendmail("Database Comments", to, subject, message);
            }
            catch (Exception ex)
            {
                logs.ErrorLog(ex.Message + " - " + Session.SessionID, "sendcomment");
            }
        }

        //Generating Captcha image to prevent boots
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10000, 99999);
            var captcha = string.Format("{0}", a);

            //store answer 
            Session["Captcha"] = captcha;
            //image stream 
            FileContentResult img = null;
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(230, 50))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (230 / 5));
                        x = rand.Next(0, 230);
                        y = rand.Next(0, 50);
                        float xx = x - r;
                        float yy = y - r;
                        gfx.DrawEllipse(pen, xx, yy, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new System.Drawing.Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }
            return img;
        }

        // Submiting Captcha image and contact form for customers
        public String captcha(String name, String email, String subject, String captcha, Int32 site)
        {
            try
            {
                String error = "";
                String sitename = Common.getsitename(site);
                String siteemail = Common.getemail(site);
                String scaptcha = "";
                if (site == 1) scaptcha = Session["Captchaclap"].ToString();
                else if (site == 2) scaptcha = Session["Captchasutt"].ToString();
                else if (site == 3) scaptcha = Session["Captchacroy"].ToString();
                else if (site == 8) scaptcha = Session["Captchaonline"].ToString();
                else
                {
                    scaptcha = Session["Captcha"].ToString();
                }
                if (captcha == scaptcha)
                {
                    error = "Thankyou, your enquiry has been submitted!";
                    message msg = new message(name, email, subject, site);
                    this.sendcmail(name, email, siteemail, subject, subject);
                    this.sendcmail(sitename, siteemail, email, subject, "Thanks for your enquiry, we will get back to you asap");
                }
                else
                {
                    error = "Error: captcha is not valid.";
                }
                return error;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex); ;
                logs.ErrorLog(ex.Message + " Line number- " + linenumber.ToString(), "captcha");
                return ex.Message;
            }
        }

        //Sending email to customers
        private void sendcmail(String name, String from, String to, String subject, String message)
        {
            try
            {
                var loginInfo = new NetworkCredential(Common.salesemail, Common.password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient(Common.smtp, Common.port);

                msg.From = new MailAddress(from, name);
                msg.To.Add(new MailAddress(to));
                msg.Subject = subject;
                msg.Body = HttpUtility.HtmlDecode(message);
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                logs.ErrorLog(ex.Message + " - sendmail Session" + Session.SessionID, "Sendmail");
            }
        }

        // Sending email to customers
        private void sendmail(String name, String to, String subject, String message)
        {
            try
            {
                var loginInfo = new NetworkCredential(Common.salesemail, Common.password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient(Common.smtp, Common.port);

                msg.From = new MailAddress(Common.salesemail, name);
                msg.To.Add(new MailAddress(to));
                msg.Subject = subject;
                msg.Body = HttpUtility.HtmlDecode(message);
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                logs.ErrorLog(ex.Message + " - sendmail Session" + Session.SessionID, "Sendmail");
            }
        }


    }
}
