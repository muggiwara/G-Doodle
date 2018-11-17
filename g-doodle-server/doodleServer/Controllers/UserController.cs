using System.Collections.Generic;
using doodleCore.Models;
using doodleCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace doodleServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = SimpleCrud.Current.GetAll<User>(new { status = UserStatus.OFFLINE }, "status != @status");
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

         [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(int id)
        {
            var user = SimpleCrud.Current.Get<User>(new { id = id });
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                var res = SimpleCrud.Current.Insert(user);
            if (res != 1)
            {
                return BadRequest();
            }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return CreatedAtRoute("GetByName", new { id = user.id }, user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var res = SimpleCrud.Current.Delete<User>(new { id =id });
            if (res != 1) {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            var res = SimpleCrud.Current.Update<User>(user, new { id = user.id});
            if (res != 1){
                return BadRequest();
            }
            return Ok();
        }
    }
}