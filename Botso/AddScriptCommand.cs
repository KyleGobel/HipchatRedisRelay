using System.Collections.Generic;
using System.Linq;
using Hipchat.Models;
using HipchatApiV2;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace Botso
{
    public class AddScriptCommand : ICommand
    {
        public AddScriptCommand()
        {
            Pattern = "^add script (.*)$";
            Arguments = new SortedList<int, string>();
        }
        public string Pattern { get; private set; }

        public void Execute()
        {
            if (OriginalMessage.FileUrl == null)
            {
                BotsoHipchatHelpers.SendError("To add a script you must include a script file.", OriginalMessage.RoomId);
                return;
            }

            if (!Arguments.Any())
            {
                BotsoHipchatHelpers.SendError("The add script commandlet requires one paramater, the pattern to register to the script", OriginalMessage.RoomId);
                return;
            }

            var script = new HipchatScript()
            {
                Filename = "testFile.js",
                Pattern = Arguments.First().Value,
                Priority = 1
            };
            BotsoHipchatHelpers.SendError(script.Dump(), OriginalMessage.RoomId);
        }

        public SortedList<int, string> Arguments { get; set; }
        public HipchatMessage OriginalMessage { get; set; }
    }
}