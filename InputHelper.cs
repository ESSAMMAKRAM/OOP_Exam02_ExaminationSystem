namespace ExaminationSystem
{
    // Small helper to read validated input from the console.
    public static class InputHelper
    {
        public static int ReadInt(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;

                Console.WriteLine($"Invalid input. Enter a number between {min} and {max}.");
            }
        }

        public static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();

                Console.WriteLine("Input cannot be empty.");
            }
        }
    }
}
