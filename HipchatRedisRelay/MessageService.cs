using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Hipchat.Models;
using ServiceStack;
using ServiceStack.Text;

namespace HipchatRedisRelay
{
    public class MessageService : Service
    {
        public object Post(RoomMessageJson roomMessage)
        {
            var hipchatMessage = default(HipchatMessage);
            try
            {
                hipchatMessage = roomMessage.ToHipchatMessage();
                File.WriteAllText(HttpContext.Current.Server.MapPath("messages.log"), roomMessage.Dump());
            }
            catch (Exception x)
            {
                File.WriteAllText(HttpContext.Current.Server.MapPath("errors.log"), x.ToString());
            }
            return "This is the message we got : " + hipchatMessage.ToJson();
        }

    }

    public static class JsonMessageExtensions
    {
        public static HipchatMessage ToHipchatMessage(this RoomMessageJson roomMessage)
        {
            var hipchatMessage = new HipchatMessage
            {
                DateSent = roomMessage.Item.Message.Date,
                FileUrl = roomMessage.Item.Message.File,
                From = new HipchatUser
                {
                    MentionName = roomMessage.Item.Message.From.Mention_Name,
                    Name = roomMessage.Item.Message.From.Name,
                    UserId = roomMessage.Item.Message.From.Id
                },

                MessageSent = roomMessage.Item.Message.Message,
                RoomId = roomMessage.Item.Room.Id,
                RoomName = roomMessage.Item.Room.Name
            };

            if (roomMessage.Item.Message.Mentions != null)
            {
                hipchatMessage.Mentions = roomMessage.Item.Message.Mentions.Select(x => new HipchatUser
                {
                    MentionName = x.Mention_Name,
                    Name = x.Name,
                    UserId = x.Id
                }).ToList();
            }

            return hipchatMessage;
        }
    }
    #region Json Expected Objects

    public class RoomMessageJson
    {
        public string Event { get; set; }
        public RoomMessageItemJson Item { get; set; }
        public string OAuth_Client_Id { get; set; }
        public string Webhook_Id { get; set; }
    }

    public class RoomMessageItemJson
    {
        public RoomMessageItemMessageJson Message { get; set; }
        public RoomMessageItemRoomJson Room { get; set; }
    }

    public class RoomMessageItemRoomJson
    {
        public int Id { get; set; }
        public RoomMessageItemRoomLinks Links { get; set; }
        public string Name { get; set; }
    }

    public class RoomMessageItemRoomLinks
    {
        public string Members { get; set; }
        public string Self { get; set; }
        public string Webhooks { get; set; }
    }

    public class RoomMessageItemMessageJson
    {
        public DateTime Date { get; set; }
        public string File { get; set; }
        public RoomUserJson From { get; set; }
        public int Id { get; set; }
        public List<RoomUserJson> Mentions { get; set; }
        public string Message { get; set; }
    }

    public class RoomUserJson
    {
        public int Id { get; set; }
        public RoomMessageItemRoomLinks Links { get; set; }
        public string Name { get; set; }
        public string Mention_Name { get; set; }
    }
    #endregion
}