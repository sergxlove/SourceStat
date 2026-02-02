namespace SourceStat.Core.Models
{
    public class FileCheckerOptions
    {
        public HashSet<string> IgnoreDirectories { get; private set; }
        public List<string> SelectExtensions { get; private set; }
        public List<string> DefaultIgnore { get; private set; } = new List<string>()
        {
            "bin", "obj", ".git", ".vs", "Debug", "Release"
        };

        public FileCheckerOptions()
        {
            IgnoreDirectories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            SelectExtensions = new List<string>();
        }
        
        public void AddExtension(AviableLanguage language)
        {
            SelectExtensions.AddRange(AviableExtensions.GetExtensions(language));
        }

        public void RemoveExtension(AviableLanguage language)
        {
            foreach(string ex in AviableExtensions.GetExtensions(language))
            {
                SelectExtensions.Remove(ex);
            }
        }

        public void ClearExtension()
        {
            SelectExtensions.Clear();
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
            foreach(string str in  DefaultIgnore)
            {
                IgnoreDirectories.Add(str);
            }
        }

        public void RemoveDefaultIgnores()
        {
            foreach (string str in DefaultIgnore)
            {
                IgnoreDirectories.Remove(str);
            }
        }
    }
}
