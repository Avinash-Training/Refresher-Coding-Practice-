using System;

// H5 - Student Grade System
// Uses: static methods, method overloading, params, out parameter, TryParse, default arguments

static class GradeCalculator
{
    // Overload 1: calculates average from exactly 3 subject marks
    public static double GetAverage(double s1, double s2, double s3)
    {
        return (s1 + s2 + s3) / 3;
    }

    // Overload 2: calculates average from any number of marks using params
    public static double GetAverage(params double[] marks)
    {
        if (marks == null || marks.Length == 0) return 0;
        double total = 0;
        foreach (double m in marks) total += m;
        return total / marks.Length;
    }

    // Returns the letter grade based on average score
    // defaultGrade is returned if average is exactly 0 - shows default parameter usage
    public static string GetGrade(double average, string defaultGrade = "N/A")
    {
        if (average == 0) return defaultGrade;
        if (average >= 90) return "A";
        if (average >= 75) return "B";
        if (average >= 60) return "C";
        if (average >= 45) return "D";
        return "F";
    }

    // Uses out to return both pass/fail status and the grade in one call
    public static bool TryEvaluate(double average, out string grade, out string status)
    {
        grade  = GetGrade(average);
        status = average >= 45 ? "Pass" : "Fail";
        return average >= 45;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== H5: Student Grade System ===\n");

        // Overload 1 - exactly 3 marks
        double avg1 = GradeCalculator.GetAverage(78, 85, 92);
        Console.WriteLine($"3-subject average (78, 85, 92): {avg1:F2}  Grade: {GradeCalculator.GetGrade(avg1)}");

        // Overload 2 - variable number of marks using params
        double avg2 = GradeCalculator.GetAverage(55, 60, 70, 80, 90);
        Console.WriteLine($"5-subject average (55,60,70,80,90): {avg2:F2}  Grade: {GradeCalculator.GetGrade(avg2)}");

        // Default parameter - returns "N/A" when average is 0
        Console.WriteLine($"Zero average grade: {GradeCalculator.GetGrade(0)}");

        Console.WriteLine();

        // out parameter usage - get grade and pass/fail in one call
        Console.WriteLine("--- Evaluation using out parameters ---");
        double[] testAverages = { 92.0, 74.0, 55.0, 40.0 };
        foreach (double avg in testAverages)
        {
            bool passed = GradeCalculator.TryEvaluate(avg, out string grade, out string status);
            Console.WriteLine($"  Average: {avg}  Grade: {grade}  Status: {status}  Passed: {passed}");
        }

        Console.WriteLine();

        // Read marks from user with TryParse validation
        Console.WriteLine("--- Enter your marks ---");
        double mark1 = ReadMark("Mark 1: ");
        double mark2 = ReadMark("Mark 2: ");
        double mark3 = ReadMark("Mark 3: ");

        double userAvg = GradeCalculator.GetAverage(mark1, mark2, mark3);
        GradeCalculator.TryEvaluate(userAvg, out string userGrade, out string userStatus);

        Console.WriteLine($"\nAverage: {userAvg:F2}  Grade: {userGrade}  Result: {userStatus}");
    }

    // Helper: reads and validates a mark between 0 and 100 using TryParse
    static double ReadMark(string prompt)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (double.TryParse(input, out value) && value >= 0 && value <= 100)
                return value;
            Console.WriteLine("  Invalid. Enter a number between 0 and 100.");
        }
    }
}
