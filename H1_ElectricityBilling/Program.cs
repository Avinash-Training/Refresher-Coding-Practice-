using System;

// Interface defining the billing contract
interface IBillingCalculator
{
    double CalculateBill(double unitsConsumed, double ratePerUnit, double fixedCharges);
}

// Residential customers: simple unit-based billing
class ResidentialBilling : IBillingCalculator
{
    public double CalculateBill(double unitsConsumed, double ratePerUnit, double fixedCharges)
    {
        return (unitsConsumed * ratePerUnit) + fixedCharges;
    }
}

// Commercial customers: 15% surcharge on top of base bill
class CommercialBilling : IBillingCalculator
{
    private const double CommercialSurchargeRate = 0.15;

    public double CalculateBill(double unitsConsumed, double ratePerUnit, double fixedCharges)
    {
        double baseBill = (unitsConsumed * ratePerUnit) + fixedCharges;
        double surcharge = baseBill * CommercialSurchargeRate;
        return baseBill + surcharge;
    }
}

// Factory to resolve the correct calculator by customer type
class BillingFactory
{
    public static IBillingCalculator GetCalculator(string customerType)
    {
        switch (customerType.Trim().ToLower())
        {
            case "residential": return new ResidentialBilling();
            case "commercial":  return new CommercialBilling();
            default:            return null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Utility Electricity Billing Calculator ===\n");

        // Customer type
        string customerType = "";
        IBillingCalculator calculator = null;
        while (calculator == null)
        {
            Console.Write("Enter Customer Type (Residential / Commercial): ");
            customerType = Console.ReadLine();
            calculator = BillingFactory.GetCalculator(customerType);
            if (calculator == null)
                Console.WriteLine("Invalid customer type. Please enter 'Residential' or 'Commercial'.");
        }

        double units        = ReadPositiveDouble("Enter Units Consumed (kWh): ");
        double rate         = ReadPositiveDouble("Enter Rate per Unit (₹): ");
        double fixedCharges = ReadNonNegativeDouble("Enter Fixed Charges (₹): ");

        double totalBill = calculator.CalculateBill(units, rate, fixedCharges);

        Console.WriteLine();
        Console.WriteLine("=== Bill Summary ===");
        Console.WriteLine($"Customer Type  : {customerType.Trim()}");
        Console.WriteLine($"Units Consumed : {units} kWh");
        Console.WriteLine($"Rate per Unit  : ₹{rate}");
        Console.WriteLine($"Fixed Charges  : ₹{fixedCharges}");
        if (customerType.Trim().ToLower() == "commercial")
            Console.WriteLine("Commercial Surcharge (15%) applied.");
        Console.WriteLine($"Total Bill     : ₹{Math.Round(totalBill, 2)}");
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

    static double ReadNonNegativeDouble(string prompt)
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
            if (value < 0)
            {
                Console.WriteLine("Value cannot be negative.");
                continue;
            }
            break;
        }
        return value;
    }
}
