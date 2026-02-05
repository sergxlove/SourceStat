namespace SourceStat.Models
{
    public class ToolsForCommand
    {
        public static Dictionary<string, string> ConvertArgsToDictionary(string[] args)
        {
            Dictionary<string, string> argsPairs = new Dictionary<string, string>();
            bool isKey = true;
            string keyCash = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                if (isKey)
                {
                    argsPairs.Add(args[i], string.Empty);
                    keyCash = args[i];
                    isKey = false;
                }
                else
                {
                    argsPairs[keyCash] = args[i];
                    isKey = true;
                }
            }
            return argsPairs;
        }
    }
}
