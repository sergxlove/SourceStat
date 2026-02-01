namespace SourceStat.Core.Models
{
    public class FileChecker
    {
        public static long GetCountFiles(string directory, string extensions)
        {
            long fileCount = 0;
            foreach(string file in Directory.EnumerateFiles(directory, extensions, 
                SearchOption.AllDirectories))
            {
                fileCount++;
            }
            return fileCount;
        }

        public static long GetCountLineInFiles(string directory, string extensions)
        {
            long lineCount = 0;
            string[] lines;
            foreach (string file in Directory.EnumerateFiles(directory, extensions, 
                SearchOption.AllDirectories))
            {
                lines = File.ReadAllLines(file);
                lineCount += lines.Length;
            }
            return lineCount;
        }
    }
}
