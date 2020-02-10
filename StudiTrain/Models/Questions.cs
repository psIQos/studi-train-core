using System;
using System.Collections.Generic;

namespace StudiTrain.Models
{
    public sealed class Questions
    {
        public Questions()
        {
            AnswersMc = new HashSet<AnswersMc>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionTitle { get; set; }
        public int? QuestionType { get; set; }
        public bool? Complete { get; set; }

        public ICollection<AnswersMc> AnswersMc { get; set; }
    }
}
