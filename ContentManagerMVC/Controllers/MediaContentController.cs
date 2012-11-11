using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentManagerMVC.Models;

namespace ContentManagerMVC.Controllers
{
    public class MediaContentController : Controller
    {
        private PlayerDBContext db = new PlayerDBContext();

        //
        // GET: /MediaContent/

        public ActionResult Index()
        {
            return View(db.Contents.ToList());
        }

        //
        // GET: /MediaContent/Details/5

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
        // GET: /MediaContent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MediaContent/Create

        [HttpPost]
        public ActionResult Create(MediaContent mediacontent)
        {
            if (ModelState.IsValid)
            {
                db.Contents.Add(mediacontent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mediacontent);
        }

        //
        // GET: /MediaContent/Edit/5

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
        // POST: /MediaContent/Edit/5

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
        // GET: /MediaContent/Delete/5

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
        // POST: /MediaContent/Delete/5

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