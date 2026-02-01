namespace SourceStat.Core.Models
{
    public class FileCheckerOptions
    {
        private HashSet<string> _ignoreDirectories { get; }
        private List<string> _selectExtensions { get; }
        private List<string> _defaultIgnore = new List<string>()
        {
            "bin", "obj", ".git", ".vs", "Debug", "Release"
        };

        public FileCheckerOptions()
        {
            _ignoreDirectories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _selectExtensions = new List<string>();
        }
        
        public void AddExtension(AviableLanguage language)
        {
            _selectExtensions.AddRange(AviableExtensions.GetExtensions(language));
        }

        public void RemoveExtension(AviableLanguage language)
        {
            foreach(string ex in AviableExtensions.GetExtensions(language))
            {
                _selectExtensions.Remove(ex);
            }
        }

        public void ClearExtension()
        {
            _selectExtensions.Clear();
        }

        public void AddIgnoreDirectories(string ignoreDirectory)
        {
            _ignoreDirectories.Add(ignoreDirectory);
        }
        
        public void RemoveIgnoreDirectories(string ignoreDirectory)
        {
            _ignoreDirectories.Remove(ignoreDirectory);
        }

        public void SetDefaultIgnores()
        {
            foreach(string str in  _defaultIgnore)
            {
                _ignoreDirectories.Add(str);
            }
        }

        public void RemoveDefaultIgnores()
        {
            foreach (string str in _defaultIgnore)
            {
                _ignoreDirectories.Remove(str);
            }
        }
    }
}
