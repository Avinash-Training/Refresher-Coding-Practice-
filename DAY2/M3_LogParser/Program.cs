using System;

// M3 - Log File Parser
// Demonstrates: in parameters (read-only ref), out parameters, ref parameters

// Severity levels supported by the log parser
enum LogLevel { Trace, Debug, Info, Warning, Error, Fatal, Unknown }

static class LogParser
{
    // in: passes logLine as read-only reference - avoids copying for large value-type structs
    // out: returns parsed timestamp and log level without needing a return type wrapper
    // ref: linesProcessed is shared across calls and incremented each time parsing succeeds
    public static bool ParseLogLine(
        in string logLine,
        out DateTime timestamp,
        out LogLevel level,
        ref int linesProcessed)
    {
        timestamp = DateTime.MinValue;
        level     = LogLevel.Unknown;

        if (string.IsNullOrWhiteSpace(logLine))
            return false;

        // Expected format: "2023-10-27 14:30:00 ERROR: Disk full"
        string[] parts = logLine.Split(' ', 3);

        if (parts.Length < 3)
            return false;

        // Combine date and time tokens then parse as DateTime
        string dateTimePart = $"{parts[0]} {parts[1]}";
        if (!DateTime.TryParse(dateTimePart, out timestamp))
            return false;

        // Extract the severity keyword before the colon
        string rest = parts[2];
        int colonIdx = rest.IndexOf(':');
        string levelStr = colonIdx >= 0 ? rest[..colonIdx].Trim() : rest.Trim();

        // TryParse the enum - falls back to Unknown if unrecognised
        if (!Enum.TryParse<LogLevel>(levelStr, ignoreCase: true, out level))
            level = LogLevel.Unknown;

        linesProcessed++;
        return true;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== M3: Log File Parser ===\n");

        int linesProcessed = 0;

        string[] logLines =
        {
            "2023-10-27 14:30:00 ERROR: Disk full",
            "2023-10-27 14:31:05 WARNING: Memory usage high",
            "2023-10-27 14:32:10 INFO: Service started",
            "2023-10-27 14:33:00 FATAL: Unhandled exception",
            "INVALID LINE WITHOUT PROPER FORMAT",
        };

        foreach (string line in logLines)
        {
            bool ok = LogParser.ParseLogLine(
                in line,
                out DateTime timestamp,
                out LogLevel level,
                ref linesProcessed);

            if (ok)
            {
                Console.WriteLine($"  Line      : \"{line}\"");
                Console.WriteLine($"  Timestamp : {timestamp:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"  LogLevel  : {level}");
            }
            else
            {
                Console.WriteLine($"  Failed    : \"{line}\" - could not parse");
            }
            Console.WriteLine();
        }

        Console.WriteLine($"Total lines processed (ref counter): {linesProcessed}");

        Console.WriteLine();
        Console.WriteLine("'in' passes by read-only reference - the caller's value cannot be changed.");
        Console.WriteLine("For large value-type structs this avoids copying the full struct to the stack.");
        Console.WriteLine("For strings it acts as a self-documenting contract that the method won't mutate the input.");
    }
}
