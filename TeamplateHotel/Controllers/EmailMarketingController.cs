using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using ProjectLibrary.Config;
using ProjectLibrary.Database;


namespace TeamplateHotel.Controllers
{
    public class EmailMarketingController : Controller
    {
        //
        // GET: /EmailMarketing/
        [HttpPost]
        public JsonResult SaveEmail(string emailMarketing)
        {
            try
            {
                using (var db = new MyDbDataContext())
                {
                    EmailMarketing checkEmail = new EmailMarketing();
                    if (db.EmailMarketings.ToList().Count > 0)
                    {
                        checkEmail = db.EmailMarketings.FirstOrDefault(a => a.Email == emailMarketing);
                    }

                    if (checkEmail != null && checkEmail.Email != null)
                    {
                        return Json(new { success = false });
                        //return Redirect("/Contact/Messages?status=" + status);
                    }
                    EmailMarketing marketing = new EmailMarketing
                    {
                        Email = emailMarketing,
                    };
                    db.EmailMarketings.InsertOnSubmit(marketing);
                    db.SubmitChanges();
                    //return Redirect("/Contact/Messages?status=" + status); 
                    return Json(new { success = true });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, Request = "This Email already exits" });
            }
        }


    }
}
