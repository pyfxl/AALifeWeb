using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClearCache : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["CatTypeID"] != null)
        {
            Response.Cookies["CatTypeID"].Expires = DateTime.Now.AddDays(-1);
        }

        if (Request.Cookies["CardID"] != null)
        {
            Response.Cookies["CardID"].Expires = DateTime.Now.AddDays(-1);
        }

        if (Request.Cookies["UserCookie"] != null)
        {
            Response.Cookies["UserCookie"].Expires = DateTime.Now.AddDays(-1);
        }

        if (Request.Cookies["ThemeCookie"] != null)
        {
            Response.Cookies["ThemeCookie"].Expires = DateTime.Now.AddDays(-1);
        }
    }
}