using doodleCore.Services;
using doodleServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace doodleServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginModel login)
        {
            var user = UserService.Current.GetByNameOrEmail(login.nameOrEmail);
            if (user != null && user.password == login.password) {
                return Ok(user);
            }
            return NotFound("login or password invalid");
        }

        /*
        [HttpGet("{nameOrEmail}", Name = "GetUser")]
        public ActionResult<User> Login(string nameOrEmail)
        {
            
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new User(user));
        }
        
         */
    }
}