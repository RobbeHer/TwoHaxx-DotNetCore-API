using AngularProjectAPI.Models;
using AngularProjectAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalkController : ControllerBase
    {
        private readonly TwoHaxxContext _context;

        public TalkController(TwoHaxxContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Talk>>> GetTalks()
        {
            return await _context.Talks.Include(t=>t.Talker).Include(m=>m.Moderator).ToListAsync();
        }

        // GET: api/Talk/talks-of-room/5
        [HttpGet("talks-of-room/{id}")]
        public async Task<ActionResult<IEnumerable<Talk>>> GetTalksOfRoom(int id)
        {
            return await _context.Talks.Where(x => x.RoomID == id).Include(t => t.Talker).Include(m => m.Moderator).ToListAsync();
        }

        // GET: api/Talk/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Talk>> GetTalk(int id)
        {
            var talk = await _context.Talks.FindAsync(id);

            if (talk == null)
            {
                return NotFound();
            }

            return talk;
        }

        // GET: api/Talk/join/1?code=1234
        [HttpGet("join/{id}")]
        public async Task<ActionResult<Talk>> UserJoinTalk(int id, string code)
        {
            var talk = await _context.Talks.FindAsync(id);

            if (talk == null)
            {
                return NotFound();
            }
            if (code != talk.Code)
            {
                return BadRequest();
            }

            return talk;
        }

        // PUT: api/Talk/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTalk(int id, Talk talk)
        {
            if (id != talk.TalkID)
            {
                return BadRequest();
            }

            _context.Entry(talk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TalkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool TalkExists(int id)
        {
            return _context.Talks.Any(e => e.TalkID == id);
        }

        [HttpPost]
        public async Task<ActionResult<Talk>> PostTalk(Talk talk)
        {
            _context.Talks.Add(talk);
            await _context.SaveChangesAsync();

            return Ok(talk);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Talk>> DeleteTalk(int id)
        {
            var talk = await _context.Talks.FindAsync(id);
            if (talk == null)
            {
                return NotFound();
            }

            _context.Talks.Remove(talk);
            await _context.SaveChangesAsync();

            return talk;
        }
    }
}
