using System;
using System.Collections.Generic;
using System.Linq;
using Hipchat.Models;
using HipchatApiV2;
using Jint;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace Botso
{
    public class Dispatcher
    {
        public static void DispatchCommands(List<ICommand> commands, HipchatMessage originalMessage)
        {
            if (!commands.Any())
                BotsoHipchatHelpers.SendError("I don't understand that command<br/>Type <code>Botso commands</code> for a list of commands", originalMessage.RoomId);

            BotsoHipchatHelpers.SendError("<code><pre>" +originalMessage.Dump() + "<pre></code>", originalMessage.RoomId);
            commands.Each(x => x.Execute());
        }
    }


    public class TestConsole
    {
        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}