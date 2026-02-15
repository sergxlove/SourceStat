namespace SourceStat.GraphicalApp.Models
{
    public class LanguageWithCheckBox
    {
        public string Name { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
