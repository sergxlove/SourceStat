using SourceStat.Commands;
using SourceStat.Interfaces;

namespace SourceStat.Cases
{
    public class ConsoleCases
    {
        public static ICommand[] UseConsoleCases()
        {
            ICommand[] commands =
            {
                new InfoCommand(),
                new VersionCommand(),
                new HelpCommand(),
                new DeveloperCommand(),
                new DirectoryCommand(),
                new LanguageCommand(),
                new StartCommand()
            };
            return commands;
        }
    }
}
