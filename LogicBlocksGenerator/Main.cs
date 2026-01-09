namespace LogicBlocksGenerator;

using System.Threading.Tasks;
using CliFx;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await new CliApplicationBuilder()
            .AddCommand<GenerateCommand>()
            .Build()
            .RunAsync(args);
    }
}
