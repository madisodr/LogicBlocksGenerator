namespace tests;

using System.Collections.Generic;

public sealed class LoggerFake : LogicBlocksGenerator.ILogger
{
    public readonly List<string> Infos = new();
    public readonly List<string> Warnings = new();
    public readonly List<string> Errors = new();

    public void Info(string message) => Infos.Add(message);
    public void Warning(string message) => Warnings.Add(message);
    public void Error(string message) => Errors.Add(message);
}