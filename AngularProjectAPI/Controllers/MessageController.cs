﻿using AngularProjectAPI.Models;
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
    public class MessageController : Controller
    {
        private readonly TwoHaxxContext _context;

        public MessageController(TwoHaxxContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Message/room/5
        [HttpGet("room/{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesOfRoom(int id)
        {
            var messages = await _context.Messages.Where(x => x.RoomID == id).ToListAsync();

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
        public async Task<ActionResult<Message>> GetMessage(int id)
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
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.MessageID)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageID == id);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }
}