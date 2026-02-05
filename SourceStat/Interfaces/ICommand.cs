using SourceStat.Models;

namespace SourceStat.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Task Execute(string[] args, DataCore data);
    }
}
