using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DataSorting.Client.hubs
{
    public class NotifyHub : Hub
    {
        public void Result()
        {
            Clients.All.result();
        }

        public void Percentage()
        {
            Clients.All.percent();
        }
    }
}