using SourceStat.Core.Models;

namespace SourceStat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileChecker f = new FileChecker();
            Console.WriteLine(f.GetCountLineInFiles("D:\\projects\\projects\\SourceStat", "*.cs"));
        }
    }
}
