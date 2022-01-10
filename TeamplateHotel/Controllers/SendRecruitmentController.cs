using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLibrary.Config;
using ProjectLibrary.Database;

namespace TeamplateHotel.Controllers
{
    public class SendRecruitmentController : Controller
    {
        //
        // GET: /SendRecruitment/
        [HttpPost]
        public ActionResult SendRec(SendEmailRecuitment model, HttpPostedFileBase FileCV, string Position, string City)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyDbDataContext())
                {
                    if (FileCV != null)
                    {
                        string path = Server.MapPath("~/UploadFile/");
                        string File_CV = Path.GetFileName(FileCV.FileName);
                        
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        FileCV.SaveAs(path + Path.GetFileName(FileCV.FileName));
                        model.File = File_CV;
                    }
                    
                    db.SendEmailRecuitments.InsertOnSubmit(model);
                    db.SubmitChanges();

                    SendEmail sendEmail =
                        db.SendEmails.FirstOrDefault(
                            a => a.Type == TypeSendEmail.Recruitment && a.LanguageID == Request.Cookies["LanguageID"].Value);
                    Hotel hotel = CommentController.DetailHotel(Request.Cookies["LanguageID"].Value);

                    sendEmail.Title = sendEmail.Title.Replace("{NameHotel}", hotel.Name);
                    string content = sendEmail.Content;
                    content = content.Replace("{Gender}", model.Gender);
                    content = content.Replace("{FullName}", model.FullName);
                    content = content.Replace("{Tel}", model.Phone);
                    content = content.Replace("{Email}", model.Email);
                    content = content.Replace("{Position}", Position);
                    content = content.Replace("{City}", City);
                    content = content.Replace("{Link}", model.Link);
                    content = content.Replace("{Birthday}", model.Birthday.ToString());
                    content = content.Replace("{NameHotel}", hotel.Name);
                    content = content.Replace("{TelHotel}", hotel.Tel);
                    content = content.Replace("{EmailHotel}", hotel.Email);
                    content = content.Replace("{AddressHotel}", hotel.Address);
                    content = content.Replace("{Website}", hotel.Website);

                    MailHelper.SendMail(model.Email, sendEmail.Title, content);
                    MailHelper.SendMail(hotel.Email, hotel.Name + " Recruitment information of " + model.FullName, content);
                    return Redirect("/Contact/Messages?status=success");
                }
            }
            return Redirect("/Contact/Messages?status=error");
        }

        [HttpGet]
        public ActionResult Messages()
        {
            using (var db = new MyDbDataContext())
            {
                SendEmail sendEmail =
                       db.SendEmails.FirstOrDefault(
                           a => a.Type == TypeSendEmail.Recruitment && a.LanguageID == Request.Cookies["LanguageID"].Value);

                string status = Request.Params["status"];
                ViewBag.Messages = "";
                if (string.IsNullOrEmpty(status) == false)
                {
                    if (status.Equals("success"))
                    {
                        ViewBag.Messages = sendEmail.Success;
                    }
                    else
                    {
                        ViewBag.Messages = sendEmail.Error;
                    }
                }
                else
                {
                    ViewBag.Messages = sendEmail.Error;
                }
                return View();
            }
        }
    }
}
