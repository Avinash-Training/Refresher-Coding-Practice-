using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Opening Balance: ");
        string inputBalance = Console.ReadLine();

        double openingBalance;

        if (double.TryParse(inputBalance, out openingBalance))
        {
            if (openingBalance >= 0)
            {
                Console.WriteLine("Valid Opening Balance");
            }
            else
            {
                Console.WriteLine("Opening Balance cannot be negative.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Opening Balance");
            return;
        }

        Console.Write("Enter Total Deposits: ");
        string inputDeposit = Console.ReadLine();

        double deposits;

        if (double.TryParse(inputDeposit, out deposits))
        {
            if (deposits >= 0)
            {
                Console.WriteLine("Valid Deposits");
            }
            else
            {
                Console.WriteLine("Deposits cannot be negative.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Deposits");
            return;
        }

        Console.Write("Enter Total Withdrawals: ");
        string inputWithdrawals = Console.ReadLine();

        double withdrawals;

        if (double.TryParse(inputWithdrawals, out withdrawals))
        {
            if (withdrawals >= 0)
            {
                Console.WriteLine("Valid Withdrawals");
            }
            else
            {
                Console.WriteLine("Withdrawals cannot be negative.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Withdrawals");
            return;
        }

        double availableBalance = openingBalance + deposits;

        if (withdrawals > availableBalance)
        {
            Console.WriteLine("Error: Withdrawals cannot exceed available balance.");
            return;
        }

        double finalBalance = availableBalance - withdrawals;

        Console.WriteLine();
        Console.WriteLine("Final Balance: " + finalBalance);
    }
}