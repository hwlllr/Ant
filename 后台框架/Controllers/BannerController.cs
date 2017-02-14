using common.验证码;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 后台框架.Controllers
{
    public class BannerController : Controller
    {
        /// <summary>
        /// 横幅轮播
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 加载验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetImg(int width = 100, int height = 40, int fontsize = 20)
        {
            string code = string.Empty;
            int codeLength = 4;
            byte[] bytes = CodeHelper.CreateValidateGraphic(out code, codeLength, width, height, fontsize);
            Session["Validate"] = code;
            return File(bytes, @"image/jpeg");
        }
    }
}