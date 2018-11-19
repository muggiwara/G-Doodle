// using doodleCore.Models;
using doodleCore.DTO;
using doodleCore.Services;
using doodleServer.Models;

namespace doodleServer.Hubs
{
    public class UserHub : GenericHub
    {
        private readonly string CONNECTED = "USER__CONNECTED";
        private readonly string UPDATED = "USER__UPDATED";


        public UserHub()
        {
        }

        public void Connect(int userId, UserStatus status = UserStatus.ONLINE)
        {
            var user = UserService.Current.GetById(userId);
            if (user != null)
            {
                user.connectionId = Context.ConnectionId;
                user.status = status;
                var res = UserService.Current.Update(user);
                if (res) {
                    SendToAll(CONNECTED, res);
                } else
                {
                    SendToCaller(CONNECTED, res);
                }
            }
        }

        public void Update(int userId)
        {
            SendToAllExceptCaller(UPDATED, userId);
        }
    }
}