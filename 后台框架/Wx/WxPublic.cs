using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXShop.Wx
{
    /// <summary>
    /// 微信相关-hwl-2016-10-17
    /// </summary>
    public class WxPublic
    {
        public static string appID = ConfigurationManager.AppSettings["wx_appID"].ToString();

        public static string appsecret = ConfigurationManager.AppSettings["wx_appsecret"].ToString();
        //回调页面
        public static string redirect_uri = ConfigurationManager.AppSettings["wx_redirect_uri"].ToString();
        public static string redirect_uri1 = ConfigurationManager.AppSettings["wx_redirect_uri1"].ToString();
        //首次请求url
        public static string auth_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo#wechat_redirect";
        //获取access_token url
        public static string access_token_url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        //获取用户信息url
        public static string user = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        //获取SDK的access_token url
        public static string sdk_access_token_url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        //获取jsapi_ticket url
        public static string jsapi_ticket_url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
    }

    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        public static string APPID = ConfigurationManager.AppSettings["wx_appID"].ToString();
        public static string MCHID = ConfigurationManager.AppSettings["wx_MCHID"].ToString();
        public static string KEY = ConfigurationManager.AppSettings["wx_KEY"].ToString();
        public static string APPSECRET = ConfigurationManager.AppSettings["wx_appsecret"].ToString();

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public const string SSLCERT_PATH = "cert/apiclient_cert.p12";
        public const string SSLCERT_PASSWORD = "1233410002";



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public static string NOTIFY_URL = ConfigurationManager.AppSettings["NOTIFY_URL"].ToString();

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public const string IP = "123.56.187.202";


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public const string PROXY_URL = "http://10.152.18.220:8080";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 0;
    }
}
