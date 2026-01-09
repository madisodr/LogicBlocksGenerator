namespace LogicBlocksGenerator.Tests;

using System.Collections.Generic;

using CliFx.Infrastructure;

public sealed class LoggerFake : ILogger
{

    private readonly IConsole _console;

    public LoggerFake(IConsole console)
    {
        _console = console;
    }

    public readonly List<string> Infos = new();
    public readonly List<string> Warnings = new();
    public readonly List<string> Errors = new();

    public void Info(string message) => Infos.Add(message);
    public void Warning(string message) => Warnings.Add(message);
    public void Error(string message) => Errors.Add(message);
}