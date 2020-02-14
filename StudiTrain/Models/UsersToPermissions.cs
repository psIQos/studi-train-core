using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class UsersToPermissions
    {
        public int? UserId { get; set; }
        public int? PermissionId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual Users User { get; set; }
    }
}
