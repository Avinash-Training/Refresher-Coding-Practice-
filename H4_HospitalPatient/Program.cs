using System;

class PatientData
{
    public string Name        { get; set; }
    public int    Age         { get; set; }
    public double WeightKg    { get; set; }
    public double HeightM     { get; set; }
    public double Temperature { get; set; }  // in Celsius
}

// Centralised validation layer — all validation logic lives here
class Validator
{
    public static bool TryValidateAge(string input, out int age)
    {
        age = 0;
        if (!int.TryParse(input, out age))        return false;
        if (age <= 0 || age > 130)                return false;
        return true;
    }

    public static bool TryValidateWeight(string input, out double weight)
    {
        weight = 0;
        if (!double.TryParse(input, out weight))  return false;
        if (weight <= 0 || weight > 500)          return false;
        return true;
    }

    public static bool TryValidateHeight(string input, out double height)
    {
        height = 0;
        if (!double.TryParse(input, out height))  return false;
        if (height <= 0 || height > 3.0)          return false;
        return true;
    }

    public static bool TryValidateTemperature(string input, out double temp)
    {
        temp = 0;
        if (!double.TryParse(input, out temp))    return false;
        if (temp < 30.0 || temp > 45.0)           return false;
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Hospital Patient Registration System ===\n");

        PatientData patient = new PatientData();

        // Name
        while (string.IsNullOrWhiteSpace(patient.Name))
        {
            Console.Write("Enter Patient Name: ");
            patient.Name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(patient.Name))
                Console.WriteLine("Name cannot be empty. Please try again.");
        }

        // Age
        while (true)
        {
            Console.Write("Enter Age (1–130): ");
            string input = Console.ReadLine();
            if (Validator.TryValidateAge(input, out int age))
            {
                patient.Age = age;
                break;
            }
            Console.WriteLine("Invalid age. Please enter a whole number between 1 and 130.");
        }

        // Weight
        while (true)
        {
            Console.Write("Enter Weight in kg (0–500): ");
            string input = Console.ReadLine();
            if (Validator.TryValidateWeight(input, out double weight))
            {
                patient.WeightKg = weight;
                break;
            }
            Console.WriteLine("Invalid weight. Please enter a value between 0.1 and 500 kg.");
        }

        // Height
        while (true)
        {
            Console.Write("Enter Height in meters (0–3.0): ");
            string input = Console.ReadLine();
            if (Validator.TryValidateHeight(input, out double height))
            {
                patient.HeightM = height;
                break;
            }
            Console.WriteLine("Invalid height. Please enter a value between 0.01 and 3.0 meters.");
        }

        // Temperature
        while (true)
        {
            Console.Write("Enter Body Temperature in °C (30.0–45.0): ");
            string input = Console.ReadLine();
            if (Validator.TryValidateTemperature(input, out double temp))
            {
                patient.Temperature = temp;
                break;
            }
            Console.WriteLine("Invalid temperature. Please enter a value between 30.0°C and 45.0°C.");
        }

        // BMI calculation
        double bmi = patient.WeightKg / (patient.HeightM * patient.HeightM);
        string bmiCategory;
        if      (bmi < 18.5)              bmiCategory = "Underweight";
        else if (bmi >= 18.5 && bmi < 25) bmiCategory = "Normal Weight";
        else if (bmi >= 25 && bmi < 30)   bmiCategory = "Overweight";
        else                              bmiCategory = "Obese";

        string tempStatus = patient.Temperature >= 37.5 ? "Fever" : "Normal";

        // Patient Summary
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║     PATIENT SUMMARY          ║");
        Console.WriteLine("╚══════════════════════════════╝");
        Console.WriteLine($"Name           : {patient.Name}");
        Console.WriteLine($"Age            : {patient.Age} years");
        Console.WriteLine($"Weight         : {patient.WeightKg} kg");
        Console.WriteLine($"Height         : {patient.HeightM} m");
        Console.WriteLine($"Temperature    : {patient.Temperature}°C ({tempStatus})");
        Console.WriteLine($"BMI            : {Math.Round(bmi, 2)} ({bmiCategory})");
    }
}
