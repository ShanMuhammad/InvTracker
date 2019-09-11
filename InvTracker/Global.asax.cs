using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InvTracker
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    const string OPTIONS = "OPTIONS";
        //    const string GET = "GET";
        //    const string POST = "POST";
        //    string[] AllowedVerbs = new[] { OPTIONS, GET, POST };
        //    const string Origin = "Origin";
        //    const string Referer = "Referer";
        //    const string AccessControlRequestMethod = "Access-Control-Request-Method";
        //    const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        //    const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        //    const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        //    const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        //    const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";

        //    var request = Context.Request;
        //    var response = Context.Response;
        //    var originArray = request.Headers.GetValues(Origin);

        //    if (originArray == null)
        //    {
        //        originArray = request.Headers.GetValues(Referer);
        //    }

        //    //if (requestOrigin.Length > 0)
        //    //{
        //    //    if (originArray == null || originArray[0].Trim().Contains(requestOrigin) == false)
        //    //    {
        //    //        Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //    //        Context.Response.End();
        //    //    }
        //    //}
        //    if (Array.Exists(AllowedVerbs, av => string.Compare(Context.Request.HttpMethod, av, true) == 0))
        //    {
        //        var accessControlRequestMethodArray = request.Headers.GetValues(AccessControlRequestMethod);
        //        var accessControlRequestHeadersArray = request.Headers.GetValues(AccessControlRequestHeaders);
        //        if (originArray != null && originArray.Length > 0)
        //        {
        //            response.AddHeader(AccessControlAllowOrigin, originArray[0].Trim());
        //        }
        //        else
        //        {
        //            response.AddHeader(AccessControlAllowOrigin, "*");
        //        }
        //        response.AddHeader(AccessControlAllowCredentials, bool.TrueString.ToLower());

        //        if (accessControlRequestMethodArray != null &&
        //            accessControlRequestMethodArray.Length > 0)
        //        {
        //            string accessControlRequestMethod = accessControlRequestMethodArray[0];
        //            if (!string.IsNullOrEmpty(accessControlRequestMethod))
        //            {
        //                response.AddHeader(AccessControlAllowMethods, accessControlRequestMethod);
        //            }
        //        }
        //        if (accessControlRequestHeadersArray != null &&
        //            accessControlRequestHeadersArray.Length > 0)
        //        {
        //            string requestedHeaders = string.Join(", ", accessControlRequestHeadersArray);
        //            if (!string.IsNullOrEmpty(requestedHeaders))
        //            {
        //                response.AddHeader(AccessControlAllowHeaders, requestedHeaders);
        //            }
        //        }
        //    }
        //    if (Context.Request.HttpMethod == OPTIONS)
        //    {
        //        try
        //        {
        //            Context.Response.StatusCode = (int)HttpStatusCode.OK;
        //            Context.Response.Flush();
        //            HttpContext.Current.ApplicationInstance.CompleteRequest();
        //        }
        //        catch
        //        {
        //            Context.Response.End();
        //        }
        //    }

        //}
    }
}
