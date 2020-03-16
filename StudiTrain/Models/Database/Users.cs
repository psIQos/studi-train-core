using System;
using System.Collections.Generic;

namespace StudiTrain.Models.Database
{
    public partial class Users
    {
        public Users()
        {
            UsersToGroups = new HashSet<UsersToGroups>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Passhash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<UsersToGroups> UsersToGroups { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
