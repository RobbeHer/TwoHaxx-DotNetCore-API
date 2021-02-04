using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class Poll
    {
        public int PollID { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public ICollection<PollOption> PollOptions { get; set; }

        // Relations
        /*public int PollTypeID { get; set; }
        public PollType PollType { get; set; }*/
        //public int RoomID { get; set; }
        //public Room Room { get; set; }
        public int TalkID { get; set; }
        public Talk Talk { get; set; }
    }
}