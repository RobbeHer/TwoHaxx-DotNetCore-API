using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int Score { get; set; }
        public string Content { get; set; }

        // Relation
        public int? UserID { get; set; }
        public User User { get; set; }
        public int TalkID { get; set; }
        public Talk Talk { get; set; }
    }
}
