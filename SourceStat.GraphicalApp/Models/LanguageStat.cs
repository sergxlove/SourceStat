namespace SourceStat.GraphicalApp.Models
{
    public class LanguageStat
    {
        public string Name { get; set; } = string.Empty;
        public long FilesCount { get; set; }
        public long LinesCount { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
