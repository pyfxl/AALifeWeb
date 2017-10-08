using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Messaging;

public partial class msmq_Default : System.Web.UI.Page
{
    MessageQueue messageQueue = null;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (MessageQueue.Exists(@".\Private$\MyQueues"))
        {
            messageQueue = new MessageQueue(@".\Private$\MyQueues");
            messageQueue.Label = "Testing Queue";
        }
        else
        {
            messageQueue = MessageQueue.Create(@".\Private$\MyQueues");
            messageQueue.Label = "Newly Created Queue";
        }

        messageQueue.Send("First ever Message is sent to MSMQ", "Title");

    }
}