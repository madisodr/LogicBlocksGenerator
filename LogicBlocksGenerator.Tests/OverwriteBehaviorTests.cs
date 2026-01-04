namespace LogicBlocksGenerator.Tests;

using System;
using System.IO;

using Xunit;

public sealed class OverwriteBehaviorTests : IDisposable
{
    private readonly string _templateDir;
    private readonly string _outputDir;

    public OverwriteBehaviorTests()
    {
        _templateDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        _outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        Directory.CreateDirectory(_templateDir);
        Directory.CreateDirectory(_outputDir);
    }

    [Fact]
    public void Execute_DoesNotOverwriteExistingFiles_ByDefault()
    {
        // Arrange
        var statesDir = Path.Combine(_templateDir, "states");
        Directory.CreateDirectory(statesDir);

        var templateFile = Path.Combine(statesDir, "Logic.cs.tpl");
        File.WriteAllText(
          templateFile,
          "class ${NAME} {}"
        );

        var outputStatesDir = Path.Combine(_outputDir, "states");
        Directory.CreateDirectory(outputStatesDir);

        var outputFile = Path.Combine(outputStatesDir, "PlayerLogic.cs");
        File.WriteAllText(outputFile, "ORIGINAL");

        var logger = new LoggerFake();
        var generator = new LogicBlocksGenerator(
          allowOverwrite: false,
          logger: logger
        );

        // Act
        generator.Execute(
          "Test.Namespace",
          "Player",
          _templateDir,
          _outputDir
        );

        // Assert
        var contents = File.ReadAllText(outputFile);
        Assert.Equal("ORIGINAL", contents);

        Assert.Single(logger.Warnings);
        Assert.Contains("Skipped existing file", logger.Warnings[0]);
    }

    [Fact]
    public void Execute_OverwritesExistingFiles_WhenOverwriteIsEnabled()
    {
        // Arrange
        var statesDir = Path.Combine(_templateDir, "states");
        Directory.CreateDirectory(statesDir);

        var templateFile = Path.Combine(statesDir, "Logic.cs.tpl");
        File.WriteAllText(
          templateFile,
          "class ${NAME} {}"
        );

        var outputStatesDir = Path.Combine(_outputDir, "states");
        Directory.CreateDirectory(outputStatesDir);

        var outputFile = Path.Combine(outputStatesDir, "PlayerLogic.cs");
        File.WriteAllText(outputFile, "ORIGINAL");

        var logger = new LoggerFake();
        var generator = new LogicBlocksGenerator(allowOverwrite: true, logger: logger);

        // Act
        generator.Execute(
          "Test.Namespace",
          "Player",
          _templateDir,
          _outputDir
        );

        // Assert
        var contents = File.ReadAllText(outputFile);
        Assert.Contains("Player", contents);

        Assert.DoesNotContain("Skipped existing file", logger.Warnings);
    }

    public void Dispose()
    {
        if (Directory.Exists(_templateDir))
        {
            Directory.Delete(_templateDir, recursive: true);
        }

        if (Directory.Exists(_outputDir))
        {
            Directory.Delete(_outputDir, recursive: true);
        }
    }
}
