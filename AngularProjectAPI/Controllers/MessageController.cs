using AngularProjectAPI.Models;
using AngularProjectAPI.Services;
using IO.Ably;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly TwoHaxxContext _context;
        ClientOptions clientOptions;
        AblyRest rest;

        public MessageController(TwoHaxxContext context)
        {
            _context = context;
            clientOptions = new ClientOptions("P00bXw.opuSTw:aX_M6hXKsMuN95ZQ");
            rest = new AblyRest(clientOptions);
        }

        private async Task<ActionResult<Models.Message>> PublishNewMessage(Models.Message message)
        {
            var channel = rest.Channels.Get("talkChannel" + message.TalkID.ToString());
            await channel.PublishAsync("newChatMessage", JsonSerializer.Serialize(message));
            return Ok(message);
        }

        private async Task<ActionResult<Models.Message>> PublishDeletedMessage(Models.Message message)
        {
            var channel = rest.Channels.Get("talkChannel" + message.TalkID.ToString());
            await channel.PublishAsync("deleteChatMessage", JsonSerializer.Serialize(message));
            return Ok(message);
        }

        private async Task<ActionResult<Models.Message>> PublishUpdatedMessage(Models.Message message)
        {
            var channel = rest.Channels.Get("talkChannel" + message.TalkID.ToString());
            await channel.PublishAsync("updateChatMessage", JsonSerializer.Serialize(message));
            return Ok(message);
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Message/room/5
        [HttpGet("room/{id}")]
        public async Task<ActionResult<IEnumerable<Models.Message>>> GetMessagesOfRoom(int id)
        {
            var messages = await _context.Messages.Where(x => x.TalkID == id).ToListAsync();

            if (messages == null)
            {
                return NotFound();
            }

            foreach (var message in messages)
            {
                message.User = _context.Users.Where(x => x.UserID == message.UserID).FirstOrDefault();
            }

            return messages;
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Message>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            message.User = _context.Users.Where(x => x.UserID == message.UserID).FirstOrDefault();

            return message;
        }

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Models.Message message)
        {
            if (id != message.MessageID) { return BadRequest(); }

            _context.Entry(message).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        // PUT: api/Message/liked/5
        [HttpPost("liked/")]
        public async Task<ActionResult<UserLikeMessage>> PostLikeOnMessage(UserLikeMessage userLikeMessage)
        {
            UserLikeMessageController userLikeMessageController = new UserLikeMessageController(_context);
            var result = await userLikeMessageController.PostUserLikeMessage(userLikeMessage);

            return Ok(result);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageID == id);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Message>> PostMessage(Models.Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            message.User = _context.Users.Where(x => x.UserID == message.UserID).FirstOrDefault();
            message.Talk = _context.Talks.Where(x => x.TalkID == message.TalkID).FirstOrDefault();

            var result = await PublishNewMessage(message);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Message>> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            // first remove all userLikeMessages before deleting the message
            UserLikeMessageController userLikeMessageController = new UserLikeMessageController(_context);
            var userLikeMessages = await _context.UserLikeMessage.Where(x => x.MessageID == id).ToListAsync();

            if (userLikeMessages != null)
            {
                foreach (var userLikeMessage in userLikeMessages)
                {
                    await userLikeMessageController.DeleteUserLikeMessage(userLikeMessage);
                }
            }

            // delte message
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            var result = await PublishDeletedMessage(message);
            return Ok(result);
        }
    }
}