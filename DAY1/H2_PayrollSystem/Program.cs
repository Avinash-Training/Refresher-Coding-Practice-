using System;

class Employee
{
    public string Name       { get; set; }
    public double HoursWorked { get; set; }
    public double HourlyRate  { get; set; }
}

class PayrollCalculator
{
    private const double OvertimeThreshold   = 40.0;  // hours per week
    private const double OvertimeMultiplier  = 1.5;

    public double CalculateRegularPay(Employee emp)
    {
        double regularHours = Math.Min(emp.HoursWorked, OvertimeThreshold);
        return regularHours * emp.HourlyRate;
    }

    public double CalculateOvertimePay(Employee emp)
    {
        if (emp.HoursWorked <= OvertimeThreshold)
            return 0;

        double overtimeHours = emp.HoursWorked - OvertimeThreshold;
        return overtimeHours * emp.HourlyRate * OvertimeMultiplier;
    }

    public double CalculateGrossSalary(Employee emp)
    {
        return CalculateRegularPay(emp) + CalculateOvertimePay(emp);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Payroll Calculator ===\n");

        // Employee name
        string name = "";
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("Enter Employee Name: ");
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
                Console.WriteLine("Name cannot be empty.");
        }

        double hoursWorked = ReadPositiveDouble("Enter Hours Worked this week: ");
        double hourlyRate  = ReadPositiveDouble("Enter Hourly Rate (₹): ");

        Employee employee = new Employee
        {
            Name        = name.Trim(),
            HoursWorked = hoursWorked,
            HourlyRate  = hourlyRate
        };

        PayrollCalculator calculator = new PayrollCalculator();
        double regularPay  = calculator.CalculateRegularPay(employee);
        double overtimePay = calculator.CalculateOvertimePay(employee);
        double grossSalary = calculator.CalculateGrossSalary(employee);

        Console.WriteLine();
        Console.WriteLine("=== Salary Summary ===");
        Console.WriteLine($"Employee Name  : {employee.Name}");
        Console.WriteLine($"Hours Worked   : {employee.HoursWorked} hrs");
        Console.WriteLine($"Hourly Rate    : ₹{employee.HourlyRate}");
        Console.WriteLine($"Regular Pay    : ₹{Math.Round(regularPay, 2)}");
        Console.WriteLine($"Overtime Pay   : ₹{Math.Round(overtimePay, 2)}");
        Console.WriteLine($"Gross Salary   : ₹{Math.Round(grossSalary, 2)}");
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
