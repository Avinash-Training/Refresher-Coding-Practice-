using System;

// H3 - Log Message Formatter
// Uses: static method, params, local function, TryParse for argument validation

static class Logger
{
    // Formats a message template by replacing {0}, {1}, {2} with the given arguments
    // params lets callers pass any number of values without building an array
    public static string FormatLogMessage(string template, params object[] args)
    {
        if (string.IsNullOrEmpty(template)) return "";
        if (args == null || args.Length == 0) return template;

        // Local function: does the actual substitution work
        // Keeping it here means it cannot be called from anywhere else
        string Replace(string t, object[] values)
        {
            string result = t;
            for (int i = 0; i < values.Length; i++)
            {
                string placeholder = "{" + i + "}";
                string replacement = FormatValue(values[i]);
                result = result.Replace(placeholder, replacement);
            }
            return result;
        }

        return Replace(template, args);
    }

    // Converts a single value to a display string
    // TryParse checks if a string argument is actually a number and formats it cleanly
    private static string FormatValue(object value)
    {
        if (value == null) return "(null)";

        if (value is DateTime dt)
            return dt.ToString("yyyy-MM-dd HH:mm:ss");

        // If the value is a string that looks like a number, parse and return it as-is
        if (value is string s)
        {
            if (int.TryParse(s, out int num))
                return num.ToString();
            if (double.TryParse(s, out double dbl))
                return dbl.ToString();
            return s;
        }

        return value.ToString() ?? "";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== H3: Log Message Formatter ===\n");

        // Basic string and DateTime arguments
        string msg1 = Logger.FormatLogMessage(
            "User {0} logged in from {1} at {2}",
            "JohnDoe", "192.168.1.1", DateTime.Now);
        Console.WriteLine(msg1);

        // Integer arguments
        string msg2 = Logger.FormatLogMessage(
            "Order #{0} placed by {1} for {2} items",
            42, "Alice", 3);
        Console.WriteLine(msg2);

        // Numeric strings handled via TryParse inside FormatValue
        string msg3 = Logger.FormatLogMessage(
            "Retry attempt {0} of {1}",
            "3", "5");
        Console.WriteLine(msg3);

        // No arguments - template returned as-is
        string msg4 = Logger.FormatLogMessage("Server started.");
        Console.WriteLine(msg4);
    }
}
