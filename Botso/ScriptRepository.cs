using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Redis;

namespace Botso
{
    public class ScriptRepository
    {
        private readonly IRedisClient _client;
        public ScriptRepository(IRedisClient client = null)
        {
            _client = client ?? new RedisClient(ConfigUtils.GetAppSetting("redisServer"));
        }

        public List<HipchatScript> GetAllScripts()
        {
            var typedClient = _client.As<HipchatScript>();

            return typedClient.GetAll().ToList();
        }

        public List<HipchatScript> GetScriptsMatching(string command)
        {
            var scripts = GetAllScripts();

            return scripts.Where(x =>
            {
                var regex = new Regex(x.Pattern);
                return regex.IsMatch(command);
            }).ToList();
        }

        public void SaveScript(HipchatScript script)
        {
            var typedClient = _client.As<HipchatScript>();
            typedClient.SetEntry(script.Pattern, script);
        }
    }

    public class HipchatScript
    {
        public string Pattern { get; set; }
        public string Filename { get; set; }
        public int Priority { get; set; }
    }
}