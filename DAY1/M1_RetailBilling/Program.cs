using System;
using System.Transactions;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Item Price: ");
        string inputPrice = Console.ReadLine();

        double price;

        if (double.TryParse(inputPrice, out price))
        {
            if (price >= 0)
            {
                Console.WriteLine("Valid Price");
            }
            else
            {
                Console.WriteLine("Price cannot be negative.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Price");
            return;
        }

        Console.Write("Enter Quantity: ");
        string inputQuantity = Console.ReadLine();

        int quantity;

        if (int.TryParse(inputQuantity, out quantity))
        {
            if (quantity > 0)
            {
                Console.WriteLine("Valid Quantity");
            }
            else
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Quantity");
            return;
        }
        Console.Write("Enter Discount percentage: ");

        string inputDiscount = Console.ReadLine();

        double discount;

        if(double.TryParse(inputDiscount, out discount))
        {
            if(discount >= 0 && discount <= 100)
            {
                Console.WriteLine("Valid Discount");
            }
            else
            {
                Console.WriteLine("Discount must be between 0 and 100");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Discount");
            return;
        }
        double subtotal = price * quantity;
        double discountAmount = subtotal * discount/100;
        double finalAmount = subtotal - discountAmount;


        Console.WriteLine();

        Console.WriteLine("Subtotal = " + Math.Round(subtotal, 2));
         Console.WriteLine("DiscountAmount = " + Math.Round(discountAmount, 2));
          Console.WriteLine("FinalAmount = " + Math.Round(finalAmount, 2));



    }
}