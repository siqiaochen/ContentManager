using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentManagerMVC.Models;
using PagedList;

namespace ContentManagerMVC.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class ScheduleController : Controller
    {
        private PlayerDBContext db = new PlayerDBContext();

        //
        // GET: /Schedule/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //return View(db.Contents.ToList());
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.StartTimeSortParm = sortOrder == "Start Time" ? "Start Time desc" : "Start Time";
            ViewBag.EndTimeSortParm = sortOrder == "End Time" ? "End Time desc" : "End Time";
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var schedules = from s in db.Schedules
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                schedules = schedules.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            //IOrderedQueryable<Player> orderedPlayers;
            switch (sortOrder)
            {
                case "Name desc":
                    schedules = schedules.OrderByDescending(s => s.Title);
                    break;
                case "Create Time":
                    schedules = schedules.OrderBy(s => s.StartTime);
                    break;
                case "Create Time desc":
                    schedules = schedules.OrderByDescending(s => s.StartTime);
                    break;
                case "End Time":
                    schedules = schedules.OrderBy(s => s.EndTime);
                    break;
                case "End Time desc":
                    schedules = schedules.OrderByDescending(s => s.EndTime);
                    break;
                default:
                    schedules = schedules.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 20;

            int pageNumber = (page ?? 1);

            return View(schedules.ToPagedList(pageNumber, pageSize));
        }
        //
        // GET: /Schedule/Details/5

        public ActionResult Details(int id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // GET: /Schedule/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ScheduledItemDetails(int scheduleid,string option, int? itemid)
        {
            ViewBag.ScheduleId = scheduleid;
            Schedule sche = db.Schedules.Find(scheduleid);
            ViewBag.ItemId = itemid;

            if (sche == null)
            {
                return HttpNotFound();
            }

            switch (option)
            {
                case "add":
                    {
                        ScheduledItem scheitem = new ScheduledItem();
                        scheitem.Content = db.Contents.Find(itemid);
                        if(scheitem.Content != null)
                        {
                            db.ScheduledItems.Add(scheitem);
                            sche.Contents.Add(scheitem);
                            db.SaveChanges();
                        }
                    };
                    break;
                case "delete":
                    sche.Contents.Remove(db.ScheduledItems.Find(itemid));
                    db.SaveChanges();
                    break;
                default:
                    break;
            }
            var scheduledItems = from s in sche.Contents
                                  select s;

            return View(scheduledItems);
        }

        public ActionResult ScheduleAddContent(int scheduleid)
        {
            ViewBag.ScheduleId = scheduleid;
            Schedule item = db.Schedules.Find(scheduleid);
            if (item == null)
            {
                return HttpNotFound();
            }
            var contents = from s in db.Contents
                            select s;
            return View(contents);
        }
        //
        // POST: /Schedule/Create

        [HttpPost]
        public ActionResult Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedule);
        }

        //
        // GET: /Schedule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // POST: /Schedule/Edit/5

        [HttpPost]
        public ActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        //
        // GET: /Schedule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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