namespace StudiTrain.Models.Database
{
    public partial class GroupsToPermissions
    {
        public int GroupId { get; set; }
        public int PermissionId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Permissions Permission { get; set; }
    }
}
