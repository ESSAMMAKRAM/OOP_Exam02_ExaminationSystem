namespace ExaminationSystem
{
    // Requirement 4: class for the answers (AnswerId, AnswerText)
    public class Answer : ICloneable, IComparable<Answer>
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer() : this(0, string.Empty) { }

        public Answer(int answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText;
        }

        public object Clone()
        {
            return new Answer(AnswerId, AnswerText);
        }

        public int CompareTo(Answer other)
        {
            if (other is null) return 1;
            return AnswerId.CompareTo(other.AnswerId);
        }

        public override string ToString()
        {
            return $"{AnswerId}. {AnswerText}";
        }
    }
}
