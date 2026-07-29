using System;
using System.Collections.Generic;

// H1 - Configuration Loader
// Uses: interface, out parameter, params, TryLoad pattern (like TryParse but for config)

// Simple key-value config model
class AppConfiguration
{
    public string SourceName { get; set; } = "";
    public Dictionary<string, string> Settings { get; } = new();

    public void Print()
    {
        Console.WriteLine($"  Source: {SourceName}");
        foreach (var kv in Settings)
            Console.WriteLine($"  {kv.Key} = {kv.Value}");
    }
}

// Every config source must follow this contract
interface IConfigurationSource
{
    string SourceName { get; }
    // Returns true if load succeeded, puts data in 'config' via out
    bool TryLoad(out AppConfiguration config);
}

// Source 1: reads from environment variables - simulated as not available
class EnvSource : IConfigurationSource
{
    public string SourceName => "EnvironmentVariables";

    public bool TryLoad(out AppConfiguration config)
    {
        config = null!;
        Console.WriteLine($"  Trying {SourceName}...");

        string val = Environment.GetEnvironmentVariable("APP_DB") ?? "";
        if (string.IsNullOrEmpty(val))
        {
            Console.WriteLine($"  {SourceName}: no data found. Skipping.\n");
            return false;
        }

        config = new AppConfiguration { SourceName = SourceName };
        config.Settings["db"] = val;
        return true;
    }
}

// Source 2: reads from a JSON file - simulated as missing
class FileSource : IConfigurationSource
{
    private readonly string _path;
    public string SourceName => "JsonFile";

    public FileSource(string path) => _path = path;

    public bool TryLoad(out AppConfiguration config)
    {
        config = null!;
        Console.WriteLine($"  Trying {SourceName} ('{_path}')...");

        if (!System.IO.File.Exists(_path))
        {
            Console.WriteLine($"  {SourceName}: file not found. Skipping.\n");
            return false;
        }

        config = new AppConfiguration { SourceName = SourceName };
        config.Settings["file"] = _path;
        return true;
    }
}

// Source 3: hardcoded defaults - always succeeds, acts as final fallback
class DefaultSource : IConfigurationSource
{
    public string SourceName => "Defaults";

    public bool TryLoad(out AppConfiguration config)
    {
        Console.WriteLine($"  Trying {SourceName}...");

        config = new AppConfiguration { SourceName = SourceName };
        config.Settings["db_host"]  = "localhost";
        config.Settings["db_port"]  = "5432";
        config.Settings["timeout"]  = "30";

        Console.WriteLine($"  {SourceName}: loaded successfully.\n");
        return true;
    }
}

// Loader tries each source one by one and stops at the first success
static class ConfigurationLoader
{
    // params lets the caller pass any number of sources without building an array first
    public static bool Load(out AppConfiguration config, params IConfigurationSource[] sources)
    {
        config = null!;

        foreach (IConfigurationSource source in sources)
        {
            if (source.TryLoad(out config))
                return true;
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== H1: Configuration Loader ===\n");

        bool ok = ConfigurationLoader.Load(
            out AppConfiguration config,
            new EnvSource(),
            new FileSource("app.json"),
            new DefaultSource()
        );

        if (ok)
        {
            Console.WriteLine("Configuration loaded:");
            config.Print();
        }
        else
        {
            Console.WriteLine("Failed to load any configuration.");
        }
    }
}
