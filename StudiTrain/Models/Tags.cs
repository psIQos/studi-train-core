using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class Tags
    {
        public Tags()
        {
            Posts = new HashSet<Posts>();
            Questions = new HashSet<Questions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
