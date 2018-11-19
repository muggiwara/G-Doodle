using doodleCore.CustomAttributes;
using doodleCore.Services;

namespace doodleCore.DTO
{
    [Name("User")]
    public class UserDto
    {
        [Primary]
        public int id {get;set;}
        public string name {get;set;}
        public string password {get;set;}
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