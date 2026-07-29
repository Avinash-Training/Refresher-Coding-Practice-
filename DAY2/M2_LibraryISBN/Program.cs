using System;
using System.Collections.Generic;

// M2 - Library ISBN Order Processor
// Demonstrates params, out parameters, and TryParse-style validation pattern
static class LibraryOrderProcessor
{
    // TryParse pattern: strips hyphens/spaces and checks if result is exactly 13 digits
    public static bool TryParseISBN(string rawIsbn, out string cleanIsbn)
    {
        cleanIsbn = string.Empty;

        if (string.IsNullOrWhiteSpace(rawIsbn))
            return false;

        // Remove hyphens and spaces to get the raw digit string
        string stripped = rawIsbn.Replace("-", "").Replace(" ", "").Trim();

        if (stripped.Length != 13)
            return false;

        foreach (char c in stripped)
        {
            if (!char.IsDigit(c))
                return false;
        }

        cleanIsbn = stripped;
        return true;
    }

    // Accepts variable number of raw ISBN strings via params, returns valid ones via out
    public static bool TryProcessOrder(out List<string> validISBNs, params string[] rawISBNs)
    {
        validISBNs = new List<string>();

        if (rawISBNs == null || rawISBNs.Length == 0)
            return false;

        foreach (string raw in rawISBNs)
        {
            if (TryParseISBN(raw.Trim(), out string clean))
            {
                validISBNs.Add(clean);
                Console.WriteLine($"  Valid   : '{raw.Trim()}' -> '{clean}'");
            }
            else
            {
                Console.WriteLine($"  Invalid : '{raw.Trim()}' - skipped");
            }
        }

        return validISBNs.Count > 0;
    }

    // Overload: splits a comma-separated string and delegates to the params overload
    public static bool TryProcessOrder(string commaSeparatedISBNs, out List<string> validISBNs)
    {
        string[] parts = commaSeparatedISBNs.Split(',');
        return TryProcessOrder(out validISBNs, parts);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== M2: Library ISBN Order Processor ===\n");

        string input = "978-3-16-148410-0, 1234567890123, invalid-isbn, 978-1-4028-9462-6";
        Console.WriteLine($"Input: \"{input}\"\n");

        bool success = LibraryOrderProcessor.TryProcessOrder(input, out List<string> validISBNs);

        Console.WriteLine();
        Console.WriteLine($"Result     : {success}");
        Console.WriteLine($"Valid ISBNs ({validISBNs.Count}):");
        foreach (string isbn in validISBNs)
            Console.WriteLine($"  - {isbn}");

        Console.WriteLine();

        // Direct use of params overload with individual string arguments
        Console.WriteLine("--- Using params overload directly ---");
        bool r2 = LibraryOrderProcessor.TryProcessOrder(
            out List<string> v2,
            "978-3-16-148410-0",
            "BADISBN",
            "9781402894626");
        Console.WriteLine($"Result: {r2}, Valid: [{string.Join(", ", v2)}]");

        Console.WriteLine();
        Console.WriteLine("TryParseISBN returns false on bad input instead of throwing an exception.");
        Console.WriteLine("The caller decides whether to skip, log, or retry - no crash occurs.");
    }
}
