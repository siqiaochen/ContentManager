using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentManagerMVC.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace ContentManagerMVC.Controllers
{

    [Authorize(Roles = "Administrator")]
    public class MembershipController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Membership/

        public ActionResult Index()
        {
            List<UserMembership> members = new List<UserMembership>();
            foreach (UserProfile user in db.UserProfiles.ToList())
            { 
                UserMembership mem = new UserMembership();
                mem.User = user;
                String[] roles =Roles.GetRolesForUser(user.UserName);
                if(roles.Length > 0)
                mem.Role = roles[0];
                members.Add(mem);
            }
            return View(members);
        }

        //
        // GET: /Membership/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /Membership/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Membership/Create

        [HttpPost]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userprofile);
        }

        //
        // GET: /Membership/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            UserMembership member = new UserMembership();
            member.User = userprofile; 
            String[] roles = Roles.GetRolesForUser(userprofile.UserName);
            if (roles.Length > 0)
                member.Role = roles[0];
            List<SelectListItem> RoleItem = new List<SelectListItem>();
            RoleItem.Add(new SelectListItem
            {
                Text = "Administrator",
                Value = "Administrator"
            });
            RoleItem.Add(new SelectListItem
            {
                Text = "Manager",
                Value = "Manager",
            });
            RoleItem.Add(new SelectListItem
            {
                Text = "Graphic Creater",
                Value = "GraphicCreater"
            });
            RoleItem.Add(new SelectListItem
            {
                Text = "Banned",
                Value = "Banned"
            });
            ViewBag.RoleTypes = RoleItem;
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // POST: /Membership/Edit/5

        [HttpPost]
        public ActionResult Edit(UserMembership member, string RoleTypes)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(userprofile).State = EntityState.Modified;
                //db.SaveChanges();
                String[] oldroles = Roles.GetRolesForUser(member.User.UserName);
                if(oldroles.Length > 0)
                    Roles.RemoveUserFromRoles(member.User.UserName, oldroles);
                Roles.AddUserToRole(member.User.UserName, RoleTypes);
                return RedirectToAction("Index");
            }
            return View(member.User);
        }

        //
        // GET: /Membership/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Membership/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
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