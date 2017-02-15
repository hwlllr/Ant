using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WXShop.Wx;

namespace 后台框架.Controllers
{
    //微信相关
    public class WxController : Controller
    {
        public ActionResult Index()
        {
            //配置微信首次请求页面通过回调获取code
            string appID = WxPublic.appID;
            string appsecret = WxPublic.appsecret;
            string auth_url = WxPublic.auth_url;
            string redirect_uri = "";
            redirect_uri = WxPublic.redirect_uri;
            auth_url = string.Format(auth_url, appID, HttpUtility.UrlEncode(redirect_uri));
            Response.Redirect(auth_url);
            return View();
        }

        public ActionResult Auth(string code = "")
        {
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                if (code == "")//用户不同意登录
                {
                    return Redirect("/Wx/Error1");
                }
                else {
                    HttpContent httpContent = new StringContent("");
                    var httpClient = new HttpClient();

                    //通过code获取access_token
                    string url = string.Format(WxPublic.access_token_url, WxPublic.appID, WxPublic.appsecret, code);
                    var responseJson = httpClient.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
                    dynamic dym = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseJson);

                    //正确返回
                    //"access_token":"ACCESS_TOKEN",
                    //"expires_in":7200,
                    //"refresh_token":"REFRESH_TOKEN",
                    //"openid":"OPENID",
                    //"scope":"SCOPE"
                    //错误返回
                    //{"errcode":40029,"errmsg":"invalid code"}

                    if (dym.errcode == 40029)
                    {
                        return Redirect("/Wx/Error2");
                    }

                    HttpContent httpContent1 = new StringContent("");
                    var httpClient1 = new HttpClient();
                    //通过access_token获取用户信息
                    var responseJson1 = httpClient1.PostAsync(string.Format(WxPublic.user, dym.access_token, dym.openid), httpContent1)
                        .Result.Content.ReadAsStringAsync().Result;
                    dynamic dym1 = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseJson1);

                    //正确返回
                    // "openid":" OPENID",
                    //  " nickname": NICKNAME,
                    //  "sex":"1",
                    //  "province":"PROVINCE"
                    //  "city":"CITY",
                    //  "country":"COUNTRY",
                    //   "headimgurl":    "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46", 
                    //"privilege":[
                    //"PRIVILEGE1"
                    //"PRIVILEGE2"
                    //   ],
                    //   "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
                    //错误返回
                    //{ "errcode":40003,"errmsg":" invalid openid "}
                    if (dym.errcode == 40003)
                    {
                        return Redirect("/Wx/Error3");
                    }

                    //保存用户相关信息
                    //UserBaseInfoModel user = new UserBaseInfoModel();
                    //user.OpenID = dym1.openid;
                    //user.UserName = HttpUtility.UrlEncode((string)dym1.nickname);
                    //user.HeadImgUrl = dym1.headimgurl;
                    //user.AppID = WxPublic.appID;
                    //System.Web.HttpContext.Current.Session["user"] = user;

                }
            }
            //return Redirect("/home/index");
            return View();
        }

        //支付页面
        public ActionResult pay(int orderID = 0, string returnURL = "")
        {
            //OrderListBusinessLogic orderListBLL = new OrderListBusinessLogic();
            //DataTable orderDT = orderListBLL.GetWXPayInfo(orderID);
            //if (orderDT.Rows.Count == 0)
            //{
            //    return Redirect("/Wx/PayErrorPage");
            //}
            //UserBaseInfoModel userBaseInfo = (UserBaseInfoModel)System.Web.HttpContext.Current.Session["user"];
            //string openid = userBaseInfo.OpenID;

            string openid = "aewgrdndbfv";
            JsApiPay jsApiPay = new JsApiPay(this);
            jsApiPay.openid = openid;
            //jsApiPay.total_fee = total_fee;//金额 单位为分

            ViewBag.orderID = orderID;
            ViewBag.returnURL = returnURL;//取消支付返回商品页面
            try
            {
                //下单
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult(orderID);

                ViewBag.wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数 
            }
            catch (Exception ex)
            {
                //下单失败
            }
            return View();
        }

        //支付回调页面
        public ActionResult NotifyPage()
        {
            ResultNotify resultNotify = new ResultNotify(this);
            resultNotify.ProcessNotify();
            return View();
        }

        /// <summary>
        /// 支付成功
        /// </summary>
        /// <returns></returns>
        public ActionResult PaySuccess(int orderID)
        {
            //OrderListBusinessLogic orderBLL = new OrderListBusinessLogic();
            ////判定一下是否已返回状态
            //if (orderBLL.CheckPayReturn(orderID))
            //{
            //    return Json(1, "application/json", JsonRequestBehavior.AllowGet);
            //}
            //int result = orderBLL.PaySuccess(orderID, (int)PayResult.确认付款);
            return Json("", "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaySuccessPage()
        {
            return View();
        }

        public ActionResult PayErrorPage()
        {
            return View();
        }
    }
}