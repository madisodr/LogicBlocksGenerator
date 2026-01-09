namespace LogicBlocksGenerator;

using CliFx.Infrastructure;

public sealed class ConsoleLogger : ILogger
{
    private readonly IConsole _console;

    public ConsoleLogger(IConsole console)
    {
        _console = console;
    }

    public void Info(string message) => _console.Output.WriteLine($"[INFO] {message}");
    public void Warning(string message) => _console.Output.WriteLine($"[WARN] {message}");
    public void Error(string message) => _console.Error.WriteLine($"[ERROR] {message}");
}
