using System;
using System.Web;
//泛型
using System.Collections.Generic;
//sdk自定义类
using NS_OpenApiV3;
using NS_SnsNetWork;

public partial class AuthorLogin_PengYou : FirstPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int appid = 100651351;
        string appkey = "e358f5d6c4c5cd822419911c13a18e73";
        string server_name = "openapi.tencentyun.com";//"119.147.19.43";
        string openid = Request.QueryString["openid"];
        string openkey = Request.QueryString["openkey"];
        string pf = Request.QueryString["pf"];

        OpenApiV3 sdk = new OpenApiV3(appid, appkey);
        sdk.SetServerName(server_name);
        RstArray result = new RstArray();

        //get_info接口
        result = UserHelper.GetUserInfo(sdk, openid, openkey, pf);

        //测试
        //HttpContext.Current.Response.Write("<br>ret = " + result.Ret + "<br>msg = " + result.Msg);

        string jsonString = result.Msg;
        QQInfoClass qq = JsonHelper.JsonDeserialize<QQInfoClass>(jsonString);

        string u = "py";
        string oId = openid;
        string aToken = openkey;
        string userNickName = HttpUtility.UrlEncode(qq.nickname);
        string userImage = HttpUtility.UrlEncode(qq.figureurl);

        Response.Redirect("OAuth.aspx?u=" + u + "&openId=" + oId + "&accessToken=" + aToken + "&name=" + userNickName + "&image=" + userImage);
    }
    
}