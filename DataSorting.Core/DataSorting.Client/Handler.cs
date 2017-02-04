using DataSorting.Client.datasorting;
using DataSorting.Client.hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DataSorting.Client
{
    public class Handler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile fileData = context.Request.Files["file"];
            byte[] file = null;
            using (var binaryReader = new BinaryReader(fileData.InputStream))
            {
                file = binaryReader.ReadBytes(fileData.ContentLength);
            }

            var _params = context.Request.Params;

            Start(_params["handler"],
                _params["type"],
                _params["sorting"],
                file);

            context.Response.Write(true);
            context.ApplicationInstance.CompleteRequest();
        }

        private void Start(string handler,
            string type,
            string sorting,
            byte[] file)
        {
            var sortingUtility = new DataSortingUtility(file, NotifySignalR);

            var sorted = sortingUtility.Sort(handler,
                type,
                sorting);

            //Console.WriteLine(sorted);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotifyHub>();
            hubContext.Clients.All.result(sorted);
            hubContext.Clients.All.timeSpend(sortingUtility.TimeSpend);
        }

        private void NotifySignalR()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotifyHub>();
            hubContext.Clients.All.percent();
        }
    }
}