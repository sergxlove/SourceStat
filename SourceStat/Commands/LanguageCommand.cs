using SourceStat.Core.Models;
using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class LanguageCommand : ICommand
    {
        public string Name => "lng";

        public string Description => throw new NotImplementedException();

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if (args.Length == 0)
            {
                if(!data.Options.SelectLanguages.Any())
                {
                    Console.WriteLine("Нет выбранных языков");
                }
                else
                {
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
                            Console.WriteLine("Аргумент не должен быть пустым");
                            return;
                        }
                        if (Enum.TryParse<AvailableLanguage>(item.Value, true, out AvailableLanguage langAdd))
                        {
                            data.Options.AddLanguage(langAdd);
                            Console.WriteLine($"Язык {item.Value} был успешно выбран");
                            return;
                        }
                        Console.WriteLine("Произошла ошибка при добавлении");
                        break;
                    case "-av":
                    case "--available":
                        foreach (AvailableLanguage lang in Enum.GetValues<AvailableLanguage>())
                        {
                            Console.WriteLine($"{lang} ({(int)lang})");
                        }
                        break;
                    case "-d":
                    case "--delete":
                        if (string.IsNullOrEmpty(item.Value))
                        {
                            Console.WriteLine("Аргумент не должен быть пустым");
                            return;
                        }
                        if (Enum.TryParse<AvailableLanguage>(item.Value, true, out AvailableLanguage langDel))
                        {
                            data.Options.RemoveLanguage(langDel);
                            Console.WriteLine($"Язык {item.Value} был успешно удален");
                            return;
                        }
                        Console.WriteLine("Произошла ошибка при удалении");
                        break;
                    default:
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }
    }
}
