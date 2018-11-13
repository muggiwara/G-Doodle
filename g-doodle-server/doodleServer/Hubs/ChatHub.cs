using System;
using System.Collections.Generic;
using doodleCore.Models;
using doodleCore.Services;
using Microsoft.AspNetCore.SignalR;

namespace doodleServer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string CHAT = "CHAT__";
        private readonly string STATUS = "CHAT__STATUS";
        private readonly string CONNECTION = "CHAT__CONNECTION";
        public ChatHub()
        {
            
        }

        public void Connect(int userId, UserStatus status = UserStatus.ONLINE) {
            var currentId = new { id = userId };
            User user = SimpleCrud.Current.Get<User>(currentId);
            if (user != null) {
                user.connectionId = Context.ConnectionId;
                user.status = status;
                SimpleCrud.Current.Update<User>(user, currentId);
                var usersConnected = SimpleCrud.Current.GetAll<User>(new { status = UserStatus.OFFLINE, id = userId}, "status != @status and id != @id");
                SendToCaller(STATUS, usersConnected);
                SendToAllExceptCaller(CONNECTION, user);
            }
        }

        public void SendToAll(int userId, string message)
        {
            var msg = new Message(){
                idFrom = userId,
                message = message,
                owner = Owner.ALL,
                created = DateTime.Now
            };
            var res = SimpleCrud.Current.Insert<Message>(msg);
            if (res == 1){

                SendToAllExceptCaller(CHAT, msg);
            }
        }

        public void SendTo(int userIdFrom, int userIdTo, string message){
            var msg = new Message() {
                idFrom = userIdFrom,
                idTo = userIdTo,
                owner = Owner.PV,
                created = DateTime.Now
            };
            var res = SimpleCrud.Current.Insert<Message>(msg);
            if (res == 1){
                var userTo = SimpleCrud.Current.Get<User>(new { id = userIdTo});
                SendTo(userTo.connectionId, CHAT, msg);
            }
        }

        public void UserChange(User user)
        {
            var currentId = new { id = user.id };
            var res = SimpleCrud.Current.Update<User>(user, currentId);
            if (res == 1){
                SendToAllExceptCaller(CONNECTION, user);
            }
        }

        private void SendTo(string connectionId, string method, object arg){
            Clients.Client(connectionId).SendAsync(method, arg);
        }

        private void SendToAllExceptCaller(string method, object arg){
            Clients.AllExcept(new List<string>() { Context.ConnectionId }).SendAsync(method, arg);
        }

        private void SendToCaller(string method, object arg){
            Clients.Caller.SendAsync(method, arg);
        }
    }
}