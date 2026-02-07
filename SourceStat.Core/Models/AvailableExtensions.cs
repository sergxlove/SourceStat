namespace SourceStat.Core.Models
{
    public enum AvailableLanguage
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
    public class AvailableExtensions
    {
        public static List<string> GetExtensions(AvailableLanguage language)
        {
            List<string> result;
            switch(language)
            {
                case AvailableLanguage.None:
                    result = new List<string>();
                    break;
                case AvailableLanguage.CSharp:
                    result = new List<string>() { "*.cs", "*.csx" };
                    break;
                case AvailableLanguage.CPlusPlus:
                    result = new List<string>() { "*.cpp", "*.cc", "*.cxx", "*.c", "*.h", "*.hpp", "*.hxx", "*.hh" };
                    break;
                case AvailableLanguage.C:
                    result = new List<string>() { "*.c", "*.h" };
                    break;
                case AvailableLanguage.Java:
                    result = new List<string>() { "*.java" };
                    break;
                case AvailableLanguage.Python:
                    result = new List<string>() { "*.py", "*.pyw", "*.pyc", "*.pyo", "*.pyd" };
                    break;
                case AvailableLanguage.JavaScript:
                    result = new List<string>() { "*.js", "*.jsx", "*.mjs", "*.cjs" };
                    break;
                case AvailableLanguage.TypeScript:
                    result = new List<string>() { "*.ts", "*.tsx" };
                    break;
                case AvailableLanguage.Go:
                    result = new List<string>() { "*.go" };
                    break;
                case AvailableLanguage.Rust:
                    result = new List<string>() { "*.rs" };
                    break;
                case AvailableLanguage.Swift:
                    result = new List<string>() { "*.swift" };
                    break;
                case AvailableLanguage.Kotlin:
                    result = new List<string>() { "*.kt", "*.kts", "*.ktm" };
                    break;
                case AvailableLanguage.Dart:
                    result = new List<string>() { "*.dart" };
                    break;
                case AvailableLanguage.PHP:
                    result = new List<string>() { "*.php", "*.phtml", "*.php3", "*.php4", "*.php5", "*.php7", "*.phps" };
                    break;
                case AvailableLanguage.Ruby:
                    result = new List<string>() { "*.rb", "*.rbw", "*.rake", "*.ru", "*.gemspec", "*.erb" };
                    break;
                case AvailableLanguage.HTML:
                    result = new List<string>() { "*.html", "*.htm", "*.xhtml", "*.html5" };
                    break;
                case AvailableLanguage.CSS:
                    result = new List<string>() { "*.css", "*.scss", "*.sass", "*.less" };
                    break;
                case AvailableLanguage.SQL:
                    result = new List<string>() { "*.sql" };
                    break;
                default:
                    result = new List<string>();
                    break;
            }
            return result;
        }
    }
}
