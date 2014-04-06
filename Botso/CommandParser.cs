using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hipchat.Models;

namespace Botso
{
    public class CommandParser
    {
        private const string CommandPrefix = "botso ";

        /// <summary>
        /// Returns weather or not a message is a command
        /// </summary>
        /// <param name="message">The hipchat message received</param>
        public static bool IsCommand(HipchatMessage message)
        {
            return message.MessageSent.StartsWith(CommandPrefix,true, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets a typed ICommand from a hipchat message
        /// </summary>
        /// <param name="message">The hipchat message recieved</param>
        /// <returns>A typed ICommand object</returns>
        public static List<ICommand> GetCommandFromMessage(HipchatMessage message)
        {
            if (IsCommand(message))
            {
                var cmdText = GetCommandText(message.MessageSent);

                var commandFactory = new CommandFactory();
                //find what type this command is (can be multiples)
                var cmdTypes = CommandHelper.GetCommandTypes(cmdText);

                var matchedCommands = cmdTypes.Select(type => commandFactory.CreateCommand(type, cmdText, message)).ToList();
                return matchedCommands;
            }
                
            return new List<ICommand>();
        }

        private static string GetCommandText(string fullCmdText)
        {
            return fullCmdText.Replace(CommandPrefix, "");
        }
    }
}