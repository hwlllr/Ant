using CachingFramework.Redis;
using CachingFramework.Redis.Contracts.RedisObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace 后台框架.Controllers.Redis配置连接操作
{
    public class redis
    {
        //需要引用三个dll
        //CachingFramework.Redis;
        //RedisAspNetProviders;
        //StackExchange.Redis
            

        Context context;
        int ExpireDuration = 60;//秒，过期时间 1小时
        readonly string StoreKey = "userToken:hash";

        public redis()
        {
            //context = new Context(ConfigurationManager.GetSection("RedisConfig").ToString());
            context = new Context(ConfigurationManager.ConnectionStrings["PortalRedisConnection"].ConnectionString);
        }



        public void InsertUserToken(string UserToken, UserModel UserInfo)
        {
            //获取全部的数据
            //IRedisList<UserModel> list = context.Collections.GetRedisList<UserModel>("userToken:hash");

            IRedisDictionary<string, UserModel> UserTokenHash = context.Collections.GetRedisDictionary<string, UserModel>(StoreKey);
            
            UserTokenHash.Add(UserToken, UserInfo);
            //UserTokenHash.TimeToLive = TimeSpan.FromSeconds(ExpireDuration);
            UserTokenHash.Expiration = DateTime.Now.AddMinutes(1);
            //查询某个key是否存在
            //bool exists = UserTokenHash.ContainsKey("");
            //获取某个key的value
            //UserModel us=UserTokenHash[""];
            //删除某个key
            //UserTokenHash.Remove("");
            //清空
            //UserTokenHash.Clear();

        }

        public void GetUserToken(string UserToken, UserModel UserInfo)
        {
            IRedisDictionary<string, UserModel> UserTokenHash = context.Collections.GetRedisDictionary<string, UserModel>(StoreKey);
            if (UserTokenHash.ContainsKey("hwl1"))
            {
                UserModel us = UserTokenHash["hwl1"];
            }
        }
    }
};