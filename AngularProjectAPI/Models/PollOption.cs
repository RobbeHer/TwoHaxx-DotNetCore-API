using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class PollOption
    {
        public int PollOptionID { get; set; }
        public string Content { get; set; }

        // Relation
        public int PollID { get; set; }
        public Poll Poll { get; set; }
    }
}
