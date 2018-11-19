using System.Collections.Generic;
using doodleCore.DTO;

namespace doodleCore.Services
{
    public class UserService
    {
        private static volatile UserService _instance;

        public static UserService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }
        }

        public IEnumerable<UserDto> GetAll(object param = null, string cond = "status != @status")
        {
            if (param == null)
            {
                param = new { status = UserStatus.OFFLINE };
            }
            return SimpleCrud.Current.GetAll<UserDto>(param, cond);
        }

        public UserDto GetById(int id)
        {
            return SimpleCrud.Current.Get<UserDto>(new {id = id}) as UserDto;  
        }

        public UserDto GetByNameOrEmail(string nameOrEmail) 
        {
            return SimpleCrud.Current.Get<UserDto>(new {name = nameOrEmail, email = nameOrEmail}, "name = @name OR email = @email") as UserDto;
        }

        public UserDto Insert(UserDto user)
        {
            var res = SimpleCrud.Current.Insert(user);
            if (res != 1){
                return null;
            }
            return SimpleCrud.Current.Get<UserDto>(new {name = user.name, email = user.email, password = user.password}) as UserDto;
        }

        public bool Update(UserDto user){
            return SimpleCrud.Current.Update(user, new {id = user.id}) == 1;
        }

        public bool Delete(int id)
        {
            return SimpleCrud.Current.Delete<UserDto>(new {id = id}) == 1;
        }
    }
}