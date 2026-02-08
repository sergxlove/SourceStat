using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class DirectoryCommand : ICommand
    {
        public string Name => "dir";

        public string Description => "\n" +
            "Структура: dir [Аргумент] \n" +
            "Отвечает за управление игнорируемыми директориями\n" +
            "Аргументы: \n" +
            "[Без аргумента]: вывод игнорируемых директорий\n" +
            "--add(-a) [Параметр]: добавление директории в список игнорируемых\n" +
            "--delete(-d) [Параметр]: удаление директории из списка игнорируемых\n" +
            "--defailt(-def) [Параметр]: добавление стандартных директорий в список игнорируемых\n" +
            "--remove-default(-rmdef) [Параметр]: удаление стандартных директорий из списка игнорируемых\n";

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                if(!data.Options.IgnoreDirectories.Any())
                {
                    Console.WriteLine("Нет игнорируемых директорий");
                }
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
                        if(data.Options.IsAddDefault)
                        {
                            Console.WriteLine("Ошибка. Игнорируемые директории по умолчанию уже были добавлены");
                        }
                        else
                        {
                            data.Options.SetDefaultIgnores();
                            Console.WriteLine("Игнорируемые директории по умолчанию успешно добавлены");
                        }
                        break;
                    case "-rmdef":
                    case "--remove-default":
                        if (data.Options.IsAddDefault)
                        {
                            data.Options.RemoveDefaultIgnores();
                            Console.WriteLine("Игнорируемые директории по умолчанию успешно удалены");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка. Игнорируемые директории по умолчанию уже были удалены");
                        }
                        break;
                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
    }
}
