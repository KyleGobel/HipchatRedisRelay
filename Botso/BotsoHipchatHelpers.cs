using Hipchat.Models;
using HipchatApiV2;

namespace Botso
{
    public class BotsoHipchatHelpers
    {
        public static void SendError(string errorMessage, int roomId)
        {
            var hipchatClient = new HipchatClient();

            errorMessage = "<b>Malfunction!</b><br/>" + errorMessage;
            hipchatClient.SendNotification(roomId, errorMessage, HipchatApiV2.Enums.RoomColors.Red);
        }
    }
}