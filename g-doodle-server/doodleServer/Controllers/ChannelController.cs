using Microsoft.AspNetCore.Mvc;
using doodleCore.Models;
using doodleCore.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace doodleServer.Controllers
{
    [Route("api/channel")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Channel>> Get()
        {
            var channels = SimpleCrud.Current.GetAll<Channel>(new { isPrivate = false });
            if (channels == null)
            {
                return NotFound();
            }
            return Ok(channels);
        }

        [HttpGet("{name}", Name = "GetChannel")]
        public ActionResult<Channel> GetByName(string name)
        {
            var channel = SimpleCrud.Current.Get<Channel>(new { name = name });
            if (channel == null)
            {
                return NotFound();
            }
            return Ok(channel);
        }

        [HttpPost]
        public IActionResult Create(Channel channel)
        {
            channel.Guid = Guid.NewGuid().ToString();
            var res = SimpleCrud.Current.Insert(channel);
            
            if (res != 1)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetByName", new { name = channel.name }, channel);
        }

        [HttpDelete("{id}/{userId}")]
        public IActionResult Delete(int id, int userId) {
            var res = SimpleCrud.Current.Delete<Channel>(new { id = id, adminId = userId });
            if (res != 1) {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Channel channel)
        {
            var res = SimpleCrud.Current.Update<Channel>(channel, new { name = channel.name});
            if (res != 1){
                return BadRequest();
            }
            return Ok();
        }
    }
}