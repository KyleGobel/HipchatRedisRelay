using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Hipchat.Models;
using Hipchat.Models.Requests;
using HipchatApiV2;
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
            const int roomId = 510675;
            const string postToUrl = "http://kylegobel.com/hipchatRedisRelay/message";

            var client = new HipchatClient();
            client.CreateWebHook(roomId, postToUrl, "", RoomEvent.Message,"Botso send message hook");

            Console.ReadLine();
        }


     
    }
}
