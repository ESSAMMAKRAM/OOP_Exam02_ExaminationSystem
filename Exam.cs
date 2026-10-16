namespace ExaminationSystem
{
    // Requirement 6: Base Exam class with the common attributes.
    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int Time { get; set; }                 // time of exam (minutes)
        public int NumberOfQuestions { get; set; }
        public Subject Subject { get; set; }          // Requirement 7: every exam belongs to a subject
        public Question[] Questions { get; set; }

        // Constructor chaining: Exam() -> Exam(time, n, subject)
        protected Exam() : this(0, 0, null) { }

        protected Exam(int time, int numberOfQuestions, Subject subject)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Subject = subject;
            Questions = new Question[numberOfQuestions];
        }

        // Each exam type implements its own way of showing the exam.
        public abstract void ShowExam();

        // Shows a question, then reads the student's answer id.
        protected Answer AskStudent(Question question)
        {
            question.Display();
            int id = InputHelper.ReadInt("Your answer (id): ", 1, question.AnswerList.Length);
            return question.AnswerList.First(a => a.AnswerId == id);
        }

        // Deep copy of the questions; the Subject reference is shared.
        public virtual object Clone()
        {
            Exam copy = (Exam)MemberwiseClone();

            copy.Questions = new Question[Questions.Length];
            for (int i = 0; i < Questions.Length; i++)
                copy.Questions[i] = Questions[i] == null ? null : (Question)Questions[i].Clone();

            return copy;
        }

        // Exams are compared by their time.
        public int CompareTo(Exam other)
        {
            if (other is null) return 1;
            return Time.CompareTo(other.Time);
        }

        public override string ToString()
        {
            return $"{GetType().Name} | Subject: {Subject?.SubjectName} | Time: {Time} min | Questions: {NumberOfQuestions}";
        }
    }

    // Requirement 9: Final exam shows the Questions, Answers and Grade.
    public class FinalExam : Exam
    {
        public FinalExam() : base() { }

        public FinalExam(int time, int numberOfQuestions, Subject subject)
            : base(time, numberOfQuestions, subject) { }

        public override void ShowExam()
        {
            Console.WriteLine($"\n===== Final Exam - {Subject.SubjectName} ({Time} minutes) =====");

            Answer[] studentAnswers = new Answer[Questions.Length];
            for (int i = 0; i < Questions.Length; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1}:");
                studentAnswers[i] = AskStudent(Questions[i]);
            }

            Console.WriteLine("\n===== Exam Result =====");
            int grade = 0;
            int total = 0;
            for (int i = 0; i < Questions.Length; i++)
            {
                Question q = Questions[i];
                total += q.Mark;
                bool correct = studentAnswers[i].AnswerId == q.RightAnswer.AnswerId;
                if (correct) grade += q.Mark;

                Console.WriteLine($"\nQuestion {i + 1}: {q.Body}");
                Console.WriteLine($"   Your answer: {studentAnswers[i].AnswerText}  ({(correct ? "Correct" : "Wrong")})");
            }

            Console.WriteLine($"\nGrade: {grade} / {total}");
        }
    }

    // Requirement 8: Practical exam shows the right answer after finishing the exam.
    public class PracticalExam : Exam
    {
        public PracticalExam() : base() { }

        public PracticalExam(int time, int numberOfQuestions, Subject subject)
            : base(time, numberOfQuestions, subject) { }

        public override void ShowExam()
        {
            Console.WriteLine($"\n===== Practical Exam - {Subject.SubjectName} ({Time} minutes) =====");

            for (int i = 0; i < Questions.Length; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1}:");
                AskStudent(Questions[i]);
            }

            Console.WriteLine("\n===== Right Answers =====");
            for (int i = 0; i < Questions.Length; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1}: {Questions[i].Body}");
                Console.WriteLine($"   Right answer: {Questions[i].RightAnswer.AnswerText}");
            }
        }
    }
}
