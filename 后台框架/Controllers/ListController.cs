using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 后台框架.Controllers
{
    public class ListController : Controller
    {
        /// <summary>
        /// 列表模板页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            //LYAdminBusinesscLogic bll = new LYAdminBusinesscLogic();
            ////下拉数据
            //DataSet ds = bll.GetFundsState();
            //ViewData["StateData"] = FundsCommon.DataToDropDownList(ds.Tables[0]);
            return View("~/Views/Funds/LYAdmin/List.cshtml");
        }

        /// <summary>
        /// 列表分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult page(/*FundsProductPageModel model*/)
        {
            //LYAdminBusinesscLogic bll = new LYAdminBusinesscLogic();
            //int Total = 0;
            //DataTable dtProductlist = bll.GetPageInfo(out Total, model);
            //List<FundsProductModel> list = DataTableToEntity.GetEntities<FundsProductModel>(dtProductlist);

            int success = 0;
            var ReturnObject = new
            {
                //flag = 1,
                //error = success,
                //message = ErrorMessage.GetErrorMessageByCode(success.ToString()),
                //model = new
                //{
                //    total = Total,
                //    page = model.page,
                //    column = model.column,
                //    datalist = list
                //}
            };
            return Json(ReturnObject, "application/json", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 操作页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            //LYAdminBusinesscLogic bll = new LYAdminBusinesscLogic();
            ////下拉数据
            //DataSet ds = bll.GetFundsState();
            //ViewData["StateData"] = FundsCommon.DataToDropDownList(ds.Tables[0]);
            //ViewData["VarietiesData"] = FundsCommon.DataToDropDownList(ds.Tables[1]);
            //ViewData["Dividend"] = FundsCommon.DataToDropDownList(ds.Tables[3]);
            //FundsProductModel model = new FundsProductModel();
            //model.Id = "0";
            //if (id > 0)
            //{
            //    DataTable da = bll.GetFundsProduct(id);
            //    model.Id = da.Rows[0]["Id"].ToString();
            //    model.Title = da.Rows[0]["Title"].ToString();
            //    model.ReturnRate = da.Rows[0]["ReturnRate"].ToString();
            //    model.Price = da.Rows[0]["Price"].ToString();
            //    model.Term = da.Rows[0]["Term"].ToString();
            //    model.SumNum = da.Rows[0]["SumNum"].ToString();
            //    model.SoldNum = da.Rows[0]["SoldNum"].ToString();
            //    model.Varieties = da.Rows[0]["Varieties"].ToString();
            //    model.State = da.Rows[0]["State"].ToString();
            //    model.StartTime = da.Rows[0]["StartTime"].ToString();
            //    model.EndTime = da.Rows[0]["EndTime"].ToString();
            //    model.Describes = da.Rows[0]["Describes"].ToString();
            //    model.Dividend = da.Rows[0]["Dividend"].ToString();
            //    model.ProfitStartTime = da.Rows[0]["ProfitStartTime"].ToString();
            //}
            //return View("~/Views/Funds/LYAdmin/Edit.cshtml", model);
            return View();
        }

        /// <summary>
        /// 上传后台
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public ActionResult EditPortrait(HttpPostedFileBase fileData)
        {

            //文件还是图片

            if (fileData != null)
            {
                try
                {
                    ////保存到本地////////////////////////////////////////////

                    string saveName = "";
                    string guid = Guid.NewGuid().ToString();

                    // 文件上传后的保存路径  上传本地服务器
                    string filePath = Server.MapPath("~/images/Funds/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    saveName = guid + fileExtension; // 保存文件名称
                    fileData.SaveAs(filePath + saveName);

                    string ServerFileName1 = @"/images/Funds/" + saveName;

                    return Json(new { Success = true, FileName = fileName, SaveName = saveName, FilePath = ServerFileName1 });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 操作页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit1()
        {
            return View();
        }
    }
}