using System;
using doodleCore.CustomAttributes;

namespace doodleCore.DTO
{
    [Name("Message")]
    public class MessageDto
    {
        [Primary]
        public int id { get; set; }
        public string message { get; set; }
        public Owner owner { get; set; }
        public DateTime created {get;set;}
        public bool isRead {get;set;}
        public int? idTo {get;set;}
        public int? idFrom {get;set;}
    }

    public enum Owner
    {
        ALL = 0,
        PV = 1,
        ME = 2
    }
}