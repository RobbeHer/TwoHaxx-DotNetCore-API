using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class VoteUser
    {
        public int VoteUserID { get; set; }

        // Relations
        public int? UserID { get; set; }
        public User User { get; set; }
        public int PollOptionID { get; set; }
        public PollOption PollOption { get; set; }
    }
}
