using System;

// H4 - Factorial and Fibonacci using Recursion
// Uses: static methods, recursion, TryParse for input validation, ref parameter to count calls

static class MathRecursion
{
    // Calculates factorial of n using recursion
    // Factorial: 5! = 5 * 4 * 3 * 2 * 1 = 120
    public static long Factorial(int n)
    {
        if (n < 0) throw new ArgumentException("Number must be non-negative.");
        if (n == 0 || n == 1) return 1; // base case
        return n * Factorial(n - 1);    // recursive call
    }

    // Calculates nth Fibonacci number using recursion
    // Fibonacci: 0, 1, 1, 2, 3, 5, 8, 13, 21 ...
    public static int Fibonacci(int n)
    {
        if (n < 0) throw new ArgumentException("Number must be non-negative.");
        if (n == 0) return 0; // base case
        if (n == 1) return 1; // base case
        return Fibonacci(n - 1) + Fibonacci(n - 2); // recursive call
    }

    // Calculates sum of digits recursively - ref callCount tracks how many times it was called
    public static int SumOfDigits(int n, ref int callCount)
    {
        callCount++;
        if (n < 10) return n; // base case: single digit
        return (n % 10) + SumOfDigits(n / 10, ref callCount); // add last digit and recurse
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== H4: Recursion - Factorial, Fibonacci, Sum of Digits ===\n");

        // Factorial examples
        Console.WriteLine("--- Factorial ---");
        for (int i = 0; i <= 10; i++)
            Console.WriteLine($"  {i}! = {MathRecursion.Factorial(i)}");

        Console.WriteLine();

        // Fibonacci examples
        Console.WriteLine("--- Fibonacci ---");
        for (int i = 0; i <= 10; i++)
            Console.Write($"  F({i}) = {MathRecursion.Fibonacci(i)}\n");

        Console.WriteLine();

        // Sum of digits with ref call counter
        Console.WriteLine("--- Sum of Digits ---");
        int[] numbers = { 123, 4567, 99, 1000, 7 };
        foreach (int num in numbers)
        {
            int callCount = 0;
            int sum = MathRecursion.SumOfDigits(num, ref callCount);
            Console.WriteLine($"  SumOfDigits({num}) = {sum}  (recursive calls: {callCount})");
        }

        Console.WriteLine();

        // Read a number from user and calculate factorial using TryParse
        Console.Write("Enter a number to calculate its factorial: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int userNum) && userNum >= 0 && userNum <= 20)
        {
            Console.WriteLine($"  {userNum}! = {MathRecursion.Factorial(userNum)}");
        }
        else
        {
            Console.WriteLine("  Invalid input. Please enter a whole number between 0 and 20.");
        }
    }
}
