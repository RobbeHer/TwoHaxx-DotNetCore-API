using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class RoomLogin
    {
        public int FeedbackID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relations
        public int UserID { get; set; }
        public User User { get; set; }
        public int RoomID { get; set; }
        public Room Room { get; set; }
    }
}
