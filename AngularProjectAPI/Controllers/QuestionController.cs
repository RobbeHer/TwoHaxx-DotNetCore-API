using AngularProjectAPI.Models;
using IO.Ably;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly TwoHaxxContext _context;
        ClientOptions clientOptions;
        AblyRest rest;

        public QuestionController(TwoHaxxContext context)
        {
            _context = context;
            clientOptions = new ClientOptions("P00bXw.opuSTw:aX_M6hXKsMuN95ZQ");
            rest = new AblyRest(clientOptions);
        }

        private async Task<ActionResult<Models.Message>> PublishNewQuestion(Models.Message question)
        {
            var channel = rest.Channels.Get("talkChannel" + question.TalkID.ToString());
            await channel.PublishAsync("newQuestion", JsonSerializer.Serialize(question));
            return Ok(question);
        }

        private async Task<ActionResult<Models.Message>> PublishAnsweredQuestion(Models.Message question)
        {
            var channel = rest.Channels.Get("talkChannel" + question.TalkID.ToString());
            await channel.PublishAsync("answeredQuestion", JsonSerializer.Serialize(question));
            return Ok(question);
        }

        private async Task<ActionResult<Models.Message>> PublishDeletedQuestion(Models.Message question)
        {
            var channel = rest.Channels.Get("talkChannel" + question.TalkID.ToString());
            await channel.PublishAsync("deleteQuestion", JsonSerializer.Serialize(question));
            return Ok(question);
        }
        
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Message>>> GetQuestions()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Question/room/5
        [HttpGet("room/{id}")]
        public async Task<ActionResult<IEnumerable<Models.Message>>> GetQuestionsOfRoom(int id)
        {
            var questions = await _context.Messages.Where(x => x.TalkID == id).ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            foreach (var message in questions)
            {
                message.User = _context.Users.Where(x => x.UserID == message.UserID).FirstOrDefault();
            }

            return questions;
        }

        // PUT: api/Question/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Models.Message question)
        {
            if (id != question.MessageID) { return BadRequest(); }

            _context.Entry(question).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        // PUT: api/Question/answered/5
        [HttpPut("answered/{id}")]
        public async Task<IActionResult> PutAnsweredQuestion(int id, Models.Message question)
        {
            if (id != question.MessageID) { return BadRequest(); }

            _context.Entry(question).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id)) { return NotFound(); }
                else { throw; }
            }

            var result = await PublishAnsweredQuestion(question);

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Messages.Any(e => e.MessageID == id);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Message>> PostQuestion(Models.Message question)
        {
            _context.Messages.Add(question);
            await _context.SaveChangesAsync();

            question.User = _context.Users.Where(x => x.UserID == question.UserID).FirstOrDefault();
            question.Talk = _context.Talks.Where(x => x.TalkID == question.TalkID).FirstOrDefault();

            var result = await PublishNewQuestion(question);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Message>> DeleteQuestion(int id)
        {
            var question = await _context.Messages.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(question);
            await _context.SaveChangesAsync();

            var result = await PublishDeletedQuestion(question);
            return Ok(result);
        }
    }
}
