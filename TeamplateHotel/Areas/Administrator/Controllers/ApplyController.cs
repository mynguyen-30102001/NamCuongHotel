using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Database;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class ApplyController : BaseController
    {
        //
        // GET: /Administrator/Contact/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang ứng tuyển";
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var db = new MyDbDataContext();
                List<SendEmailRecuitment> listContact = db.SendEmailRecuitments.ToList();

                var records = listContact.Select(a => new
                {
                    a.ID,
                    a.FullName,
                    a.Phone,
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
            SendEmailRecuitment del = db.SendEmailRecuitments.FirstOrDefault(a => a.ID == id);
            if (del == null)
            {
                return Json(new { Result = "ERROR", Message = "Liên hệ không tồn tại" });
            }
            db.SendEmailRecuitments.DeleteOnSubmit(del);
            db.SubmitChanges();
            return Json(new { Result = "OK", Message = "Xóa liên hệ thành công" });
        }

        [HttpGet]
        public ActionResult Detail(int Id)
        {
            var db = new MyDbDataContext();
            SendEmailRecuitment detail = db.SendEmailRecuitments.FirstOrDefault(a => a.ID == Id);
            if (detail == null)
            {
                TempData["Messages"] = "Liên hệ không tồn tại";
                return RedirectToAction("Index");
            }
            return View("Detail", detail);
        }
    }
}