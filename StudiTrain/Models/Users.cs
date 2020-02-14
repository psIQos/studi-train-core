using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class Users
    {
        public Users()
        {
            UsersToGroups = new HashSet<UsersToGroups>();
            UsersToPermissions = new HashSet<UsersToPermissions>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Passhash { get; set; }

        public virtual ICollection<UsersToGroups> UsersToGroups { get; set; }
        public virtual ICollection<UsersToPermissions> UsersToPermissions { get; set; }
    }
}
