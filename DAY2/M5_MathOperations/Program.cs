using System;

// M5 - Math Operations
// Demonstrates method overloading and the params keyword for variable arguments
static class MathOperations
{
    // Two-param overload: no array allocation, preferred for known fixed arguments
    public static int Add(int a, int b) => a + b;

    // params overload: accepts any number of ints, useful when count is unknown at compile time
    public static int Add(params int[] numbers)
    {
        int sum = 0;
        foreach (int n in numbers) sum += n;
        return sum;
    }

    // Two-param overload for multiply
    public static int Multiply(int a, int b) => a * b;

    // params overload for multiply: starts at 1 (identity for multiplication)
    public static int Multiply(params int[] numbers)
    {
        int product = 1;
        foreach (int n in numbers) product *= n;
        return product;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== M5: Math Operations - params and Overloading ===\n");

        Console.WriteLine("--- Addition ---");
        Console.WriteLine($"Add(5, 10)          = {MathOperations.Add(5, 10)}");
        Console.WriteLine($"Add(1,2,3,4,5)      = {MathOperations.Add(1, 2, 3, 4, 5)}");
        Console.WriteLine($"Add(10, 20, 30)     = {MathOperations.Add(10, 20, 30)}");
        Console.WriteLine($"Add()               = {MathOperations.Add()}");

        Console.WriteLine();
        Console.WriteLine("--- Multiplication ---");
        Console.WriteLine($"Multiply(2, 3)      = {MathOperations.Multiply(2, 3)}");
        Console.WriteLine($"Multiply(2,3,4,5)   = {MathOperations.Multiply(2, 3, 4, 5)}");
        Console.WriteLine($"Multiply(1,2,3,4,5) = {MathOperations.Multiply(1, 2, 3, 4, 5)}");
        Console.WriteLine($"Multiply()          = {MathOperations.Multiply()}");

        Console.WriteLine();

        // Passing an existing array works the same as inline values
        int[] scores = { 10, 20, 30, 40 };
        Console.WriteLine($"Add(int[] scores)   = {MathOperations.Add(scores)}");

        Console.WriteLine();
        Console.WriteLine("params is useful when the number of arguments varies at runtime,");
        Console.WriteLine("for example summing scores for a variable number of players in a game.");
        Console.WriteLine();
        Console.WriteLine("Pitfalls:");
        Console.WriteLine("1. params must be the last parameter - cannot put anything after it.");
        Console.WriteLine("2. Only one params parameter is allowed per method signature.");
        Console.WriteLine("3. Every call with inline values allocates a new int[] on the heap.");
        Console.WriteLine("4. Add(5,10) matches both overloads - C# picks the more specific one,");
        Console.WriteLine("   but this can confuse other developers reading the code.");
    }
}
