using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class Categories
    {
        public Categories()
        {
            CategoryRelationsAncestorNavigation = new HashSet<CategoryRelations>();
            CategoryRelationsDescendantNavigation = new HashSet<CategoryRelations>();
            Questions = new HashSet<Questions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<CategoryRelations> CategoryRelationsAncestorNavigation { get; set; }
        public virtual ICollection<CategoryRelations> CategoryRelationsDescendantNavigation { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
