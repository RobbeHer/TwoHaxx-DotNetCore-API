﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string ActiveDirectoryGUID { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsGuest { get; set; }

        public virtual ICollection<Talk> TalksAsModerator { get; set; }
        public virtual ICollection<Talk> TalksAsTalker{ get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
