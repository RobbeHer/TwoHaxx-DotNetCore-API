using AngularProjectAPI.Models;
using IO.Ably;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AngularProjectAPI.Controllers
{
    public class UserLikeMessageController : Controller
    {
        private readonly TwoHaxxContext _context;
        ClientOptions clientOptions;
        AblyRest rest;

        public UserLikeMessageController(TwoHaxxContext context)
        {
            _context = context;
            clientOptions = new ClientOptions("P00bXw.opuSTw:aX_M6hXKsMuN95ZQ");
            rest = new AblyRest(clientOptions);
        }

        private async Task<ActionResult<UserLikeMessage>> PublishLikeOnMessage(UserLikeMessage userLikeMessage)
        {
            var channel = rest.Channels.Get("talkChannel" + userLikeMessage.Message.TalkID.ToString());
            await channel.PublishAsync("likeOnChatMessage", JsonSerializer.Serialize(userLikeMessage));
            return userLikeMessage;
        }

        public async Task<ActionResult<UserLikeMessage>> DeleteUserLikeMessage(UserLikeMessage userLikeMessage)
        {
            _context.UserLikeMessage.Remove(userLikeMessage);
            await _context.SaveChangesAsync();

            return userLikeMessage;
        }

        public async Task<ActionResult<UserLikeMessage>> PostUserLikeMessage(UserLikeMessage userLikeMessage)
        {
            _context.UserLikeMessage.Add(new UserLikeMessage() { 
                MessageID = userLikeMessage.MessageID,
                UserID = userLikeMessage.UserID
            });
            await _context.SaveChangesAsync();

            var result = await PublishLikeOnMessage(userLikeMessage);

            return result;
        }
    }
}
