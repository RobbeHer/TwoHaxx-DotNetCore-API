using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class Talk
    {
        public int TalkID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relations
        public int TalkerID { get; set; }
        public User Talker { get; set; }
        public int ModeratorID { get; set; }
        public User Moderator { get; set; }
        public int RoomID { get; set; }
        public Room Room { get; set; }
    }
}
