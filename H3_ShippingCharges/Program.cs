using System;

// Interface for shipping cost strategy
interface IShippingCalculator
{
    double CalculateCost(double weightKg, double distanceKm);
}

// Standard packages: flat rate per kg per km
class StandardShipping : IShippingCalculator
{
    private const double RatePerKgPerKm = 0.05;

    public double CalculateCost(double weightKg, double distanceKm)
    {
        return weightKg * distanceKm * RatePerKgPerKm;
    }
}

// Express packages: 2x rate + ₹50 handling fee
class ExpressShipping : IShippingCalculator
{
    private const double RatePerKgPerKm = 0.10;
    private const double HandlingFee    = 50.0;

    public double CalculateCost(double weightKg, double distanceKm)
    {
        return (weightKg * distanceKm * RatePerKgPerKm) + HandlingFee;
    }
}

// Fragile packages: 1.5x rate + ₹100 special handling
class FragileShipping : IShippingCalculator
{
    private const double RatePerKgPerKm  = 0.075;
    private const double SpecialHandling = 100.0;

    public double CalculateCost(double weightKg, double distanceKm)
    {
        return (weightKg * distanceKm * RatePerKgPerKm) + SpecialHandling;
    }
}

class ShippingFactory
{
    public static IShippingCalculator GetCalculator(string packageType)
    {
        switch (packageType.Trim().ToLower())
        {
            case "standard": return new StandardShipping();
            case "express":  return new ExpressShipping();
            case "fragile":  return new FragileShipping();
            default:         return null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Logistics Shipping Cost Calculator ===\n");

        // Package type selection
        IShippingCalculator calculator = null;
        string packageType = "";
        while (calculator == null)
        {
            Console.Write("Enter Package Type (Standard / Express / Fragile): ");
            packageType = Console.ReadLine();
            calculator = ShippingFactory.GetCalculator(packageType);
            if (calculator == null)
                Console.WriteLine("Invalid package type. Please enter Standard, Express, or Fragile.");
        }

        double weight   = ReadPositiveDouble("Enter Package Weight (kg): ");
        double distance = ReadPositiveDouble("Enter Shipping Distance (km): ");

        double cost = calculator.CalculateCost(weight, distance);

        Console.WriteLine();
        Console.WriteLine("=== Shipping Cost Summary ===");
        Console.WriteLine($"Package Type   : {packageType.Trim()}");
        Console.WriteLine($"Weight         : {weight} kg");
        Console.WriteLine($"Distance       : {distance} km");
        Console.WriteLine($"Shipping Cost  : ₹{Math.Round(cost, 2)}");
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
}
