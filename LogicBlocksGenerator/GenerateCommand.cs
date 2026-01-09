namespace LogicBlocksGenerator;

using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

[Command(name: "generate", Description = "Generates LogicBlocks files from templates.")]
public sealed class GenerateCommand : ICommand
{
    [CommandOption(
        "namespace",
        Description = "Target C# namespace to inject.",
        IsRequired = true
    )]
    public string Namespace { get; init; } = string.Empty;

    [CommandOption(
        "name",
        Description = "Root name used for template substitution.",
        IsRequired = true
    )]
    public string Name { get; init; } = string.Empty;

    [CommandOption(
        "template",
        Description = "Directory containing *.cs.tpl templates.",
        IsRequired = false
    )]
    public string TemplateDir { get; init; } = "./templates";

    [CommandOption(
        "output",
        Description = "Output directory for generated files.",
        IsRequired = true
    )]
    public string OutputDir { get; init; } = string.Empty;

    [CommandOption(
        "overwrite",
        Description = "Allow overwriting existing files."
    )]
    public bool AllowOverwrite { get; init; }

    public ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            var logger = new ConsoleLogger(console);

            var generator = new LogicBlocksGenerator(
                allowOverwrite: AllowOverwrite,
                logger: logger
            );

            generator.Execute(
                Namespace,
                Name,
                TemplateDir,
                OutputDir
            );

            return ValueTask.CompletedTask;
        }
        catch (ArgumentException ex)
        {
            console.Error.WriteLine($"[ERROR] {ex.Message}");
            return new ValueTask(Task.FromResult(1));
        }
        catch (Exception ex)
        {
            console.Error.WriteLine("[FATAL] Unhandled exception:");
            console.Error.WriteLine(ex.ToString());
            return new ValueTask(Task.FromResult(2));
        }
    }
}
