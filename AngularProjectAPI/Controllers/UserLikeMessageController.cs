using AngularProjectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Controllers
{
    public class UserLikeMessageController : Controller
    {
        private readonly TwoHaxxContext _context;

        public UserLikeMessageController(TwoHaxxContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<UserLikeMessage>> DeleteUserLikeMessage(UserLikeMessage userLikeMessage)
        {
            _context.UserLikeMessage.Remove(userLikeMessage);
            await _context.SaveChangesAsync();

            return Ok(userLikeMessage);
        }

        public async Task<ActionResult<UserLikeMessage>> PostUserLikeMessage(UserLikeMessage userLikeMessage)
        {
            _context.UserLikeMessage.Add(userLikeMessage);
            await _context.SaveChangesAsync();

            return Ok(userLikeMessage);
        }
    }
}
