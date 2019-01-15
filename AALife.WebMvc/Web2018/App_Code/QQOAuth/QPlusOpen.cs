using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// Q+ OpenID的帮助对象，用于应用后台验证用户登录状态，获取用户资料。
/// </summary>
public class QPlusOpen
{
    //SDK版本号
    public const string VERSION = "1.0.1";
    public const string SERVER_NAME = "http://openid.qplus.com/";

    private string _AppID;
    public string AppID { get { return _AppID; } set { _AppID = value; } }
    private string _AppSecret;
    public string AppSecret { get { return _AppSecret; } set { _AppSecret = value; } }
    private string _AppLang;
    public string AppLang { get { return _AppLang; } set { _AppLang = value; } }

    /// <summary>
    /// 创建Q+ OpenID的帮助对象
    /// </summary>
    /// <param name="appid">应用的AppID</param>
    /// <param name="appsecret">应用的AppSecret</param>
    /// <param name="applang">应用的语言代码。2052是简体中文，1033是英文</param>
    public QPlusOpen(string appid, string appsecret, string applang)
    {
        this.AppID = appid;
        this.AppSecret = appsecret;
        this.AppLang = applang;
        //this.Request = request;
    }
    public string MakeRequest(string cgi, SortedList<string, string> list, HttpRequest httpRequest)
    {
        string test = this.TestSever();
        string result = cgi;
        //组装url
        string url = SERVER_NAME + "cgi-bin/" + cgi;
        //组装参数
        if (list == null)
        {
            list = new SortedList<string, string>();
        }
        list["app_id"] = this.AppID;
        list["app_userip"] = httpRequest.UserHostAddress;
        list["app_nonce"] = GetNonce();
        list["app_ts"] = GetTimeStamp().ToString();
        //编码
        string[] pairs = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            pairs[i] = list.Keys[i] + "=" + System.Web.HttpUtility.UrlEncode(list.Values[i]);
        }
        //进行签名，组装url
        string param = string.Join("&", pairs);
        result = result + "&" + param;
        string sig = GetSHA1(result, this.AppSecret + "&");
        param = param + "&sig=" + sig;
        byte[] byteArray = Encoding.UTF8.GetBytes(param);
        System.Net.ServicePointManager.Expect100Continue = false;

        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        request.Credentials = CredentialCache.DefaultCredentials;

        request.Method = "POST";
        request.ContentLength = param.Length;
        request.ContentType = "application/x-www-form-urlencoded";
        request.Headers.Add("Cache-Control", "no-cache");

        System.IO.Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        string responseFromServer = "";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        System.IO.Stream responseData = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(responseData);
        // Read the content.
        responseFromServer = reader.ReadToEnd();
        // Display the content.
        Console.WriteLine(responseFromServer);
        // Clean up the streams.
        reader.Close();
        responseData.Close();
        response.Close();


        return responseFromServer;

    }
    public static string GetNonce()
    {
        string s = System.Guid.NewGuid().ToString();
        Guid g = System.Guid.NewGuid();
        System.Random rnd = new Random();

        StringBuilder sb = new StringBuilder(rnd.Next(65534).ToString(), 20);
        byte[] gs = g.ToByteArray();
        foreach (byte i in gs)
        {
            sb.Append(((int)i).ToString("x"));
        }
        if (sb.Length > 20)
        {
            s = sb.ToString().Substring(0, 20).ToLower();
        }
        else
        {
            s = sb.ToString();
        }
        return s;
    }
    public static long GetTimeStamp()
    {
        long time = System.DateTime.UtcNow.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
        time = time / 10000000;
        return time;
    }
    public static string GetSHA1(string text, string key)
    {
        UTF8Encoding utf8 = new UTF8Encoding();
        Encoding en = utf8;
        byte[] hashValue;
        byte[] message = en.GetBytes(text);

        byte[] k = en.GetBytes(key);

        HMACSHA1 hmac = new HMACSHA1(k, true);

        string hex = "";

        hashValue = hmac.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

        
    /// <summary>
    /// 验证从Q+跳转过来的信息是否正确
    /// </summary>
    /// <param name="request">Request对象</param>
    /// <returns></returns>
    public bool CheckSig(HttpRequest request)
    {
        string result = "";
            
        SortedList<string, string> list = new SortedList<string, string>();
        System.Collections.Specialized.NameValueCollection get
            = System.Web.HttpUtility.ParseQueryString(request.QueryString.ToString());
        foreach (string key in get.Keys)
        {
            list.Add(key, get[key]);
        }
            
        string sig = list["sig"];
        list.Remove("sig");

        result = http_build_query(list);
        return sig == GetSHA1(result, this.AppSecret + "&").ToUpper(); ;
    }

    private string http_build_query(SortedList<string, string> list)
    {
        string[] pairs = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            pairs[i] = list.Keys[i] + "=" + System.Web.HttpUtility.UrlEncode(list.Values[i]);
        }
        string param = string.Join("&", pairs);
        return param;
    }
    /// <summary>
    /// 获得Q+的用户资料
    /// </summary>
    /// <param name="appOpenID">用户的OpenID</param>
    /// <param name="appOpenKey">用户的OpenKey</param>
    /// <param name="request">Request对象</param>
    /// <returns>JSON字符串</returns>
    public string GetUserInfo(string appOpenID, string appOpenKey, HttpRequest request)
    {
        SortedList<string, string> list = new SortedList<string, string>();
        list["app_openid"] = appOpenID;
        list["app_openkey"] = appOpenKey;
        string result = this.MakeRequest("app_get_userinfo", list,request);
        return result;
    }
    /// <summary>
    /// 通过Q+服务器检查用户登录状态
    /// </summary>
    /// <param name="appOpenID">用户的OpenID</param>
    /// <param name="appOpenKey">用户的OpenKey</param>
    /// <param name="request">Request对象</param>
    /// <returns>JSON字符串，含义请参考Q+登录文档</returns>
    public string CheckLogin(string appOpenID, string appOpenKey, HttpRequest request)
    {
        SortedList<string, string> list = new SortedList<string, string>();
        list["app_openid"] = appOpenID;
        list["app_openkey"] = appOpenKey;
        string result = this.MakeRequest("app_verify", list, request);
        return result;
    }
    /// <summary>
    /// 生成登录参数，用于前端JavaScript调用qplus.user.auth方法的loginParam参数。
    /// </summary>
    /// <param name="request">Request对象</param>
    /// <returns>生成前端JavaScript调用qplus.user.auth方法的loginParam参数字符串</returns>
    public string GetLoginParam(HttpRequest request)
    {
        string result = "app_qqlogin";
        SortedList<string, string> list = new SortedList<string, string>();
        list["app_id"] = this.AppID;
        list["app_userip"] = request.UserHostAddress;
        list["app_nonce"] = GetNonce();
        list["app_ts"] = GetTimeStamp().ToString();
        list["app_lang"] = this.AppLang;
        //编码
        string[] pairs = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            pairs[i] = list.Keys[i] + "=" + System.Web.HttpUtility.UrlEncode(list.Values[i]);
        }
        //进行签名，组装url
        string param = string.Join("&", pairs);
        result = result + "&" + param;
        string sig = GetSHA1(result, this.AppSecret + "&");
        param = param + "&sig=" + sig;
        return param;
    }

    string TestSever()
    {
        WebClient web = new WebClient();
        return web.DownloadString(SERVER_NAME + "cgi-bin/app_verify");
    }
}