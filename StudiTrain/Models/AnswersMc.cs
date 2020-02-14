using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class AnswersMc
    {
        public int QId { get; set; }
        public int Number { get; set; }
        public string Text { get; set; }
        public bool? Correct { get; set; }

        public virtual Questions Q { get; set; }
    }
}
