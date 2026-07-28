using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Package Volume Calculator ===\n");

        double length = ReadPositiveDouble("Enter Length (cm): ");
        double width  = ReadPositiveDouble("Enter Width (cm): ");
        double height = ReadPositiveDouble("Enter Height (cm): ");

        double volume = length * width * height;

        Console.WriteLine();
        Console.WriteLine($"Length   : {length} cm");
        Console.WriteLine($"Width    : {width} cm");
        Console.WriteLine($"Height   : {height} cm");
        Console.WriteLine($"Volume   : {Math.Round(volume, 2)} cm³");
    }

    static double ReadPositiveDouble(string prompt)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (!double.TryParse(input, out value))
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                continue;
            }
            if (value <= 0)
            {
                Console.WriteLine("Value must be greater than zero. Please try again.");
                continue;
            }
            break;
        }
        return value;
    }
}
