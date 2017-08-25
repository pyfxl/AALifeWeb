<%@ WebHandler Language="C#" Class="SyncUserImage" %>

using System;
using System.Web;

public class SyncUserImage : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {

        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        int count = context.Request.Files.Count;

        for (int i = 0; i < count; i++) 
        {
            HttpPostedFile aFile = context.Request.Files[i];

            if (aFile.ContentLength == 0 || String.IsNullOrEmpty(aFile.FileName))
            {
                continue;
            }
            
            string displayFileName = System.IO.Path.GetFileName(HttpUtility.UrlDecode(aFile.FileName));
            string realFileName = System.Web.HttpContext.Current.Server.MapPath("/Backup/Cloud/") + displayFileName;
            string imageFileName = displayFileName;

            bool success = true;
            try
            {
                aFile.SaveAs(realFileName);
            }
            catch
            {
                success = false;
            }

            if (success)
            {
                context.Response.Write(1);
            }
            else
            {
                context.Response.Write(0);
            }
        }
        
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }
    
}