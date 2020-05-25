using AALife.BLL;
using AALife.Model;
using AALife.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AALife.WebMvc.Web2018
{
    public partial class DingUser : FirstPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = Request.QueryString["code"];
                string accessToken = Request.QueryString["accessToken"];
                string userid = DingHelper.GetUserByCode(accessToken, code);//获取dtuser

                UserTableBLL bll = new UserTableBLL();
                UserInfo user = bll.GetUserByDtUser(userid);//查询dtuser是否有关联
                if (user.UserName != "")
                {
                    Session["TodayDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                    UserHelper.SaveSession(user);

                    Response.Cookies["ThemeCookie"].Value = user.UserTheme;
                    Response.Cookies["ThemeCookie"].Expires = DateTime.MaxValue;
                    
                    Response.Write("{ \"user\": \"" + userid + "\", \"exists\": \"1\" }");
                    Response.End();
                }
                else
                {
                    Response.Write("{ \"user\": \"" + userid + "\", \"exists\": \"0\" }");
                    Response.End();
                }
            }
        }
    }

}