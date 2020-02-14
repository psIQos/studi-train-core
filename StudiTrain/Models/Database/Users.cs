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

        public virtual ICollection<UsersToGroups> UsersToGroups { get; set; }
    }
}
