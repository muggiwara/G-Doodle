using Microsoft.AspNetCore.SignalR;

namespace doodleServer.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {
            
        }
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("chat", name, message);
        }
    }
}