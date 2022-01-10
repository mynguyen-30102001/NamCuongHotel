using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using ProjectLibrary.Utility;
using TeamplateHotel.Areas.Administrator.EntityModel;

namespace TeamplateHotel.Areas.Administrator.Controllers
{
    public class RecruitmentController : Controller
    {
        //
        // GET: /Administrator/Recruitment/
        public ActionResult Index()
        {
            ViewBag.Messages = CommentController.Messages(TempData["Messages"]);
            ViewBag.Title = "Trang quảng lý tuyển dụng";
            return View();
        }

        [HttpPost]
        public ActionResult UpdateIndex()
        {
            using (var db = new MyDbDataContext())
            {
                List<Recruitment> records = db.Recruitments.Where(r => r.LanguageID == Request.Cookies["lang_client"].Value).ToList();
                foreach (Recruitment record in records)
                {
                    string itemRoom = Request.Params[string.Format("Sort[{0}].Index", record.ID)];
                    int index;
                    int.TryParse(itemRoom, out index);
                    record.Index = index;
                    db.SubmitChanges();
                }
                TempData["Messages"] = "Sắp xếp phòng thành công";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    List<Recruitment> list = db.Recruitments.Where(a => a.LanguageID == Request.Cookies["lang_client"].Value).ToList();
                    var records = list.Select(a => new
                    {
                        a.ID,
                        a.Position,
                        a.Index,
                        a.Status,
                        a.Quantity
                    }).OrderBy(a => a.Index).Skip(jtStartIndex).Take(jtPageSize).ToList();
                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = list.Count });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm bài tuyển dụng";
            var eRecruit = new ERecruitment();
            return View(eRecruit);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ERecruitment model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Alias))
                    {
                        model.Alias = StringHelper.ConvertToAlias(model.Position);
                    }
                    try
                    {
                        var rec = new Recruitment
                        {
                            LanguageID = Request.Cookies["lang_client"].Value,
                            Position = model.Position,
                            Alias = model.Alias,
                            Desciption = model.Description,
                            Content = model.Content,
                            ExpirationDate = model.ExpirationDate,
                            Quantity = model.Quantity,
                            City = model.City,
                            Index = 0,
                            Status = model.Status,
                        };
                        db.Recruitments.InsertOnSubmit(rec);
                        db.SubmitChanges();
                        TempData["Messages"] = "Thêm bài tuyển dụng thành công.";
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Error: " + exception.Message;
                        return View(model);
                    }
                }
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            using (var db = new MyDbDataContext())
            {
                Recruitment detailRec = db.Recruitments.FirstOrDefault(a => a.ID == id);
                if (detailRec == null)
                {
                    TempData["Messages"] = "Bài tuyển dụng không tồn tại";
                    return RedirectToAction("Index");
                }
                ViewBag.Title = "Sửa bài tuyển dụng";
                var rec = new ERecruitment
                {
                    ID = detailRec.ID,
                    LanguageID = detailRec.LanguageID,
                    Content = detailRec.Content,
                    City = detailRec.City,
                    Position = detailRec.Position,
                    Quantity = detailRec.Quantity,
                    ExpirationDate = detailRec.ExpirationDate,
                    Description = detailRec.Desciption,
                    Status = detailRec.Status,
                    Index = (int)detailRec.Index,
                    Alias = detailRec.Alias,

                };
                //lấy danh sách hình ảnh
              
                return View(rec);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ERecruitment model)
        {
            using (var db = new MyDbDataContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Recruitment rec = db.Recruitments.FirstOrDefault(b => b.ID == model.ID);
                        if (rec != null)
                        {
                            rec.Alias = model.Alias;
                            rec.Content = model.Content;
                            rec.City = model.City;
                            rec.Position = model.Position;
                            rec.Quantity = model.Quantity;
                            rec.ExpirationDate = model.ExpirationDate;
                            rec.Desciption = model.Description;
                            rec.Status = model.Status;
                            rec.Index = (int)model.Index;
                            db.SubmitChanges();

                          
                            TempData["Messages"] = "Sửa phòng thành công";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception exception)
                    {
                        ViewBag.Messages = "Error: " + exception.Message;
                        return View(model);
                    }
                }
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    Recruitment del = db.Recruitments.FirstOrDefault(c => c.ID == id);
                    if (del != null)
                    {

                        db.Recruitments.DeleteOnSubmit(del);
                        db.SubmitChanges();
                        return Json(new { Result = "OK", Message = "Xóa phòng thành công" });
                    }
                    return Json(new { Result = "ERROR", Message = "Phòng không tồn tại" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }

    }
}
