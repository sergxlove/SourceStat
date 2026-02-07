using SourceStat.Interfaces;
using SourceStat.Models;

namespace SourceStat.Commands
{
    public class InfoCommand : ICommand
    {
        public string Name => "?";

        public string Description => throw new NotImplementedException();

        public Task Execute(string[] args, DataCore data)
        {
            throw new NotImplementedException();
        }
    }
}
