using doodleCore.Services;

namespace doodleCore.Models
{
    public class User
    {
        [Primary]
        public int id {get;set;}
        public string name {get;set;}
        public string email {get;set;}
        public UserStatus status {get;set;}
        public string connectionId {get;set;}
    }

    public enum UserStatus{
        OFFLINE = 0,
        ONLINE = 1,
        HS = 2,
    }
}