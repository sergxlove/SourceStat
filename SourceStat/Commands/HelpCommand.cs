using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name => "help";

        public string Description => throw new NotImplementedException();

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            Console.WriteLine("\n" +
                "version - Вывод текущей версии приложения\n" +
                "help - Вывод помощи\n");
        }
    }
}
