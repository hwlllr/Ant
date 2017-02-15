using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 后台框架.Controllers.Redis配置连接操作
{
    [Serializable]
    public class UserModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
    }
}