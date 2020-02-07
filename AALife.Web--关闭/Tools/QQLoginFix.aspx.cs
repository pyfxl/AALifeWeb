using AALife.BLL;
using AALife.Model;
using NS_OpenApiV3;
using NS_SnsNetWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_QQLoginFix : FirstPage
{
    private OAuthTableBLL bll = new OAuthTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void GetQQImageButton_Click(object sender, EventArgs e)
    {
        if (this.AppID.Text.Trim() == "" || this.AppKey.Text.Trim() == "" || this.OpenIDBox.Text.Trim() == "" || this.AccessTokenBox.Text.Trim() == "")
        {
            this.ResultLabel.Text = "{ empty. }";
            return;
        }

        int appid = Convert.ToInt32(this.AppID.Text.Trim());
        string appkey = this.AppKey.Text.Trim();
        string server_name = "openapi.tencentyun.com";//"119.147.19.43";
        string openid = this.OpenIDBox.Text.Trim();
        string openkey = this.AccessTokenBox.Text.Trim();

        OpenApiV3 sdk = new OpenApiV3(appid, appkey);
        sdk.SetServerName(server_name);
        RstArray result = new RstArray();

        //get_info接口
        result = UserHelper.GetUserInfo(sdk, openid, openkey, "qzone");

        //测试
        //Response.Write("<br>ret = " + result.Ret + "<br>msg = " + result.Msg);

        string jsonString = result.Msg;
        QQInfoClass qq = JsonHelper.JsonDeserialize<QQInfoClass>(jsonString);

        string str = "";
        OAuthInfo oauth = bll.GetOAuthByOpenId(openid);
        if (FixImageBox.Checked && oauth.OAuthID > 0)
        {
            int userId = oauth.UserID;
            UserInfo user = user_bll.GetUserByUserId(userId);
            user.UserNickName = qq.nickname;
            user.UserImage = qq.figureurl;
            //user.ModifyDate = DateTime.Now;

            bool success = user_bll.UpdateUser(user);
            if (success)
            {
                str = "{ " + user.UserImage + " }";
            }
            else
            {
                str = "{ error. }";
            }

        }

        this.ResultLabel.Text = str + "<br><br>" + jsonString;
    }
    
}