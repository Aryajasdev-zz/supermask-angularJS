using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace wigsboot
{
    public class requirehttps : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p>Please use HTTPS</p>");
                UriBuilder ub = new UriBuilder(actionContext.Request.RequestUri);
                ub.Scheme = Uri.UriSchemeHttps;
                ub.Port = 44320;
                actionContext.Response.Headers.Location = ub.Uri;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}