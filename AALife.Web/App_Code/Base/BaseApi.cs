using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// BaseApi 的摘要说明
/// </summary>
public class BaseApi : System.Web.UI.Page
{
    public BaseApi()
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
        HttpResponse Response = HttpContext.Current.Response;

        if (IsGZipSupported())
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (AcceptEncoding.Contains("gzip"))
            {
                Response.Filter = new System.IO.Compression.GZipStream(Response.Filter,
                                            System.IO.Compression.CompressionMode.Compress);
                Response.Headers.Remove("Content-Encoding");
                Response.AppendHeader("Content-Encoding", "gzip");
            }
            else
            {
                Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter,
                                            System.IO.Compression.CompressionMode.Compress);
                Response.Headers.Remove("Content-Encoding");
                Response.AppendHeader("Content-Encoding", "deflate");
            }
        }

        // Allow proxy servers to cache encoded and unencoded versions separately
        Response.AppendHeader("Vary", "Content-Encoding");
    }
    
    /// <summary>
    /// Simple routine to retrieve HTTP Content as a string with
    /// optional POST data and GZip encoding.
    /// </summary>
    /// <param name="Url"></param>
    /// <param name="PostData"></param>
    /// <param name="GZip"></param>
    /// <returns></returns>
    public string GetUrl(string Url, string PostData, bool GZip)
    {
        HttpWebRequest Http = (HttpWebRequest)WebRequest.Create(Url);

        if (GZip)
            Http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

        if (!string.IsNullOrEmpty(PostData))
        {
            Http.Method = "POST";
            byte[] lbPostBuffer = Encoding.Default.GetBytes(PostData);

            Http.ContentLength = lbPostBuffer.Length;

            Stream PostStream = Http.GetRequestStream();
            PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length);
            PostStream.Close();
        }

        HttpWebResponse WebResponse = (HttpWebResponse)Http.GetResponse();

        Stream responseStream = responseStream = WebResponse.GetResponseStream();
        if (WebResponse.ContentEncoding.ToLower().Contains("gzip"))
            responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
        else if (WebResponse.ContentEncoding.ToLower().Contains("deflate"))
            responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

        StreamReader Reader = new StreamReader(responseStream, Encoding.Default);

        string Html = Reader.ReadToEnd();

        WebResponse.Close();
        responseStream.Close();

        return Html;
    }
    
}