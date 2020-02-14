using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudiTrain.Entities
{
    public class Question
    {
        [Required] public string QuestionText { get; set; }

        public string Title { get; set; }

        [Required] public virtual IEnumerable<AnswerMc> answers { get; set; }
    }
}