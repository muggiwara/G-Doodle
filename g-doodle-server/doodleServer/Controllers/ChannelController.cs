using Microsoft.AspNetCore.Mvc;
using doodleCore.DTO;
using doodleCore.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using doodleServer.Models;

namespace doodleServer.Controllers
{
    [Route("api/channel")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Channel>> Get()
        {
            var channels = ChannelService.Current.GetAll();
            if (channels == null)
            {
                return NotFound();
            }
            return Ok(channels.Select(c => new Channel(c)));
        }

        [HttpGet("{id}", Name = "GetChannel")]
        public ActionResult<Channel> GetById(int id)
        {
            var channel = ChannelService.Current.GetById(id);
            if (channel == null)
            {
                return NotFound();
            }
            return Ok(new Channel(channel));
        }

        [HttpPost]
        public IActionResult Create(Channel channel)
        {
            channel.guid = Guid.NewGuid().ToString();
            var chan = ChannelService.Current.Insert(channel.ToDto());
            if (chan == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetById", new { id = chan.id }, new Channel(chan));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (!ChannelService.Current.Delete(id)) {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Channel channel)
        {
            
            if (!ChannelService.Current.Update(channel.ToDto())){
                return BadRequest();
            }
            return Ok();
        }
    }
}