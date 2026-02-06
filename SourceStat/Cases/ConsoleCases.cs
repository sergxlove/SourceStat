using SourceStat.Core.Models;
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
                foreach (string dir in data.Options.IgnoreDirectories)
                {
                    Console.WriteLine(dir);
                }
                return;
            }
            Dictionary<string, string> argsPairs = ToolsForCommand.ConvertArgsToDictionary(args);
            foreach (var item in argsPairs)
            {
                switch (item.Key)
                {
                    case "-a":
                    case "--add":
                        if (item.Value == string.Empty)
                        {
                            Console.WriteLine("Аргумент не может быть пустым");
                            break;
                        }
                        if (data.Options.IgnoreDirectories.Contains(item.Value))
                        {
                            Console.WriteLine("Игнорируемая директория уже добавлена");
                            break;
                        }
                        data.Options.AddIgnoreDirectories(item.Value);
                        Console.WriteLine("Игнорируемая директория успешно добавлена ");
                        break;
                    case "-d":
                    case "--delete":
                        if (item.Value == string.Empty)
                        {
                            Console.WriteLine("Аргумент не может быть пустым");
                            break;
                        }
                        if (data.Options.IgnoreDirectories.Contains(item.Value))
                        {
                            Console.WriteLine("Директория не найдена в списке игнорируемых");
                            break;
                        }
                        data.Options.RemoveIgnoreDirectories(item.Value);
                        Console.WriteLine("Директория успешно удалена из игнорируемых");
                        break;
                    case "-def":
                    case "--default":
                        data.Options.SetDefaultIgnores();
                        Console.WriteLine("Игнорируемые директории по умолчанию успешно добавлены");
                        break;
                    case "-rmdef":
                    case "--remove-default":
                        data.Options.RemoveDefaultIgnores();
                        Console.WriteLine("Игнорируемые директории по умолчанию успешно удалены");
                        break;
                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
    }

    public class LanguageCommand : ICommand
    {
        public string Name => "lng";

        public string Description => throw new NotImplementedException();

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                foreach (AviableLanguage lng in data.Options.SelectLanguages)
                {
                    Console.WriteLine(Enum.GetName(typeof(AviableLanguage), lng));
                }
                return;
            }
            Dictionary<string, string> argsPairs = ToolsForCommand.ConvertArgsToDictionary(args);
            foreach (var item in argsPairs)
            {
                switch (item.Key)
                {
                    case "-a":
                    case "--add":
                        break;
                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
    }
}
