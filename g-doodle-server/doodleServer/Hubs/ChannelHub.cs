using System;
using System.Collections.Generic;
using doodleCore.Services;
using doodleServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace doodleServer.Hubs
{
    public class ChannelHub : GenericHub
    {
        private readonly string SEND = "CHANNEL__SEND";
        private readonly string UPDATE = "CHANNEL__UPDATED";

        public void Send(string guid, string userId, string message)
        {
            Clients.OthersInGroup(guid).SendAsync(SEND, userId, message);
        }

        public void Update(Channel channel)
        {
            Clients.OthersInGroup(channel.guid).SendAsync(UPDATE, channel);
        }
    }
}