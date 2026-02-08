using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name => "help";

        public string Description => "\n" +
            "Структура: help \n" +
            "Отвечает за вывод информации о доступных командах\n";

        public async Task Execute(string[] args, DataCore data)
        {
            await Task.CompletedTask;
            if (args.Length > 0)
            {
                Console.WriteLine("\nФункция не принимает аргументы.\n" +
                    "Для получения дополнительной информации воспользуйтесь командой: ? help");
                return;
            }
            Console.WriteLine("\n" +
                "developer - Вывод информации о разработчике\n" +
                "dir - Работа со списком игнорируемых директорий\n" +
                "? - Получение подробной информации о команде\n" +
                "lng - Работа со списком языков программирования\n" +
                "go - Запуск подсчета строк кода в директории\n" +
                "version - Вывод текущей версии приложения\n" +
                "help - Вывод помощи\n");
        }
    }
}
