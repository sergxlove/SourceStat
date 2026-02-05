using SourceStat.Core.Models;

namespace SourceStat.Models
{
    public class DataCore
    {
        public FileCheckerOptions Options { get; set; }
        public DataCore() 
        {
            Options = new FileCheckerOptions();
        }
    }
}
