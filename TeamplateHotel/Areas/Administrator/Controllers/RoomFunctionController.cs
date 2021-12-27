using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class RoomFunctionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trang tiện ích";
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            return View();
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<RoomFunction> list = db.RoomFunctions.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).OrderBy(a => a.Index).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = list, TotalRecordCount = list.Count });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(RoomFunction model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var insert = new RoomFunction
                        {
                            LanguageID = Request.Cookies["lang_client"].Value,
                            Title = model.Title,
                            Index = 0,
                            Image = model.Image,
                        };

                        db.RoomFunctions.InsertOnSubmit(insert);
                        db.SubmitChanges();
                        string message = "Thêm tiện ích thành công";
                        return Json(new { Result = "OK", Message = message, Record = model });
                    }
                    catch (Exception exception)
                    {
                        return Json(new { Result = "Error", Message = "Error: " + exception.Message });
                    }
                }
                return
                    Json(
                        new
                        {
                            Result = " Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dang"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(RoomFunction model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        RoomFunction edit = db.RoomFunctions.FirstOrDefault(b => b.ID == model.ID);
                        if (edit != null)
                        {
                            edit.Title = model.Title;
                            edit.Image = model.Image;
                            db.SubmitChanges();

                            string message = "Sửa tiện ích thành công";
                            return Json(new { Result = "OK", Message = message, Record = model });
                        }
                        return Json(new { Result = "ERROR", Message = "Tiện ích không tồn tại" });
                    }
                    catch (Exception exception)
                    {
                        return Json(new { Result = "Error", Message = "Error: " + exception.Message });
                    }
                }
                return
                    Json(
                        new
                        {
                            Result = " Error",
                            Errors = ModelState.Errors(),
                            Message = "Dữ liệu đầu vào không đúng định dạng"
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    RoomFunction del = db.RoomFunctions.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {
                        db.RoomFunctions.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa tiện ích thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Tiện ích không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<RoomFunction> records = db.RoomFunctions.ToList();
                foreach (RoomFunction record in records)
                {
                    string itemAdv = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(itemAdv, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp tiện ích thành công";
                return RedirectToAction("Index");
            }
        }
    }
}