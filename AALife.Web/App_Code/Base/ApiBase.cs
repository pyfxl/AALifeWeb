using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ApiBase 的摘要说明
/// </summary>
public class ApiBase : System.Web.UI.Page
{
    public ApiBase()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// Determines if GZip is supported
    /// </summary>
    /// <returns></returns>
    public static bool IsGZipSupported()
    {
        string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
        if (!string.IsNullOrEmpty(AcceptEncoding) &&
             (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate")))
            return true;
        return false;
    }

    /// <summary>
    /// Sets up the current page or handler to use GZip through a Response.Filter
    /// IMPORTANT:  
    /// You have to call this method before any output is generated!
    /// </summary>
    public static void GZipEncodePage()
    {
        if (IsGZipSupported())
        {
            HttpResponse Response = HttpContext.Current.Response;

            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (AcceptEncoding.Contains("gzip"))
            {
                Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,
                                          System.IO.Compression.CompressionMode.Compress);
                Response.AppendHeader("Content-Encoding", "gzip");
            }
            else
            {
                Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter,
                                          System.IO.Compression.CompressionMode.Compress);
                Response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}