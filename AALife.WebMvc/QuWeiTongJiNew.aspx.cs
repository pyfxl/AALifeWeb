using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AALife.WebMvc
{
    public partial class QuWeiTongJiNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/Web2015/QuWeiTongJiNew.aspx?flag=" + (Request.QueryString["flag"] ?? "1"));
        }
    }
}