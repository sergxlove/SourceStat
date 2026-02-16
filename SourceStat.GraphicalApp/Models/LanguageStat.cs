namespace SourceStat.GraphicalApp.Models
{
    public class LanguageStat
    {
        public string Name { get; set; } = string.Empty;
        public int FilesCount { get; set; }
        public int LinesCount { get; set; }
        public double Percentage { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
