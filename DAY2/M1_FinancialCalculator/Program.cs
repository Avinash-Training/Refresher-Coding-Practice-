using System;

// M1 - Static class with overloaded methods, default parameters, and named arguments
// Formula: FV = P * (1 + r/n)^(n*t)
static class FinancialCalculator
{
    // Overload 1: accepts annual rate, time in years and compounding frequency with defaults
    public static double CalculateCompoundInterest(
        double principal,
        double rate,
        double time = 1,
        int compoundingFrequency = 1)
    {
        if (principal <= 0) throw new ArgumentException("Principal must be positive.");
        if (rate < 0)       throw new ArgumentException("Rate cannot be negative.");
        if (time <= 0)      throw new ArgumentException("Time must be positive.");
        if (compoundingFrequency <= 0) throw new ArgumentException("Compounding frequency must be positive.");

        double futureValue = principal * Math.Pow(1 + rate / compoundingFrequency,
                                                   compoundingFrequency * time);
        return Math.Round(futureValue, 2);
    }

    // Overload 2: accepts monthly rate and total months directly
    public static double CalculateCompoundInterest(double principal, double monthlyRate, int months)
    {
        if (principal <= 0)  throw new ArgumentException("Principal must be positive.");
        if (monthlyRate < 0) throw new ArgumentException("Rate cannot be negative.");
        if (months <= 0)     throw new ArgumentException("Months must be positive.");

        double futureValue = principal * Math.Pow(1 + monthlyRate, months);
        return Math.Round(futureValue, 2);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== M1: Financial Calculator - Compound Interest ===\n");

        // Call 1: annual compounding using default compoundingFrequency = 1
        double result1 = FinancialCalculator.CalculateCompoundInterest(10000, 0.05, 10);
        Console.WriteLine($"Call 1 - CalculateCompoundInterest(10000, 0.05, 10)");
        Console.WriteLine($"  Compounding : Annually (default)");
        Console.WriteLine($"  Future Value: ${result1:N2}\n");

        // Call 2: monthly compounding using named argument for clarity
        double result2 = FinancialCalculator.CalculateCompoundInterest(
            principal: 10000,
            rate: 0.05,
            time: 10,
            compoundingFrequency: 12);
        Console.WriteLine($"Call 2 - CalculateCompoundInterest(principal:10000, rate:0.05, time:10, compoundingFrequency:12)");
        Console.WriteLine($"  Compounding : Monthly");
        Console.WriteLine($"  Future Value: ${result2:N2}\n");

        // Call 3: quarterly compounding, only frequency changed using named arg
        double result3 = FinancialCalculator.CalculateCompoundInterest(
            10000, 0.05, 10, compoundingFrequency: 4);
        Console.WriteLine($"Call 3 - CalculateCompoundInterest(10000, 0.05, 10, compoundingFrequency:4)");
        Console.WriteLine($"  Compounding : Quarterly");
        Console.WriteLine($"  Future Value: ${result3:N2}\n");

        // Call 4: overload 2 - passing monthly rate and total months directly
        double result4 = FinancialCalculator.CalculateCompoundInterest(10000, 0.004167, 120);
        Console.WriteLine($"Call 4 - Overload: CalculateCompoundInterest(10000, monthlyRate:0.004167, months:120)");
        Console.WriteLine($"  Future Value: ${result4:N2}\n");

        Console.WriteLine("Named arguments make call intent clear:");
        Console.WriteLine("  Without: CalculateCompoundInterest(10000, 0.05, 10, 12) - '12' is ambiguous");
        Console.WriteLine("  With   : CalculateCompoundInterest(principal:10000, rate:0.05, time:10, compoundingFrequency:12)");
    }
}
