namespace SourceStat.Core.Models
{
    public class FileCheckerOptions
    {
        public HashSet<string> IgnoreDirectories { get; private set; }
        public List<AvailableLanguage> SelectLanguages { get; private set; }
        public List<string> DefaultIgnore { get; private set; } = new List<string>()
        {
            "bin", "obj", ".git", ".vs", "Debug", "Release"
        };
        public bool IsAddDefault { get; private set; } = false;

        public FileCheckerOptions()
        {
            IgnoreDirectories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            SelectLanguages = new List<AvailableLanguage>();
        }

        public void AddLanguage(AvailableLanguage language)
        {
            SelectLanguages.Add(language);
        }

        public void RemoveLanguage(AvailableLanguage language)
        {
            SelectLanguages.Remove(language); 
        }

        public void AddIgnoreDirectories(string ignoreDirectory)
        {
            IgnoreDirectories.Add(ignoreDirectory);
        }
        
        public void RemoveIgnoreDirectories(string ignoreDirectory)
        {
            IgnoreDirectories.Remove(ignoreDirectory);
        }

        public void SetDefaultIgnores()
        {
            foreach(string str in DefaultIgnore)
            {
                IgnoreDirectories.Add(str);
            }
            IsAddDefault = true;
        }

        public void RemoveDefaultIgnores()
        {
            foreach (string str in DefaultIgnore)
            {
                IgnoreDirectories.Remove(str);
            }
            IsAddDefault = false;
        }
    }
}
