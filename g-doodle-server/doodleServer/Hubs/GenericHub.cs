using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace doodleServer.Hubs
{
    public class GenericHub : Hub
    {
        protected readonly string SUCCESS = "SUCCESS";
        protected readonly string FAILED = "FAILED";

        protected void SendTo(string connectionId, string method, object arg)
        {
            Clients.Client(connectionId).SendAsync(method, arg);
        }

        protected void SendToAll(string method, object arg)
        {
            Clients.All.SendAsync(method, arg);
        }

        protected void SendToAllExceptCaller(string method, object arg)
        {
            Clients.AllExcept(new List<string>() { Context.ConnectionId }).SendAsync(method, arg);
        }

        protected void SendToCaller(string method, object arg)
        {
            Clients.Caller.SendAsync(method, arg);
        }

    }

}