using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class VersionCommand : ICommand
    {
        public string Name => "version";

        public string Description => "\n" +
            "Структура: version \n" +
            "Отвечает за вывод текущей версии приложения\n";

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            Console.WriteLine("\n" +
                "Версия 1.0.0, developer sergxlove, 2026\n" +
                "Все права защищены\n");
        }
    }
}
