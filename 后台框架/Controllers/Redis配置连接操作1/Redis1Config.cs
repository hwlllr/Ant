
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 后台框架.Controllers.Redis配置连接操作1
{
    public class Redis1Config
    {
        //需要引用三个dll
        //ServiceStack.Interfaces
        //ServiceStack.Redis;
        public static PooledRedisClientManager ClientManager { get; private set; }
        static Redis1Config()
        {
            RedisClientManagerConfig redisConfig = new RedisClientManagerConfig();
            redisConfig.MaxWritePoolSize = 128;
            redisConfig.MaxReadPoolSize = 128;

            //可以读写分离，指定一台服务器读，一台写。
            // new PooledRedisClientManage(读写的服务器地址，只读的服务器地址
            ClientManager = new PooledRedisClientManager(new string[] { "127.0.0.1" },
                new string[] { "127.0.0.1" }, redisConfig);
        }
    }
}