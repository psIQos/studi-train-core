using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class CategoryRelations
    {
        public int? Ancestor { get; set; }
        public int Descendant { get; set; }

        public virtual Categories AncestorNavigation { get; set; }
        public virtual Categories DescendantNavigation { get; set; }
    }
}
