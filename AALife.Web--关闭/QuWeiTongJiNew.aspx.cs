using System;
using System.Web.UI.WebControls;

public partial class QuWeiTongJiNew : FirstPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("/Web2015/QuWeiTongJiNew.aspx?flag=" + (Request.QueryString["flag"] ?? "1"));
    }

}