using System;

// Interface for all investment calculators
interface IInvestmentCalculator
{
    double CalculateReturn(double principal, double annualRatePercent, int years);
    string InvestmentType { get; }
}

// Simple Interest: Return = P + (P * R * T) / 100
class SimpleInterestCalculator : IInvestmentCalculator
{
    public string InvestmentType => "Simple Interest";

    public double CalculateReturn(double principal, double annualRatePercent, int years)
    {
        double interest = (principal * annualRatePercent * years) / 100;
        return principal + interest;
    }
}

// Compound Interest: Return = P * (1 + R/100)^T
class CompoundInterestCalculator : IInvestmentCalculator
{
    public string InvestmentType => "Compound Interest (Annual)";

    public double CalculateReturn(double principal, double annualRatePercent, int years)
    {
        return principal * Math.Pow(1 + (annualRatePercent / 100), years);
    }
}

// Recurring Deposit: Future Value = P * [((1 + R/100)^T - 1) / (R/100)]
class RecurringDepositCalculator : IInvestmentCalculator
{
    public string InvestmentType => "Recurring Deposit";

    public double CalculateReturn(double monthlyDeposit, double annualRatePercent, int years)
    {
        double monthlyRate = annualRatePercent / (12 * 100);
        int    months      = years * 12;
        // Standard RD maturity formula
        double maturity = monthlyDeposit * ((Math.Pow(1 + monthlyRate, months) - 1) / monthlyRate) * (1 + monthlyRate);
        return maturity;
    }
}

class InvestmentFactory
{
    public static IInvestmentCalculator GetCalculator(string type)
    {
        switch (type.Trim().ToLower())
        {
            case "simple":    return new SimpleInterestCalculator();
            case "compound":  return new CompoundInterestCalculator();
            case "recurring": return new RecurringDepositCalculator();
            default:          return null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Investment Return Calculator ===\n");

        // Investment type
        IInvestmentCalculator calculator = null;
        string investmentType = "";
        while (calculator == null)
        {
            Console.Write("Enter Investment Type (Simple / Compound / Recurring): ");
            investmentType = Console.ReadLine();
            calculator = InvestmentFactory.GetCalculator(investmentType);
            if (calculator == null)
                Console.WriteLine("Invalid type. Please enter Simple, Compound, or Recurring.");
        }

        double principal = ReadPositiveDouble(
            calculator.InvestmentType.Contains("Recurring")
                ? "Enter Monthly Deposit Amount (₹): "
                : "Enter Principal Amount (₹): ");

        double rate = ReadRateDouble("Enter Annual Interest Rate (%): ");
        int    years = ReadPositiveInt("Enter Investment Duration (years): ");

        double projectedValue = calculator.CalculateReturn(principal, rate, years);
        double totalInvested  = calculator.InvestmentType.Contains("Recurring")
                                ? principal * years * 12
                                : principal;
        double totalReturn    = projectedValue - totalInvested;

        Console.WriteLine();
        Console.WriteLine("=== Investment Summary ===");
        Console.WriteLine($"Investment Type    : {calculator.InvestmentType}");
        Console.WriteLine($"Principal / Deposit: ₹{principal}");
        Console.WriteLine($"Annual Rate        : {rate}%");
        Console.WriteLine($"Duration           : {years} year(s)");
        Console.WriteLine($"Total Invested     : ₹{Math.Round(totalInvested, 2)}");
        Console.WriteLine($"Projected Value    : ₹{Math.Round(projectedValue, 2)}");
        Console.WriteLine($"Total Return       : ₹{Math.Round(totalReturn, 2)}");
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
                Console.WriteLine("Value must be greater than zero.");
                continue;
            }
            break;
        }
        return value;
    }

    static double ReadRateDouble(string prompt)
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
            if (value <= 0 || value > 100)
            {
                Console.WriteLine("Rate must be between 0.01 and 100.");
                continue;
            }
            break;
        }
        return value;
    }

    static int ReadPositiveInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!int.TryParse(input, out value))
            {
                Console.WriteLine("Invalid input. Please enter a whole number.");
                continue;
            }
            if (value <= 0)
            {
                Console.WriteLine("Duration must be at least 1 year.");
                continue;
            }
            break;
        }
        return value;
    }
}
