using System;
using System.Web;

public class AdminPage : BasePage
{
    public AdminPage()
    {
        this.Load += new EventHandler(AdminPage_Load);
    }

    void AdminPage_Load(object sender, EventArgs e)
    {
        if (!object.Equals(Session["UserLevel"], "9"))
        {
            Response.Write("<script>alert('����ҳ�����޷���Ȩ�ޣ�');window.location.href='/UserLogin.aspx?url=Default.aspx';</script>");
            Response.End();
        }
    }
}