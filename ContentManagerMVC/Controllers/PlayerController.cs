﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentManagerMVC.Models;

namespace ContentManagerMVC.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerDBContext db = new PlayerDBContext();

        //
        // GET: /Player/

        public ActionResult Index()
        {
            return View(db.Players.ToList());
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