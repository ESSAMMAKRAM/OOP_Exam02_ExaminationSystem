namespace ExaminationSystem
{
    // Requirement 7: Subject (Id, Name, Exam) + functionality to create its exam.
    public class Subject : ICloneable, IComparable<Subject>
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; set; }

        public Subject() : this(0, string.Empty) { }

        public Subject(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
        }

        // Creates the exam of the subject (Final or Practical) by reading the data from the console.
        public void CreateExam()
        {
            Console.WriteLine("Choose the exam type:");
            Console.WriteLine("   1. Final");
            Console.WriteLine("   2. Practical");
            int examType = InputHelper.ReadInt("Exam type: ", 1, 2);

            int time = InputHelper.ReadInt("Time of exam (minutes): ", 1, 600);
            int count = InputHelper.ReadInt("Number of questions: ", 1, 100);

            Exam = examType == 1
                ? new FinalExam(time, count, this)
                : new PracticalExam(time, count, this);

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Question {i + 1} ---");

                if (examType == 1)
                {
                    // Final exam: True/False or MCQ
                    Console.WriteLine("Question type: 1. True or False   2. MCQ");
                    int kind = InputHelper.ReadInt("Type: ", 1, 2);
                    Exam.Questions[i] = kind == 1 ? ReadTrueFalseQuestion() : ReadMcqQuestion();
                }
                else
                {
                    // Practical exam: MCQ only
                    Exam.Questions[i] = ReadMcqQuestion();
                }
            }
        }

        private static Question ReadTrueFalseQuestion()
        {
            string body = InputHelper.ReadString("Question body: ");
            int mark = InputHelper.ReadInt("Mark: ", 1, 100);
            int right = InputHelper.ReadInt("Right answer (1 = True, 2 = False): ", 1, 2);
            return new TrueFalseQuestion(body, mark, right);
        }

        private static Question ReadMcqQuestion()
        {
            string body = InputHelper.ReadString("Question body: ");
            int mark = InputHelper.ReadInt("Mark: ", 1, 100);
            int choices = InputHelper.ReadInt("Number of choices (2-6): ", 2, 6);

            Answer[] answers = new Answer[choices];
            for (int i = 0; i < choices; i++)
                answers[i] = new Answer(i + 1, InputHelper.ReadString($"Choice {i + 1}: "));

            int right = InputHelper.ReadInt("Right answer id: ", 1, choices);
            return new MCQQuestion(body, mark, answers, right);
        }

        // Deep copy: the exam is cloned too and re-linked to the cloned subject.
        public object Clone()
        {
            Subject copy = (Subject)MemberwiseClone();
            if (Exam != null)
            {
                copy.Exam = (Exam)Exam.Clone();
                copy.Exam.Subject = copy;
            }
            return copy;
        }

        public int CompareTo(Subject other)
        {
            if (other is null) return 1;
            return SubjectId.CompareTo(other.SubjectId);
        }

        public override string ToString()
        {
            return $"Subject {SubjectId}: {SubjectName}";
        }
    }
}
