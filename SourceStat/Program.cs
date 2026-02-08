using SourceStat.Cases;
using SourceStat.Models;

namespace SourceStat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExecuteCommandCore cmd = new();
            DataCore data = new DataCore();
            cmd.AddRange(ConsoleCases.UseConsoleCases());
            string commandLine = string.Empty;
            bool exit = false;
            while (!exit)
            {
                Console.Write($"USER > ");
                commandLine = Console.ReadLine()!;
                commandLine = commandLine.Trim();
                if (commandLine == "exit")
                {
                    exit = true;
                    continue;
                }
                cmd.ExecuteCommand(commandLine, data);
            }
        }
    }
}
