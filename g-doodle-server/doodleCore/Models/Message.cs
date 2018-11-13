using System;

namespace doodleCore.Models
{
    public class Message
    {
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