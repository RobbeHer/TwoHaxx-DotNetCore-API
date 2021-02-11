using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Content { get; set; }
        public bool IsQuestion { get; set; }
        public bool IsAnswered { get; set; }
        public int Likes { get; set; }
        public ICollection<User> LikedBy { get; set; }
        public DateTime TimeStamp { get; set; }

        // RelationsMasp.net
        public int? UserID { get; set; }
        public User User { get; set; }
        //public int RoomID { get; set; }
        //public Room Room { get; set; }
        public int TalkID { get; set; }
        public Talk Talk { get; set; }
    }
}
