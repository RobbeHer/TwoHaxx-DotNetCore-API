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
    public class VoteUserController : Controller
    {
        private readonly TwoHaxxContext _context;
        ClientOptions clientOptions;
        AblyRest rest;

        public VoteUserController(TwoHaxxContext context)
        {
            _context = context;
            clientOptions = new ClientOptions("P00bXw.opuSTw:aX_M6hXKsMuN95ZQ");
            rest = new AblyRest(clientOptions);
        }

        private async Task<ActionResult<VoteUser>> PublishVoteUser(VoteUser voteUser)
        {
            var channel = rest.Channels.Get("pollChannel" + voteUser.PollOption.Poll.TalkID.ToString());
            await channel.PublishAsync("voteUser", JsonSerializer.Serialize(voteUser));
            return Ok(voteUser);
        }

        public async Task<ActionResult<VoteUser>> DeleteVoteUser(VoteUser voteUser)
        {
            _context.VoteUsers.Remove(voteUser);
            await _context.SaveChangesAsync();

            return Ok(voteUser);
        }

        public async Task<ActionResult<VoteUser>> PostVoteUser(VoteUser voteUser)
        {
            _context.VoteUsers.Add(new VoteUser()
            {
                UserID = voteUser.UserID,
                PollOptionID = voteUser.PollOptionID
            });
            await _context.SaveChangesAsync();

            voteUser.PollOption.Poll = _context.Polls.Where(x => x.PollID == voteUser.PollOption.PollID).FirstOrDefault();

            var result = await PublishVoteUser(voteUser);

            return Ok(result);
        }
    }
}