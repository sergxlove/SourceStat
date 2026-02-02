namespace SourceStat.Core.Models
{
    public class FileChecker
    {
        public static long GetCountFiles(string directory, FileCheckerOptions options)
        {
            long fileCount = 0;
            foreach(string extensions in options.SelectExtensions)
            {
                foreach(string file in Directory.EnumerateFiles(directory, extensions, 
                    SearchOption.AllDirectories))
                {
                    if(!IsInIgnoredDir(file, options)) fileCount++;
                }
            }
            return fileCount;
        }

        public static long GetCountLineInFiles(string directory, FileCheckerOptions options)
        {
            long lineCount = 0;
            string[] lines;
            foreach (string extensions in options.SelectExtensions)
            {
                foreach (string file in Directory.EnumerateFiles(directory, extensions, 
                    SearchOption.AllDirectories))
                {
                    if (!IsInIgnoredDir(file, options))
                    {
                        lines = File.ReadAllLines(file);
                        lineCount += lines.Length;
                    }
                }
            }
            return lineCount;
        }

        private static bool IsInIgnoredDir(string filePath, FileCheckerOptions options)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(directory))
                return false;
            string[] folders = directory.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            foreach (string folder in folders)
            {
                if(options.IgnoreDirectories.Contains(folder))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
