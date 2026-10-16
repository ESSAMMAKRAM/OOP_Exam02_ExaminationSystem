namespace ExaminationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Requirement 10: declare a subject object to create one type of exam.
            Subject subject = new Subject(1, "OOP");

            subject.CreateExam();

            Console.WriteLine($"\n{subject}");
            Console.WriteLine(subject.Exam);

            subject.Exam.ShowExam();
        }
    }
}
