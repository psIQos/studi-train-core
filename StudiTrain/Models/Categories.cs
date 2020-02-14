using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class Categories
    {
        public Categories()
        {
            InverseAncestorNavigation = new HashSet<Categories>();
            Questions = new HashSet<Questions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? Ancestor { get; set; }

        public virtual Categories AncestorNavigation { get; set; }
        public virtual ICollection<Categories> InverseAncestorNavigation { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
