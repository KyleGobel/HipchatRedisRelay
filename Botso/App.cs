using System;
using Hipchat.Models;
using ServiceStack;
using ServiceStack.Redis;

namespace Botso
{
    public class App
    {
        #region Initalize Code
        private readonly IRedisClient _redisClient;
        public App(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        
        public void Startup()
        {
            using (var subscription = _redisClient.CreateSubscription())
            {
                subscription.OnMessage = (channel, msg) =>
                {
                    var message = msg.FromJson<HipchatMessage>();
                    if (message != null)
                        HandleMessage(message);
                };

                subscription.SubscribeToChannels("HipchatMessage"); //blocking
            }

        }
        #endregion

        /// <summary>
        /// This method is called for every message received
        /// this is the main starting point where we kick off the parser
        /// and the dispatcher
        /// </summary>
        /// <param name="message">The message that was sent via hipchat</param>
        private void HandleMessage(HipchatMessage message)
        {
            var command = Parser.GetCommandFromMessage(message);

            if (command != null)
                Dispatcher.DispatchCommand(command);
        }
    }
}