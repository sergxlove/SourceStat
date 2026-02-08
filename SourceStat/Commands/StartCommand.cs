using SourceStat.Core.Models;
using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class StartCommand : ICommand
    {
        public string Name => "go";

        public string Description => "\n" +
            "Структура: go [Аргумент] \n" +
            "Отвечает за вывод текущей версии приложения\n";

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if(data.Options.SelectLanguages.Count == 0)
            {
                Console.WriteLine("\nНет выбранных языков\n");
            }
            long countFile = 0;
            long countLine = 0;
            if (args.Length == 0)
            {
                foreach (AvailableLanguage lang in data.Options.SelectLanguages)
                {
                    countFile = FileChecker.GetCountFiles(Directory.GetCurrentDirectory().ToString(),
                        data.Options);
                    countLine = FileChecker.GetCountLineInFiles(Directory.GetCurrentDirectory().ToString(),
                        data.Options);
                    Console.WriteLine($"{Enum.GetName(lang)}: Найдено {countFile} файлов, в них {countLine} строк.");
                }
            }
            else
            {
                Dictionary<string, string> argsPairs = ToolsForCommand.ConvertArgsToDictionary(args);
                foreach (var item in argsPairs)
                {
                    switch (item.Key)
                    {
                        case "-d":
                        case "-directory":
                            if(string.IsNullOrEmpty(item.Value))
                            {
                                Console.WriteLine("\nПараметр не может быть пустым.\n" +
                                    "Для получения дополнительной информации воспользуйтесь командой: ? go\n");
                                break;
                            }
                            if(!Directory.Exists(item.Value))
                            {
                                Console.WriteLine("\nОшибка при вводе директории\n");
                            }
                            foreach (AvailableLanguage lang in data.Options.SelectLanguages)
                            {
                                countFile = FileChecker.GetCountFiles(item.Value, data.Options);
                                countLine = FileChecker.GetCountLineInFiles(item.Value, data.Options);
                                Console.WriteLine($"{Enum.GetName(lang)}: Найдено {countFile} файлов, в них {countLine} строк.");
                            }
                            break;
                        default:
                            Console.WriteLine("\nНеверный ввод.\n" +
                            "Для получения дополнительной информации воспользуйтесь командой: ? go\n");
                            break;
                    }
                }
            }
        }
    }
}
