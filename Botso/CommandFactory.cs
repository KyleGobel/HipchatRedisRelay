using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hipchat.Models;
using ServiceStack;

namespace Botso
{
    public class CommandFactory
    {
        private readonly Dictionary<Type, ICommand> _commandDictionary; 
        public CommandFactory()
        {
            _commandDictionary = new Dictionary<Type, ICommand>
            {
                {typeof(AddScriptCommand), new AddScriptCommand()}
            };     
        }
        //factory method
        public ICommand CreateCommand(Type type, string cmdText, HipchatMessage originalMessage)
        {
            var cmd = _commandDictionary[type];

            var match = Regex.Match(cmdText, cmd.Pattern);

            if (match.Groups.Count > 1)
            {
                Enumerable.Range(1, match.Groups.Count -1).Each(
                    x => cmd.Arguments.Add(x,match.Groups[(int) x].Value));   
            }

            cmd.OriginalMessage = originalMessage;
            return cmd;
        }

    }
}