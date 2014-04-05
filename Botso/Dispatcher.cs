using HipchatApiV2;

namespace Botso
{
    public class Dispatcher
    {
        public static void DispatchCommand(BotsoCommand command)
        {
            var hipchatClient = new HipchatClient();

            hipchatClient.SendMessage(510675, "You entered command: " + command.CommandText);
        }
    }
}