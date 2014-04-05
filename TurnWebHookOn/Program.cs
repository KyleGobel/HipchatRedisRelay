using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Hipchat.Models;
using Hipchat.Models.Requests;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Text;

namespace TurnWebHookOn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Setup shit
            JsConfig.EmitCamelCaseNames = true;
            const int roomId = 510675;
            const string postToUrl = "http://kylegobel.com/hipchatRedisRelay/message";

            var response = CreateWebHook(roomId, postToUrl, "", RoomEvent.Message, "test hook");
            Console.ReadLine();
        }


        public static string CreateWebHook(int roomId, string url, string pattern, RoomEvent eventType, string name)
        {
            var authToken = ConfigUtils.GetAppSetting("hipchat_auth_token");
            var request = new CreateWebHookRequest
            {
                Event = eventType,
                Pattern = pattern,
                Url = url,
                Name = name
            };
            return HipchatEndpoints.CreateRoomEndpoint(roomId, authToken).PostJsonToUrl(request);
        }
    }
}
