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
    public class PollOptionController : ControllerBase
    {
        private readonly TwoHaxxContext _context;

        public PollOptionController(TwoHaxxContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollOption>>> GetPollOptions()
        {
            return await _context.PollOptions.ToListAsync();
        }

        // GET: api/PollOption/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollOption>> GetPollOption(int id, string code)
        {
            var pollOption = await _context.PollOptions.FindAsync(id);

            if (pollOption == null)
            {
                return NotFound();
            }

            return pollOption;
        }

        // PUT: api/PollOption/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollOption(int id, PollOption pollOption)
        {
            if (id != pollOption.PollOptionID)
            {
                return BadRequest();
            }

            _context.Entry(pollOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollOptionExists(id))
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

        private bool PollOptionExists(int id)
        {
            return _context.PollOptions.Any(e => e.PollOptionID == id);
        }

        [HttpPost]
        public async Task<ActionResult<PollOption>> PostPollOption(PollOption pollOption)
        {
            _context.PollOptions.Add(pollOption);
            await _context.SaveChangesAsync();

            return Ok(pollOption);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PollOption>> DeletePollOption(int id)
        {
            var pollOption = await _context.PollOptions.FindAsync(id);
            if (pollOption == null)
            {
                return NotFound();
            }

            _context.PollOptions.Remove(pollOption);
            await _context.SaveChangesAsync();

            return pollOption;
        }
    }
}
