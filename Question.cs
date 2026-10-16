namespace ExaminationSystem
{
    // Requirements 1, 3, 5: Base Question class (Header, Body, Mark)
    // associated with an array of answers and its right answer.
    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public Answer[] AnswerList { get; set; }
        public Answer RightAnswer { get; set; }

        // Constructor chaining: Question() -> Question(h, b, m) -> Question(h, b, m, answers, right)
        protected Question() : this(string.Empty, string.Empty, 0) { }

        protected Question(string header, string body, int mark)
            : this(header, body, mark, new Answer[0], null) { }

        protected Question(string header, string body, int mark, Answer[] answerList, Answer rightAnswer)
        {
            Header = header;
            Body = body;
            Mark = mark;
            AnswerList = answerList;
            RightAnswer = rightAnswer;
        }

        // Each question type displays itself in its own way.
        public abstract void Display();

        protected void DisplayCommon()
        {
            Console.WriteLine($"{Header}    (Mark: {Mark})");
            Console.WriteLine(Body);
            foreach (Answer answer in AnswerList)
                Console.WriteLine($"   {answer}");
        }

        // Deep copy: the answers are cloned too, and RightAnswer points to the cloned answer.
        public virtual object Clone()
        {
            Question copy = (Question)MemberwiseClone();

            copy.AnswerList = new Answer[AnswerList.Length];
            for (int i = 0; i < AnswerList.Length; i++)
                copy.AnswerList[i] = (Answer)AnswerList[i].Clone();

            copy.RightAnswer = RightAnswer == null
                ? null
                : copy.AnswerList.FirstOrDefault(a => a.AnswerId == RightAnswer.AnswerId);

            return copy;
        }

        // Questions are compared by their Mark.
        public int CompareTo(Question other)
        {
            if (other is null) return 1;
            return Mark.CompareTo(other.Mark);
        }

        public override string ToString()
        {
            return $"{Header} | {Body} | Mark: {Mark}";
        }
    }

    // Final exam question: True or False
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion() : base() { }

        // rightAnswerId: 1 = True, 2 = False
        public TrueFalseQuestion(string body, int mark, int rightAnswerId)
            : base("True or False Question", body, mark,
                   new Answer[] { new Answer(1, "True"), new Answer(2, "False") }, null)
        {
            RightAnswer = AnswerList.First(a => a.AnswerId == rightAnswerId);
        }

        public override void Display()
        {
            DisplayCommon();
        }
    }

    // Final and Practical exam question: MCQ (choose one answer)
    public class MCQQuestion : Question
    {
        public MCQQuestion() : base() { }

        public MCQQuestion(string body, int mark, Answer[] answers, int rightAnswerId)
            : base("MCQ Question (choose one answer)", body, mark, answers, null)
        {
            RightAnswer = AnswerList.First(a => a.AnswerId == rightAnswerId);
        }

        public override void Display()
        {
            DisplayCommon();
        }
    }
}
