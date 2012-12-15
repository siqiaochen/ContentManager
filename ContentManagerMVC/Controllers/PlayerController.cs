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
    public class PlayerController : Controller
    {
        private PlayerDBContext db = new PlayerDBContext();

        //
        // GET: /Player/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.IPSortParm = sortOrder == "IP" ? "IP desc" : "IP";
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var players = from s in db.Players
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                players = players.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            //IOrderedQueryable<Player> orderedPlayers;
            switch (sortOrder)
            {
                case "Name desc":
                    players = players.OrderByDescending(s => s.Name);
                    break;
                case "IP":
                    players = players.OrderBy(s => s.IP);
                    break;
                case "IP desc":
                    players = players.OrderByDescending(s => s.IP);
                    break;
                default:
                    players = players.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 20;
            
            int pageNumber = (page ?? 1);

            return View(players.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Player/Details/5

        public ActionResult Details(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }
        // GET: /Player/Details/5

        public ActionResult GeneratePassword(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player != null)
            {
                Random random = new Random((int )(DateTime.Now.Ticks/10000000));
                player.Password = string.Empty;
                for (int i = 0; i < 9; i++)
                    player.Password += random.Next(0, 9).ToString();
                db.SaveChanges();
            }
            else 
            { }
            return RedirectToAction("Index");
        }

        //
        // GET: /Player/Details/5

        public ActionResult PlayerScheduleDetails(int? playerid, string option, int? scheduleid)
        {
            ViewBag.PlayerId = playerid;
            Player player = db.Players.Find(playerid);
            
            if (player == null)
            {
                return HttpNotFound();
            }

            switch (option)
            {
                case "add":
                    {
                        PlayerSchedule psche = new PlayerSchedule();
                        psche.schedule = db.Schedules.Find(scheduleid);
                        if(psche.schedule!=null)
                        {
                            db.PlayerSchedules.Add(psche);
                            player.Schedules.Add(psche);
                        }
                    }
                    db.SaveChanges();
                    break;
                case "delete":
                    player.Schedules.Remove(db.PlayerSchedules.Find(scheduleid));
                    db.PlayerSchedules.Remove(db.PlayerSchedules.Find(scheduleid));
                    db.SaveChanges();
                    break;
                default:
                    break;
            }
            var playerschedules = from s in player.Schedules
                            select s;

            return View(playerschedules);
        }
        public ActionResult PlayerAddSchedule(int playerid)
        {

            ViewBag.PlayerId = playerid;
            Player player = db.Players.Find(playerid);
            if (player == null)
            {
                return HttpNotFound();
            }
            var schedules = from s in db.Schedules
                            select s;
            return View(schedules);
        }
        //
        // GET: /Player/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Player/Create

        [HttpPost]
        public ActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        //
        // GET: /Player/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        //
        // POST: /Player/Edit/5

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        //
        // GET: /Player/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        //
        // POST: /Player/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            while (player.Schedules.Count() > 0)
            {
                db.PlayerSchedules.Remove(player.Schedules.First());
                //player.Schedules.Remove(player.Schedules.First());                
            }
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SearchIndex(string searchString)
        { 
            var players = from m in db.Players select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                players = players.Where(s => s.Name.Contains(searchString));
            }
            return View(players);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}