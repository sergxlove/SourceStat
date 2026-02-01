namespace SourceStat.Core.Models
{
    public enum AviableLanguage
    {
        None = 0,
        CSharp = 1,
        CPlusPlus = 2, 
        C = 3,
        Java = 4,
        Python = 5,
        JavaScript = 6,
        TypeScript = 7, 
        Go = 8,
        Rust = 9,
        Swift = 10,
        Kotlin = 11,
        Dart = 12, 
        PHP = 13,
        Ruby = 14, 
        HTML = 15,
        CSS = 16,
        SQL = 17,
    }
    public class AviableExtensions
    {
        public static List<string> GetExtensions(AviableLanguage language)
        {
            List<string> result;
            switch(language)
            {
                case AviableLanguage.None:
                    result = new List<string>();
                    break;
                case AviableLanguage.CSharp:
                    result = new List<string>() { ".cs", ".csx" };
                    break;
                case AviableLanguage.CPlusPlus:
                    result = new List<string>() { ".cpp", ".cc", ".cxx", ".c", ".h", ".hpp", ".hxx", ".hh" };
                    break;
                case AviableLanguage.C:
                    result = new List<string>() { ".c", ".h" };
                    break;
                case AviableLanguage.Java:
                    result = new List<string>() { ".java" };
                    break;
                case AviableLanguage.Python:
                    result = new List<string>() { ".py", ".pyw", ".pyc", ".pyo", ".pyd" };
                    break;
                case AviableLanguage.JavaScript:
                    result = new List<string>() { ".js", ".jsx", ".mjs", ".cjs" };
                    break;
                case AviableLanguage.TypeScript:
                    result = new List<string>() { ".ts", ".tsx" };
                    break;
                case AviableLanguage.Go:
                    result = new List<string>() { ".go" };
                    break;
                case AviableLanguage.Rust:
                    result = new List<string>() { ".rs" };
                    break;
                case AviableLanguage.Swift:
                    result = new List<string>() { ".swift" };
                    break;
                case AviableLanguage.Kotlin:
                    result = new List<string>() { ".kt", ".kts", ".ktm" };
                    break;
                case AviableLanguage.Dart:
                    result = new List<string>() { ".dart" };
                    break;
                case AviableLanguage.PHP:
                    result = new List<string>() { ".php", ".phtml", ".php3", ".php4", ".php5", ".php7", ".phps" };
                    break;
                case AviableLanguage.Ruby:
                    result = new List<string>() { ".rb", ".rbw", ".rake", ".ru", ".gemspec", ".erb" };
                    break;
                case AviableLanguage.HTML:
                    result = new List<string>() { ".html", ".htm", ".xhtml", ".html5" };
                    break;
                case AviableLanguage.CSS:
                    result = new List<string>() { ".css", ".scss", ".sass", ".less" };
                    break;
                case AviableLanguage.SQL:
                    result = new List<string>() { ".sql" };
                    break;
                default:
                    result = new List<string>();
                    break;
            }
            return result;
        }
    }
}
