namespace LogicBlocksGenerator;

using System;

public static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            var options = ParseArguments(args);

            var logger = new ConsoleLogger();

            var generator = new LogicBlocksGenerator(
              allowOverwrite: options.AllowOverwrite,
              logger: logger
            );

            generator.Execute(
              options.Namespace,
              options.Name,
              options.TemplateDir,
              options.OutputDir
            );

            return 0;
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"[ERROR] {ex.Message}");
            PrintUsage();
            return 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("[FATAL] Unhandled exception:");
            Console.Error.WriteLine(ex);
            return 2;
        }
    }

    private static Options ParseArguments(string[] args)
    {
        if (args.Length == 0)
        {
            throw new ArgumentException("No arguments provided.");
        }

        var options = new Options();

        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            switch (arg)
            {
                case "--namespace":
                    options.Namespace = RequireValue(args, ref i, arg);
                    break;

                case "--name":
                    options.Name = RequireValue(args, ref i, arg);
                    break;

                case "--template":
                    options.TemplateDir = RequireValue(args, ref i, arg);
                    break;

                case "--output":
                    options.OutputDir = RequireValue(args, ref i, arg);
                    break;

                case "--overwrite":
                    options.AllowOverwrite = true;
                    break;

                case "--help":
                case "-h":
                    PrintUsage();
                    Environment.Exit(0);
                    break;

                default:
                    throw new ArgumentException($"Unknown argument: {arg}");
            }
        }

        ValidateOptions(options);
        return options;
    }

    private static string RequireValue(string[] args, ref int index, string flag)
    {
        if (index + 1 >= args.Length)
        {
            throw new ArgumentException($"Missing value for {flag}");
        }

        index++;
        return args[index];
    }

    private static void ValidateOptions(Options options)
    {
        if (string.IsNullOrWhiteSpace(options.Namespace))
        {
            throw new ArgumentException("--namespace is required");
        }

        if (string.IsNullOrWhiteSpace(options.Name))
        {
            throw new ArgumentException("--name is required");
        }

        if (string.IsNullOrWhiteSpace(options.TemplateDir))
        {
            throw new ArgumentException("--template is required");
        }

        if (string.IsNullOrWhiteSpace(options.OutputDir))
        {
            throw new ArgumentException("--output is required");
        }
    }

    private static void PrintUsage()
    {
        Console.WriteLine("""
        Usage:
        dotnet run -- \
            --namespace <namespace> \
            --name <Name> \
            --template <templateDir> \
            --output <outputDir> \
            [--overwrite]

        Options:
        --namespace   Target C# namespace to inject
        --name        Root name used for template substitution
        --template    Directory containing *.cs.tpl templates
        --output      Output directory for generated files
        --overwrite   Allow overwriting existing files
        --help, -h    Show this help
        """);
    }

    private sealed class Options
    {
        public string Namespace { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string TemplateDir { get; set; } = string.Empty;
        public string OutputDir { get; set; } = string.Empty;
        public bool AllowOverwrite { get; set; }
    }
}
