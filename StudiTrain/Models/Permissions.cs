using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            GroupsToPermissions = new HashSet<GroupsToPermissions>();
            UsersToPermissions = new HashSet<UsersToPermissions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<GroupsToPermissions> GroupsToPermissions { get; set; }
        public virtual ICollection<UsersToPermissions> UsersToPermissions { get; set; }
    }
}
