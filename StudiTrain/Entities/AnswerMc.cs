using System.ComponentModel.DataAnnotations;
using StudiTrain.Models.Database;

namespace StudiTrain.Entities
{
    public class AnswerMc
    {
        public AnswerMc(AnswersMc answer)
        {
            Number = answer.Number;
            AnswerText = answer.Text;
            Correct = answer.Correct;
        }

        public int Number { get; set; }

        [Required] public string AnswerText { get; set; }

        public bool? Correct { get; set; }
    }
}