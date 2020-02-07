using AALife.Model;
using AALife.MSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AALifeWeb_MsmqTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            MessageQueue myQueue = MsmqHelper.CreateQueue();

            this.Label1.Text = "成功。";
        }
        catch (Exception ex)
        {
            this.Label1.Text = "失败！" + ex.ToString();
        }
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool success = MsmqHelper.Delete();

        this.Label1.Text = success.ToString();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ItemInfo item = MsmqHelper.ReceiveMessage();

        this.Label1.Text = item.ToString();
    }
}