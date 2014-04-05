using System.Globalization;
using Hipchat.Models;

namespace Botso
{
    public class Parser
    {
        private const string CommandPrefix = "botso ";
        public static bool IsCommand(HipchatMessage message)
        {
            return message.MessageSent.StartsWith(CommandPrefix,true, CultureInfo.InvariantCulture);
        }

        public static BotsoCommand GetCommandFromMessage(HipchatMessage message)
        {
            if (IsCommand(message))
                return new BotsoCommand {CommandText = message.MessageSent.Replace(CommandPrefix, "")};

            return null;
        }
    }

    public class BotsoCommand
    {
        public string CommandText { get; set; }
    }
}