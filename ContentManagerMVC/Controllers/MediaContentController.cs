using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentManagerMVC.Models;
using PagedList;
using System.IO;
namespace ContentManagerMVC.Controllers
{
    [Authorize(Roles = "Administrator,Manager,GraphicCreater")]
    public class MediaContentController : Controller
    {
        private PlayerDBContext db = new PlayerDBContext();

        public String TestIndex()
        {
            return "Index View";
        }


        //
        // GET: /Play/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //return View(db.Contents.ToList());
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Title desc" : "";
            ViewBag.DateSortParm = sortOrder == "Create Time" ? "Create Time desc" : "Create Time";
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var contents = from s in db.Contents
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                contents = contents.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            //IOrderedQueryable<Player> orderedPlayers;
            switch (sortOrder)
            {
                case "Name desc":
                    contents = contents.OrderByDescending(s => s.Title);
                    break;
                case "Create Time":
                    contents = contents.OrderBy(s => s.CreateDate);
                    break;
                case "Create Time desc":
                    contents = contents.OrderByDescending(s => s.CreateDate);
                    break;
                default:
                    contents = contents.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 20;

            int pageNumber = (page ?? 1);

            return View(contents.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Play/Details/5

        public ActionResult Details(int id = 0)
        {
            MediaContent mediacontent = db.Contents.Find(id);
            if (mediacontent == null)
            {
                return HttpNotFound();
            }
            return View(mediacontent);
        }

        //
        // GET: /Play/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Play/Create

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)//,MediaContent mediacontent)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                        int i = 0;
                        while (System.IO.File.Exists(path))
                        {
                            path = Path.Combine(Server.MapPath("~/App_Data/"), i+"_"+fileName);
                        }
                        file.SaveAs(path);

                        MediaContent mediacontent = new MediaContent();
                        FileInfo finfo = new FileInfo(path);
                        mediacontent.CreateDate = finfo.CreationTime;
                        mediacontent.Duration = 10;
                        mediacontent.Title = finfo.Name;
                        mediacontent.Path = path;//Path.GetFileName(path);
                        db.Contents.Add(mediacontent);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exp)
                    {

                        ModelState.AddModelError("", exp);
                    }
                }
            }

            return View();
        }

        //
        // GET: /Play/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MediaContent mediacontent = db.Contents.Find(id);
            if (mediacontent == null)
            {
                return HttpNotFound();
            }
            return View(mediacontent);
        }

        //
        // POST: /Play/Edit/5

        [HttpPost]
        public ActionResult Edit(MediaContent mediacontent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediacontent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediacontent);
        }

        //
        // GET: /Play/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MediaContent mediacontent = db.Contents.Find(id);
            if (mediacontent == null)
            {
                return HttpNotFound();
            }
            return View(mediacontent);
        }

        //
        // POST: /Play/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MediaContent mediacontent = db.Contents.Find(id);
            db.Contents.Remove(mediacontent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}