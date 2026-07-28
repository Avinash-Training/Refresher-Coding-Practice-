using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Weight (kg): ");
        string inputWeight = Console.ReadLine();

        double weight;

        if (double.TryParse(inputWeight, out weight))
        {
            if (weight > 0)
            {
                Console.WriteLine("Valid Weight");
            }
            else
            {
                Console.WriteLine("Weight must be greater than 0.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Weight");
            return;
        }

        Console.Write("Enter Height (m): ");
        string inputHeight = Console.ReadLine();

        double height;

        if (double.TryParse(inputHeight, out height))
        {
            if (height > 0)
            {
                Console.WriteLine("Valid Height");
            }
            else
            {
                Console.WriteLine("Height must be greater than 0.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Height");
            return;
        }

        double bmi = weight / (height * height);

        Console.WriteLine();
        Console.WriteLine("BMI = " + Math.Round(bmi, 2));

        if (bmi < 18.5)
        {
            Console.WriteLine("Category : Underweight");
        }
        else if (bmi >= 18.5 && bmi < 25)
        {
            Console.WriteLine("Category : Normal Weight");
        }
        else if (bmi >= 25 && bmi < 30)
        {
            Console.WriteLine("Category : Overweight");
        }
        else
        {
            Console.WriteLine("Category : Obese");
        }
    }
}

