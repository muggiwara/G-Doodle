using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using doodleCore.DTO;

namespace doodleServer.Models
{
    public class Channel
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int adminId { get; set; }
        public IEnumerable<int> usersId { get; set; }
        public bool isPrivate { get; set; }
        public string guid { get; set; }

        public ChannelDto ToDto()
        {
            return new ChannelDto()
            {
                id = this.id,
                name = this.name,
                guid = this.guid,
                isPrivate = this.isPrivate,
                adminId = this.adminId,
                usersId = string.Join('|', this.usersId)
            };
        }

        public Channel()
        {

        }

        public Channel(ChannelDto channel)
        {
            this.id = channel.id;
            this.name = channel.name;
            this.adminId = channel.adminId;
            this.guid = channel.guid;
            this.isPrivate = channel.isPrivate;
            this.usersId = channel.usersId.Split("|".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries).Select(userId => int.Parse(userId));
        }
    }
}