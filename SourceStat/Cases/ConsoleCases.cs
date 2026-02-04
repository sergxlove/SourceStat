using SourceStat.Interfaces;

namespace SourceStat.Cases
{
    public class ConsoleCases
    {
        public static ICommand[] UseConsoleCases()
        {
            ICommand[] commands =
            {
                new VersionCommand(),
            };
            return commands;
        }
    }

    public class VersionCommand : ICommand
    {
        public string Name => "version";

        public string Description => "\n" +
            "Структура: version \n" +
            "Отвечает за вывод текущей версии приложения\n";

        public async Task Execute(string[] args)
        {
            await Task.CompletedTask;
            Console.WriteLine("\n" +
                "Версия 1.0.0, developer sergxlove, 2025\n" +
                "Все права защищены\n");
        }
    }

}
