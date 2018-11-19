using System.Collections.Generic;
using doodleCore.DTO;

namespace doodleCore.Services
{
    public class ChannelService
    {
        private static volatile ChannelService _instance;

        public static ChannelService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChannelService();
                }
                return _instance;
            }
        }

        public IEnumerable<ChannelDto> GetAll(object param = null, string cond = null)
        {
            if (param == null)
            {
                param = new { isPrivate = false };
            }
            return SimpleCrud.Current.GetAll<ChannelDto>(param, cond);
        }

        public ChannelDto GetById(int id)
        {
            return SimpleCrud.Current.Get<ChannelDto>(new {id = id}) as ChannelDto;  
        }

        public ChannelDto Insert(ChannelDto channel)
        {
            var res = SimpleCrud.Current.Insert(channel);
            if (res != 1){
                return null;
            }
            return SimpleCrud.Current.Get<ChannelDto>(new {name = channel.name, guid = channel.guid}) as ChannelDto;
        }

        public bool Update(ChannelDto user){
            return SimpleCrud.Current.Update(user, new {id = user.id}) == 1;
        }

        public bool Delete(int id)
        {
            return SimpleCrud.Current.Delete<ChannelDto>(new {id = id}) == 1;
        }
    }
}