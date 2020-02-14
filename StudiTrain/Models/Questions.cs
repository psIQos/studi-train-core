using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public partial class Questions
    {
        public Questions()
        {
            AnswersMc = new HashSet<AnswersMc>();
            Posts = new HashSet<Posts>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionTitle { get; set; }
        public int? QuestionType { get; set; }
        public bool? Complete { get; set; }
        public int? Category { get; set; }
        public int? Tag { get; set; }

        public virtual Categories CategoryNavigation { get; set; }
        public virtual Tags TagNavigation { get; set; }
        public virtual ICollection<AnswersMc> AnswersMc { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
