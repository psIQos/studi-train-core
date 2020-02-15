using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using StudiTrain.Models.Database;

namespace StudiTrain.Entities
{
    public class Question
    {
        public Question()
        {

        }

        public Question(Questions question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));
            QuestionText = question.QuestionText;
            Title = question.QuestionTitle;
            Answers = question.AnswersMc.Select(a=> new AnswerMc(a));
        }

        [Required] public string QuestionText { get; set; }

        public string Title { get; set; }

        [Required] public IEnumerable<AnswerMc> Answers { get; set; }
    }
}