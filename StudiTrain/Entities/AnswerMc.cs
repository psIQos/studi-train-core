using System.ComponentModel.DataAnnotations;

namespace StudiTrain.Entities
{
    public class AnswerMc
    {
        public int Number { get; set; }

        [Required] public string AnswerText { get; set; }

        public bool? Correct { get; set; }
    }
}