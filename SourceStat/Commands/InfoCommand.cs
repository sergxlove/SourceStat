using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class InfoCommand : ICommand
    {
        public string Name => "?";

        public string Description => "\n" +
            "Структура: ? [Аргумент] \n" +
            "Отвечает за вывод подробной информации о команде\n" +
            "В качестве аргумента используется нужная команда\n";

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                Console.WriteLine(Description);
                return;
            }
            ICommand cmd;
            switch (args[0])
            {
                case "developer":
                    cmd = new DeveloperCommand();
                    break;
                case "dir":
                    cmd = new DirectoryCommand();
                    break;
                case "?":
                    cmd = new InfoCommand();
                    break;
                case "lng":
                    cmd = new LanguageCommand();
                    break;
                case "go":
                    cmd = new StartCommand();
                    break;
                case "version":
                    cmd = new VersionCommand();
                    break;
                case "help":
                    cmd = new HelpCommand();
                    break;
                default:
                    Console.WriteLine($"Неизвестный аргумент: {args[0]}");
                    return;
            }
            Console.WriteLine(cmd.Description);
        }
    }
}
