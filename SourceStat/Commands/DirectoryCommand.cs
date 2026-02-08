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
                    Console.WriteLine("\nНет игнорируемых директорий\n");
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
                            Console.WriteLine("\nАргумент не может быть пустым.\n" +
                                "Для получения дополнительной информации воспользуйтесь командой: ? dir\n");
                            break;
                        }
                        if (data.Options.IgnoreDirectories.Contains(item.Value))
                        {
                            Console.WriteLine("\nИгнорируемая директория уже добавлена\n");
                            break;
                        }
                        data.Options.AddIgnoreDirectories(item.Value);
                        Console.WriteLine("\nИгнорируемая директория успешно добавлена\n");
                        break;
                    case "-d":
                    case "--delete":
                        if (item.Value == string.Empty)
                        {
                            Console.WriteLine("\nАргумент не может быть пустым.\n" +
                                "Для получения дополнительной информации воспользуйтесь командой: ? dir\n");
                            break;
                        }
                        if (data.Options.IgnoreDirectories.Contains(item.Value))
                        {
                            Console.WriteLine("\nДиректория не найдена в списке игнорируемых\n");
                            break;
                        }
                        data.Options.RemoveIgnoreDirectories(item.Value);
                        Console.WriteLine("\nДиректория успешно удалена из игнорируемых\n");
                        break;
                    case "-def":
                    case "--default":
                        if(data.Options.IsAddDefault)
                        {
                            Console.WriteLine("\nОшибка. Игнорируемые директории по умолчанию уже были добавлены\n");
                        }
                        else
                        {
                            data.Options.SetDefaultIgnores();
                            Console.WriteLine("\nИгнорируемые директории по умолчанию успешно добавлены\n");
                        }
                        break;
                    case "-rmdef":
                    case "--remove-default":
                        if (data.Options.IsAddDefault)
                        {
                            data.Options.RemoveDefaultIgnores();
                            Console.WriteLine("\nИгнорируемые директории по умолчанию успешно удалены\n");
                        }
                        else
                        {
                            Console.WriteLine("\nОшибка. Игнорируемые директории по умолчанию уже были удалены\n");
                        }
                        break;
                    default:
                        Console.WriteLine("\nНеверный ввод\n" +
                            "Для получения дополнительной информации воспользуйтесь командой: ? dir\n");
                        break;
                }
            }
        }
    }
}
