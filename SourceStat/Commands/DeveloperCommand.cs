using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
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
}
