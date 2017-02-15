using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 后台框架.Controllers.Redis配置连接操作;

namespace 后台框架.Controllers
{
    public class RedisController : Controller
    {
        // GET: Redis
        public ActionResult Index()
        {
            redis db = new redis();
            db.InsertUserToken("hwl5", new UserModel { UserId = "123", Name = "黄文林", Age = "18" });
            return View();
        }
    }
}