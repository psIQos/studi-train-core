using StudiTrain.Entities;

namespace StudiTrain.Models.Database
{
    public partial class AnswersMc
    {
        public AnswersMc()
        {

        }

        public AnswersMc(AnswerMc answer, int number)
        {
            Number = number;
            Text = answer.AnswerText;
            Correct = answer.Correct;
        }

        public int QId { get; set; }
        public int Number { get; set; }
        public string Text { get; set; }
        public bool? Correct { get; set; }

        public virtual Questions Q { get; set; }
    }
}
