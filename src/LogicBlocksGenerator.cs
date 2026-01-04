namespace LogicBlocksGenerator;

using System;
using System.IO;
using System.Text;

public sealed class LogicBlocksGenerator
{
    private readonly bool _allowOverwrite;
    private readonly ILogger _logger;

    private const string TemplateFileExtension = ".tpl";

    public LogicBlocksGenerator(bool allowOverwrite = false, ILogger? logger = null)
    {
        _allowOverwrite = allowOverwrite;
        _logger = logger ?? new ConsoleLogger();
    }

    public void Execute(string @namespace, string name, string templateDir, string outputDir)
    {
        if (string.IsNullOrWhiteSpace(@namespace))
        {
            @namespace = "";
            _logger.Warning("Namespace is null or empty. Using empty namespace.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        if (!Directory.Exists(templateDir))
        {
            throw new DirectoryNotFoundException($"Template directory not found: {templateDir}");
        }

        Directory.CreateDirectory(outputDir);

        _logger.Info($"Generating LogicBlocks '{name}'");
        _logger.Info($"Template Dir: {templateDir}");
        _logger.Info($"Output Dir: {outputDir}");

        var templateFiles = Directory.GetFiles(templateDir, "*.cs" + TemplateFileExtension, SearchOption.AllDirectories);

        foreach (var templateFile in templateFiles)
        {
            ProcessTemplateFile(templateFile, templateDir, outputDir, @namespace, name);
        }

        _logger.Info("Generation complete.");
    }

    private void ProcessTemplateFile(string templateFile, string templateRoot, string outputRoot, string @namespace, string name)
    {
        var relativePath = Path.GetRelativePath(templateRoot, templateFile);

        // Remove `.tpl`
        var withoutTpl = relativePath[..^".tpl".Length];

        // Split directory + filename
        var directory = Path.GetDirectoryName(withoutTpl);
        var fileName = Path.GetFileName(withoutTpl);

        // Prefix name to filename
        var prefixedFileName = $"{name}{fileName}";

        var outputRelativePath = string.IsNullOrEmpty(directory) ? prefixedFileName : Path.Combine(directory, prefixedFileName);
        var outputFilePath = Path.Combine(outputRoot, outputRelativePath);

        if (File.Exists(outputFilePath) && !_allowOverwrite)
        {
            _logger.Warning($"Skipped existing file: {outputFilePath}");
            return;
        }

        var outputDirectory = Path.GetDirectoryName(outputFilePath);
        if (!string.IsNullOrEmpty(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        var contents = File.ReadAllText(templateFile, Encoding.UTF8)
          .Replace("${NAMESPACE}", @namespace)
          .Replace("${NAME}", name)
          .Replace("${NAME_LOWER}", name.ToLowerInvariant());

        File.WriteAllText(outputFilePath, contents, Encoding.UTF8);

        _logger.Info($"Generated: {outputFilePath}");
    }
}
