using SourceStat.Interfaces;

namespace SourceStat.Models
{
    public class ExecuteCommandCore
    {
        public List<ICommand> Commands { get; } = [];

        public void ExecuteCommand(string command, DataCore data)
        {
            string[] parts = command.Split(' ');
            string cmdName = parts[0];
            string[] args = parts.Skip(1).ToArray();
            ICommand? cmd = Commands.FirstOrDefault(a => a.Name == cmdName);
            if (cmd is not null)
            {
                cmd.Execute(args, data);
            }
            else
            {
                Console.WriteLine($"\nНе удалось распознать команду: {cmdName}. \n" +
                    $"Для получения дополнительной иформации воспользуйтесь командой: help\n");
            }
        }

        public void Add(ICommand command)
        {
            Commands.Add(command);
        }

        public void AddRange(ICommand[] commands)
        {
            Commands.AddRange(commands);
        }
    }
}
