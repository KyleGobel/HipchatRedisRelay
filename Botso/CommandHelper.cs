using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ServiceStack;

namespace Botso
{
    public class CommandHelper
    {
        public static Dictionary<string, Type> GlobalCommandsRegistry
        {
            get
            {
                return new Dictionary<string, Type>
                {
                    { "^add script (.*)$", typeof(AddScriptCommand)}
                };
            }
        }

        public static List<Type> GetCommandTypes(string fullCmdText)
        {
            return GlobalCommandsRegistry
                .Where(x => Regex.IsMatch(fullCmdText, x.Key, RegexOptions.IgnoreCase))
                .Select(x => x.Value)
                .ToList();
        }
    }

    public enum CommandType
    {
        Global,
        Custom
    }
}