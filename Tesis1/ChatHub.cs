using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Tesis1
{
    public class ChatHub : Hub
    {
        public void Send(string img)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(img);
        }
    }
}