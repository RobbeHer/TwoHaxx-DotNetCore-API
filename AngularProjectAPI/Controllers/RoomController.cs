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
    public class RoomController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TwoHaxxContext _context;

        public RoomController(IUserService userService, TwoHaxxContext context)
        {
            _userService = userService;
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        [HttpGet("plannings")]
        public async Task<ActionResult<IEnumerable<Room>>> GetPlannings()
        {
            var plannings = await _context.Rooms.ToListAsync();
            foreach (var room in plannings)
            {
                room.Talks = _context.Talks.Where(x => x.RoomID == room.RoomID).ToArray();
            }
            return plannings;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // GET: api/Room/planning/5
        [HttpGet("planning/{id}")]
        public async Task<ActionResult<Room>> GetPlanning(int id)
        {
            var planning = await _context.Rooms.FindAsync(id);

            if (planning == null)
            {
                return NotFound();
            }

            planning.Talks = _context.Talks.Where(x => x.RoomID == planning.RoomID).ToArray();

            return planning;
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.RoomID)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }
    }
}
