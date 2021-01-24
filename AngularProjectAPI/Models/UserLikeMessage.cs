using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class UserLikeMessage
    {
        public int UserLikeMessageID { get; set; }

        // Relations
        public int UserID { get; set; }
        public User User { get; set; }
        public int MessageID { get; set; }
        public Message Message { get; set; }
    }
}

