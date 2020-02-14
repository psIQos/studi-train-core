namespace StudiTrain.Models.Database
{
    public partial class UsersToGroups
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Users User { get; set; }
    }
}
