using System;

// M4 - Geometry Library
// Demonstrates method overloading, default parameter values, and named arguments
static class GeometryLibrary
{
    // Overload 1: Circle - single double parameter identifies this as the circle overload
    // decimals has a default value of 2 so the caller can omit it
    public static double CalculateArea(double radius, int decimals = 2)
    {
        if (radius <= 0) throw new ArgumentException("Radius must be positive.");
        return Math.Round(Math.PI * radius * radius, decimals);
    }

    // Overload 2: Rectangle - two double parameters
    public static double CalculateArea(double length, double width)
    {
        if (length <= 0 || width <= 0)
            throw new ArgumentException("Dimensions must be positive.");
        return length * width;
    }

    // Overload 3: Triangle - bool flag disambiguates from the rectangle overload
    public static double CalculateArea(double baseLength, double height, bool isTriangle)
    {
        if (!isTriangle) throw new ArgumentException("Pass isTriangle: true to use this overload.");
        if (baseLength <= 0 || height <= 0)
            throw new ArgumentException("Dimensions must be positive.");
        return 0.5 * baseLength * height;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== M4: Geometry Library - Area Calculator ===\n");

        // Call 1: circle with default 2 decimal places
        double c1 = GeometryLibrary.CalculateArea(5);
        Console.WriteLine($"Call 1 - CalculateArea(5)                     -> Circle    area = {c1}  (default 2 dp)");

        // Call 2: rectangle - two doubles route to the rectangle overload
        double r1 = GeometryLibrary.CalculateArea(4, 6);
        Console.WriteLine($"Call 2 - CalculateArea(4, 6)                  -> Rectangle area = {r1}");

        // Call 3: triangle - bool flag selects triangle overload
        double t1 = GeometryLibrary.CalculateArea(3, 7, isTriangle: true);
        Console.WriteLine($"Call 3 - CalculateArea(3, 7, isTriangle:true) -> Triangle  area = {t1}");

        // Call 4: circle with named argument overriding the default decimal precision
        double c2 = GeometryLibrary.CalculateArea(radius: 5, decimals: 4);
        Console.WriteLine($"Call 4 - CalculateArea(radius:5, decimals:4)  -> Circle    area = {c2}  (4 dp)");

        Console.WriteLine();
        Console.WriteLine("--- Additional calls ---");
        Console.WriteLine($"Circle  r=10 default dp : {GeometryLibrary.CalculateArea(10)}");
        Console.WriteLine($"Circle  r=10 6 dp       : {GeometryLibrary.CalculateArea(radius: 10, decimals: 6)}");
        Console.WriteLine($"Rectangle 12x5          : {GeometryLibrary.CalculateArea(12, 5)}");
        Console.WriteLine($"Triangle  base=8 h=6    : {GeometryLibrary.CalculateArea(8, 6, isTriangle: true)}");

        Console.WriteLine();
        Console.WriteLine("Default : CalculateArea(5)                - uses decimals=2 implicitly");
        Console.WriteLine("Named   : CalculateArea(radius:5, decimals:4) - overrides default, intent is clear");
    }
}
