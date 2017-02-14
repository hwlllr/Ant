using BID.DAL.DataAccess;
using LC.LS.DataAccess;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 后台框架.Controllers
{
    public class HomeController : Controller
    {
        //框架模板
        public ActionResult Index()
        {
            return View();
        }

        //连接SqlServer数据库
        public ActionResult JoinDB()
        {
            //web.config
            //<section name="dataConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings, Microsoft.Practices.EnterpriseLibrary.Data"/>
            //<dataConfiguration defaultDatabase="Binconn"/>
            try
            {
                string sql = "select * from ant";
                SqlDataAccess SqlDataAccess = SqlDataAccess.CreateDataAccess();
                DataTable ds = SqlDataAccess.ExecuteDataSet(sql).Tables[0];
                ViewBag.text = "连接数据库成功！";
            }
            catch (Exception ex)
            {
                ViewBag.text = "连接数据库失败！";
            }
            return View();
        }

        //连接MySql数据库
        public ActionResult JoinMySqlDB()
        {
            try
            {
                string sql = "select * from ant";

                MySqlDataAccess mysqlHelper = new MySqlDataAccess();
                MySqlParameter[] pa = { };
                DataTable ds = mysqlHelper.ExecuteDataTable(sql, pa);
                ViewBag.text = "连接数据库成功！";
            }
            catch (Exception ex)
            {
                ViewBag.text = "连接数据库失败！";
            }
            return View("~/Views/Home/JoinDB.cshtml");
        }
    }
}