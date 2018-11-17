using doodleCore.Models;
using doodleCore.Services;

namespace doodleServer.Hubs
{
    public class UserHub : GenericHub
    {
        private readonly string CONNECTED = "USER__CONNECTED";
        private readonly string USERSCONNECTED = "USERS__CONNECTED";
        private readonly string UPDATED = "USER__UPDATED";

        public void Connect(int userId, UserStatus status = UserStatus.ONLINE)
        {
            var currentId = new { id = userId };
            var user = SimpleCrud.Current.Get<User>(currentId);
            if (user != null)
            {
                user.connectionId = Context.ConnectionId;
                user.status = status;
                SimpleCrud.Current.Update<User>(user, currentId);
                var usersConnected = SimpleCrud.Current.GetAll<User>(new { status = UserStatus.OFFLINE, id = userId }, "status != @status and id != @id");
                SendToCaller(USERSCONNECTED, usersConnected);
                SendToAllExceptCaller(CONNECTED, user);
            }
        }

        public void Update(User user)
        {
            var currentId = new { id = user.id };
            var res = SimpleCrud.Current.Update<User>(user, currentId);
            if (res == 1)
            {
                SendToAllExceptCaller(UPDATED, user);
                SendToCaller(SUCCESS, null);
            }
        }
    }
}