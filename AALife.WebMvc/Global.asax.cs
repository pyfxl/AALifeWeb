using AALife.Service.Tasks;
using AALife.WebMvc.JobBase;
using System;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AALife.WebMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //webapi返回json
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //解决ModelState提示字段必需问题 TODO
            //ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            //ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());

            try
            {
                //start scheduled tasks
                //TaskManager.Instance.Initialize();
                //TaskManager.Instance.Start();

                //quartz.net
                JobManager.Start();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.Request.FilePath == "/") Context.RewritePath("/Web2018/Default.aspx");
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            //log error
            LogException(exception);

            //process 404 HTTP errors
            var httpException = exception as HttpException;
            if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            {
                //Response.WriteFile("~/FileNotFound.htm");
                Response.Write(httpException.Message);
                Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                Response.End();
                Server.ClearError();
                return;
            }
        }

        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;

            //ignore 404 HTTP errors
            //var httpException = exc as HttpException;
            //if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            //    return;

            try
            {
                
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }
    }

}
