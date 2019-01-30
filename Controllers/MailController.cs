using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;
using wigsboot.Models;

namespace wigsboot.Controllers
{
    public class MailController : MailerBase
    {
        public EmailResult Invoice(order model)
        {
            To.Add(model.custemail);
            From = Common.salesemail;
            Subject = "Your order : " + model.orderid + " from " + Common.sitename + " generated Successfully";
            return Email("Invoice", model);
        }

        public EmailResult StaffInvoice(order model)
        {
            To.Add(Common.orderemail);
            CC.Add(Common.ordercc);
            From = Common.salesemail;            
            Subject = model.postname + "- Order : " + model.orderid + " of " + Common.sitename;
            return Email("StaffInvoice", model);
        }

        public EmailResult ReturnInvoice(order model)
        {
            To.Add(model.custemail);
            From = Common.salesemail;
            Subject = "Your Refund : " + model.orderid + " from " + Common.sitename + " generated Successfully";
            return Email("ReturnInvoice", model);
        }
    }
}
