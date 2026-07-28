using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Student Performance Calculator ===\n");

        double mark1 = ReadMark("Enter marks for Subject 1 (0-100): ");
        double mark2 = ReadMark("Enter marks for Subject 2 (0-100): ");
        double mark3 = ReadMark("Enter marks for Subject 3 (0-100): ");
        double mark4 = ReadMark("Enter marks for Subject 4 (0-100): ");
        double mark5 = ReadMark("Enter marks for Subject 5 (0-100): ");

        double total      = mark1 + mark2 + mark3 + mark4 + mark5;
        double average    = total / 5;
        double percentage = (total / 500) * 100;

        Console.WriteLine();
        Console.WriteLine("=== Results ===");
        Console.WriteLine($"Total Marks  : {total} / 500");
        Console.WriteLine($"Average      : {Math.Round(average, 2)}");
        Console.WriteLine($"Percentage   : {Math.Round(percentage, 2)}%");
    }

    static double ReadMark(string prompt)
    {
        double mark;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (!double.TryParse(input, out mark))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
                continue;
            }
            if (mark < 0 || mark > 100)
            {
                Console.WriteLine("Marks must be between 0 and 100. Please try again.");
                continue;
            }
            break;
        }
        return mark;
    }
}
