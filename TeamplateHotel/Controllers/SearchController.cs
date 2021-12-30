using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using ProjectLibrary.Config;
using ProjectLibrary.Database;
using TeamplateHotel.Models;

namespace TeamplateHotel.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult SearchLayout(string keySearch, int? page, int? pageSize)
        {
            using (var db = new MyDbDataContext())
            {
                int pagenumber = page ?? 1;
                int pagesize = pageSize ?? 9;
                ViewBag.SearchText = keySearch;
                var lan = Request.Cookies["LanguageID"].Value;
                List<ShowObject> result = new List<ShowObject>();
                Menu roomMenu = CommentController.GetMenuRoom(lan).FirstOrDefault();
                //search tour
                List<ShowObject> tours = db.Rooms.Where(a => a.Status && a.LanguageID == lan
                && (a.Title.Contains(keySearch) || a.Description.Contains(keySearch)))
                    .Select(a => new ShowObject
                    {
                        ID = a.ID,
                        Alias = a.Alias,
                        MenuAlias = roomMenu.Alias,
                        Price = a.Price,
                        Size = a.Size,
                        MaxPeople = a.MaxPeople,
                        Content = a.Content,
                        Title = a.Title,
                        Image = a.Image,
                        Description = a.Description,
                        Index = a.Index
                    }).OrderBy(a => a.Index).ToList();
                result.AddRange(tours);

                ////search article
                //List<ShowObject> articles = db.Articles.Where(a => a.Status
                //&& (a.Title.Contains(keySearch) || a.Description.Contains(keySearch)))
                //    .Join(db.Menus, a => a.MenuID, b => b.ID, (a, b) => new ShowObject
                //    {
                //        ID = a.ID,
                //        Alias = a.Alias,
                //        //MenuAlias = newsMenu.Alias,
                //        Title = a.Title,
                //        Image = a.Image,
                //        Description = a.Description,
                //        Index = a.Index
                //    }).OrderBy(a => a.Index).ToList();
                //result.AddRange(articles);

              

                //result.Add(new SearchAll
                //{
                //    Tours = tours,
                //    Articles = articles,
                //    ListHotels = listHotels,
                //});
                IPagedList<ShowObject> _list = result.ToPagedList(pagenumber, pagesize);
                return View(_list);
            }
        }
    }
}