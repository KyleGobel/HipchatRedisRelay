using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Configuration;
using ServiceStack.Redis;

namespace Botso
{
    class Program
    {
        static void Main(string[] args)
        {
            //do injection or setup here
            IRedisClient redisClient = new RedisClient(ConfigUtils.GetAppSetting("redisServer"));
            var app = new App(redisClient);
            app.Startup();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
