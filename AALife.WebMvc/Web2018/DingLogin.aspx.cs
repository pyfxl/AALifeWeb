using AALife.BLL;
using AALife.Model;
using AALife.Service;
using System;
using System.Collections.Generic;
using System.Web;

namespace AALife.WebMvc.Web2018
{
    public partial class DingLogin : FirstPage
    {
        public Dictionary<string, string> dic = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //判断是否有登录
            if (Session["UserID"] != null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                var dd = new DingClient();

                string accessToken = dd.GetToken(dd.AppKey, dd.AppSecret);
                string timestamp = dd.GetTimestamp();
                string noncestr = dd.GetNoncestr();
                string ticket = dd.GetTicket(accessToken);
                string url = dd.GetUrl();
                string signature = dd.GetSignature(ticket, noncestr, timestamp, url);

                //dingding参数
                dic.Add("accessToken", accessToken);
                dic.Add("agentId", dd.AgentId);
                dic.Add("corpId", dd.CorpId);
                dic.Add("timeStamp", timestamp);
                dic.Add("nonceStr", noncestr);
                dic.Add("signature", signature);

                //if (Request.Cookies["UserCookie"] != null)
                //{
                //    this.UserName.Text = Request.Cookies["UserCookie"].Value;
                //}
            }
        }

        //登录按钮
        protected void SubmitButtom_Click(object sender, EventArgs e)
        {
            string userName = this.UserName.Text.Trim();
            string userPassword = this.UserPassword.Text.Trim();
            string code = this.HiddenCode.Value;

            if (userName == "")
            {
                Utility.Alert(this, "用户名未填写！");
                return;
            }

            if (userPassword == "")
            {
                Utility.Alert(this, "密码未填写！");
                return;
            }

            //保留用户名Cookie
            Response.Cookies["UserCookie"].Value = userName;
            Response.Cookies["UserCookie"].Expires = DateTime.MaxValue;

            UserTableBLL bll = new UserTableBLL();
            bool success = bll.UserLogin(userName, userPassword);
            if (success)
            {
                Session["TodayDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                UserInfo user = bll.GetUserByUserPassword(userName, userPassword);
                user.DtUser = code;
                bll.UpdateUser(user);
                UserHelper.SaveSession(user);

                Response.Cookies["ThemeCookie"].Value = user.UserTheme;
                Response.Cookies["ThemeCookie"].Expires = DateTime.MaxValue;

                string url = Request.QueryString["url"];
                if (url == "" || url == null)
                {
                    url = "Default.aspx";
                }

                Response.Redirect(url);
            }
            else
            {
                Utility.Alert(this, "登录失败！");
            }
        }
    }
}