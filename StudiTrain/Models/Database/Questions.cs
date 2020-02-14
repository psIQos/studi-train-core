using System.Collections.Generic;
using System.Linq;
using StudiTrain.Entities;

namespace StudiTrain.Models.Database
{
    public partial class Questions
    {
        public Questions()
        {
            AnswersMc = new HashSet<AnswersMc>();
            Posts = new HashSet<Posts>();
        }

        public Questions(Question question)
        {
            QuestionText = question.QuestionText;
            QuestionTitle = question.Title;
            if (question.answers == null) return;
            AnswersMc = new List<AnswersMc>();
            foreach (var item in question.answers.Select((value, index) => (value, index))) 
                AnswersMc.Add(new AnswersMc(item.value, item.index));
            Complete = question.answers.Count() >= 4 && question.answers.All(a => a.Correct != null);
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
