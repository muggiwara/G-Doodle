using System.ComponentModel.DataAnnotations;
using doodleCore.DTO;

namespace doodleServer.Models
{
    public class User
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public UserStatus status { get; set; }
        public string connectionId { get; set; }

        public UserDto ToDto()
        {
            return new UserDto()
            {
                id = this.id,
                name = this.name,
                password = this.password,
                email = this.email,
                status = this.status,
                connectionId = this.connectionId
            };
        }

        public User()
        {
        }

        public User(UserDto user)
        {
            this.id = user.id;
            this.name = user.name;
            this.password = user.password;
            this.email = user.email;
            this.connectionId = user.connectionId;
            this.status = user.status;
        }
    }
}