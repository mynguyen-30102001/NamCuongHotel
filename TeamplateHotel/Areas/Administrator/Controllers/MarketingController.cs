using ProjectLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class MarketingController : BaseController
    {
        //
        // GET: /Administrator/EmailMarketing/

        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang quảng lý tuyển dụng";
            return View();
        }
        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var db = new MyDbDataContext();
                List<EmailMarketing> listContact = db.EmailMarketings.ToList();

                var records = listContact.Select(a => new
                {
                    a.Id,
                    a.Email,
                }).Skip(jtStartIndex).Take(jtPageSize).ToList();
                //Return result to jTable
                return Json(new { Result = "OK", Records = records, TotalRecordCount = listContact.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var db = new MyDbDataContext();
            EmailMarketing del = db.EmailMarketings.FirstOrDefault(a => a.Id == id);
            if (del == null)
            {
                return Json(new { Result = "ERROR", Message = "Liên hệ không tồn tại" });
            }
            db.EmailMarketings.DeleteOnSubmit(del);
            db.SubmitChanges();
            return Json(new { Result = "OK", Message = "Xóa liên hệ thành công" });
        }
    }
}
