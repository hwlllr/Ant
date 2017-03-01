using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 后台框架.Controllers.Redis配置连接操作1
{
    public class redis1Controller : Controller
    {
        // GET: redis1
        public ActionResult Index()
        {
            using (IRedisClient con = Redis1Config.ClientManager.GetClient())
            {
                ////存数据
                Dictionary<string, People> dic = new Dictionary<string, People>();
                dic["yzk1"] = new People { ID = "123", Name = "234"};
                con.Set("user1", dic, DateTime.Now.AddMinutes(1));
                if (con.ContainsKey("user1"))
                {
                    Dictionary < string, People > pe = con.Get<Dictionary<string, People>>("user1");
                }

                ////存数据
                //con.Set<int>("ages", 18);
                //Dictionary<string, People> dics = new Dictionary<string, People>();
                //dics["yzk2"] = new People { ID = "123", Name = "234", Age = "19" };
                //con.Set<Dictionary<string, People>>("user2", dics, DateTime.Now.AddMinutes(2));

            }
            return View();
        }

        private class People {
            public string ID { get; set; }
            public string Name { get; set; }
        }
    }
}