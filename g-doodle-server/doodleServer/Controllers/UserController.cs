using System.Collections.Generic;
using doodleCore.DTO;
using doodleServer.Models;
using doodleCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace doodleServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = UserService.Current.GetAll();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users.Select(u => new User(u)));
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(int id)
        {
            var user = UserService.Current.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new User(user));
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                var usr = UserService.Current.Insert(user.ToDto());
                if (usr == null)
                {
                    return BadRequest();
                }
                return Ok(usr);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    var reg = new Regex(@".*\((.*)\).*");
                    var match = reg.Match(ex.Message);
                    var values = match.Groups.Last().Value.Split(", ".ToArray(), System.StringSplitOptions.RemoveEmptyEntries);
                    return BadRequest(values.Select(v => $"{v} allready exist"));
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!UserService.Current.Delete(id))
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            if (!UserService.Current.Update(user.ToDto()))
            {
                return BadRequest(false);
            }
            return Ok(true);
        }
    }
}