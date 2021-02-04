using AngularProjectAPI.Models;
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
    public class PollController : ControllerBase
    {
        private readonly TwoHaxxContext _context;

        public PollController(TwoHaxxContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPolls()
        {
            var polls = await _context.Polls.ToListAsync();
            foreach (var poll in polls)
            {
                poll.PollOptions = _context.PollOptions.Where(x => x.PollID == poll.PollID).ToArray();
            }
            return polls;
        }

        // GET: api/Poll/polls-of-room/5
        [HttpGet("polls-of-room/{id}")]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPollsOfRoom(int id)
        {
            var polls = await _context.Polls.Where(x => x.TalkID == id).ToListAsync();
            foreach (var poll in polls)
            {
                poll.PollOptions = _context.PollOptions.Where(x => x.PollID == poll.PollID).ToArray();
            }
            return polls;
        }

        // GET: api/Poll/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPoll(int id, string code)
        {
            var poll = await _context.Polls.FindAsync(id);

            if (poll == null)
            {
                return NotFound();
            }

            poll.PollOptions = _context.PollOptions.Where(x => x.PollID == poll.PollID).ToArray();

            return poll;
        }

        // PUT: api/Poll/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoll(int id, Poll poll)
        {
            if (id != poll.PollID)
            {
                return BadRequest();
            }

            _context.Entry(poll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(id))
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

        private bool PollExists(int id)
        {
            return _context.Polls.Any(e => e.PollID == id);
        }

        [HttpPost]
        public async Task<ActionResult<Poll>> PostPoll(Poll poll)
        {
            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();

            return Ok(poll);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Poll>> DeletePoll(int id)
        {
            var poll = await _context.Polls.FindAsync(id);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();

            return poll;
        }
    }
}
