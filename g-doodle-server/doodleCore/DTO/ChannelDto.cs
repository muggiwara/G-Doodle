using System.Collections.Generic;
using System.Linq;
using doodleCore.CustomAttributes;
using doodleCore.Services;

namespace doodleCore.DTO
{
    [Name("Channel")]
    public class ChannelDto
    {
        [Primary]
        public int id { get; set; }
        public string name { get; set; }
        public int adminId { get; set; }
        public string usersId { get; set; }
        public bool isPrivate { get; set; }
        public string guid {get;set;}
    }
}