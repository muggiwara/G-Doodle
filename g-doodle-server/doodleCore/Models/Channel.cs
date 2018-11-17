using System.Collections.Generic;
using System.Linq;
using doodleCore.Services;

namespace doodleCore.Models
{
    public class Channel
    {
        [Primary]
        public int id { get; set; }
        public string name { get; set; }
        public int adminId { get; set; }
        public string usersId { get; set; }
        public bool isPrivate { get; set; }

        public string Guid {get;set;}

        public IEnumerable<int> Users
        {
            get
            {
                return usersId.Split("|".ToCharArray()).Select(u => int.Parse(u));
            }
            set
            {
                usersId = string.Join("|", value.Select(v => v.ToString()));
            }
        }
    }
}