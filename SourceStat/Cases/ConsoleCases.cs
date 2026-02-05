using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Cases
{
    public class ConsoleCases
    {
        public static ICommand[] UseConsoleCases()
        {
            ICommand[] commands =
            {
                new VersionCommand(),
                new HelpCommand(),
                new DeveloperCommand(),

            };
            return commands;
        }
    }

    public class InfoCommand : ICommand
    {
        public string Name => "?";

        public string Description => throw new NotImplementedException();

        public Task Execute(string[] args, DataCore data)
        {
            throw new NotImplementedException();
        }
    }

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
                "Версия 1.0.0, developer sergxlove, 2025\n" +
                "Все права защищены\n");
        }
    }

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

    public class DeveloperCommand : ICommand
    {
        public string Name => "developer";

        public string Description => "\n" +
            "Структура: developer \n" +
            "Отвечает за вывод информации о разработчике\n";

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            Console.WriteLine("\n" +
                "╔══╗╔═══╗╔═══╗╔═══╗╔══╗╔══╗╔╗──╔══╗╔╗╔╗╔═══╗\n" +
                "║╔═╝║╔══╝║╔═╗║║╔══╝╚═╗║║╔═╝║║──║╔╗║║║║║║╔══╝\n" +
                "║╚═╗║╚══╗║╚═╝║║║╔═╗──║╚╝║──║║──║║║║║║║║║╚══╗\n" +
                "╚═╗║║╔══╝║╔╗╔╝║║╚╗║──║╔╗║──║║──║║║║║╚╝║║╔══╝\n" +
                "╔═╝║║╚══╗║║║║─║╚═╝║╔═╝║║╚═╗║╚═╗║╚╝║╚╗╔╝║╚══╗\n" +
                "╚══╝╚═══╝╚╝╚╝─╚═══╝╚══╝╚══╝╚══╝╚══╝─╚╝─╚═══╝\n");
        }
    }

    public class DirectoryCommand : ICommand
    {
        public string Name => "dir";

        public string Description => throw new NotImplementedException();

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                Console.WriteLine("\nИгнорируемые директории: ");
                foreach(string dir in data.Options.IgnoreDirectories)
                {
                    Console.WriteLine(dir);
                }
            }
            Dictionary<string, string> argsPairs = ToolsForCommand.ConvertArgsToDictionary(args);
            foreach (var item in argsPairs)
            {
                switch (item.Key)
                {

                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
    }
}
