using System.Collections.Generic;

namespace StudiTrain.Models.Database
{
    public partial class Groups
    {
        public Groups()
        {
            GroupsToPermissions = new HashSet<GroupsToPermissions>();
            UsersToGroups = new HashSet<UsersToGroups>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<GroupsToPermissions> GroupsToPermissions { get; set; }
        public virtual ICollection<UsersToGroups> UsersToGroups { get; set; }
    }
}
