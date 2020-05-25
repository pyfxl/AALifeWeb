using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Hubs
{
    public class ServerHub : Hub
    {
        public void SendMsg(string connectionId, string message)
        {
            Clients.All.sendMessage(connectionId, message);
        }
    }
}