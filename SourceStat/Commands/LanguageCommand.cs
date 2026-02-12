using SourceStat.Core.Models;
using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class LanguageCommand : ICommand
    {
        public string Name => "lng";

        public string Description => "\n" +
            "Структура: lng [Аргумент] \n" +
            "Отвечает за управление выбраннными языками\n" +
            "Аргументы:\n" +
            "[Без аргументов]: вывод выбранных языков программирования\n" +
            "--add(-a) [Параметр]: добавление языка в список для поиска\n" +
            "--available(-av): вывод доступных для поиска языков\n" +
            "--delete(-d) [Параметр]: удаление языка из списка для поиска\n";

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                if (!data.Options.SelectLanguages.Any())
                {
                    Console.WriteLine("\nНет выбранных языков\n");
                }
                else
                {
                    Console.WriteLine("\nСписок выбранных языков:\n");
                    foreach (AvailableLanguage lng in data.Options.SelectLanguages)
                    {
                        Console.WriteLine(Enum.GetName(typeof(AvailableLanguage), lng));
                    }
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
                        if (string.IsNullOrEmpty(item.Value))
                        {
                            Console.WriteLine("\nАргумент не должен быть пустым.\n" +
                                "Для получения дополнительной информации воспользуйтесь командой: ? lng\n");
                            return;
                        }
                        if (Enum.TryParse<AvailableLanguage>(item.Value, true, out AvailableLanguage langAdd))
                        {
                            data.Options.AddLanguage(langAdd);
                            Console.WriteLine($"Язык {item.Value} был успешно выбран");
                            return;
                        }
                        Console.WriteLine("\nПроизошла ошибка при добавлении\n");
                        break;
                    case "-all":
                        foreach (AvailableLanguage lang in Enum.GetValues<AvailableLanguage>())
                        {
                            if (lang == AvailableLanguage.None) continue;
                            data.Options.AddLanguage(lang);
                        }
                        Console.WriteLine("Все доступные языки были успешно выбраны");
                        break;
                    case "-av":
                    case "--available":
                        foreach (AvailableLanguage lang in Enum.GetValues<AvailableLanguage>())
                        {
                            Console.WriteLine($"{lang}");
                        }
                        break;
                    case "-d":
                    case "--delete":
                        if (string.IsNullOrEmpty(item.Value))
                        {
                            Console.WriteLine("\nАргумент не должен быть пустым.\n" +
                                "Для получения дополнительной информации воспользуйтесь командой: ? lng\n");
                            return;
                        }
                        if (Enum.TryParse<AvailableLanguage>(item.Value, true, out AvailableLanguage langDel))
                        {
                            data.Options.RemoveLanguage(langDel);
                            Console.WriteLine($"Язык {item.Value} был успешно удален");
                            return;
                        }
                        Console.WriteLine("\nПроизошла ошибка при удалении\n");
                        break;
                    default:
                        Console.WriteLine("\nНеверный ввод.\n" +
                            "Для получения дополнительной информации воспользуйтесь командой: ? lng\n");
                        break;
                }
            }
        }
    }
}
