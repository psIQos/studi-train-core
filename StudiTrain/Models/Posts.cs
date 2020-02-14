using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class Posts
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? TagId { get; set; }
        public int QuestionId { get; set; }

        public virtual Questions Question { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
