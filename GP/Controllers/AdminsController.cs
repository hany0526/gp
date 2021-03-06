﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.Models;

namespace GP.Controllers
{
    
    public class AdminsController : Controller
    {
        private GPContext db = new GPContext();

        //GET
        public ActionResult ListProfsssors()
        {
            return View(db.Professors.ToList());
        }

        public ActionResult AddPro()
        {
            return View();
        }

        // POST: Adminss/AddPro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPro([Bind(Include = "id,name,email,password")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professors.Add(professor);
                db.SaveChanges();
                return RedirectToAction("ListProfsssors");
            }

            return View(professor);
        }

        // GET: Professors/Delete/{{id}}
        public ActionResult DeletePro(int? id)
        {
            ProfessorsController p = new ProfessorsController();

            return p.Delete(id);
        }

        public ActionResult EditPro()
        {
            return View();
        }

        // GET: Admins/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/login
        [HttpPost]
        public ActionResult login([Bind(Include = "email,password")]  Admin admin)
        {

            var adminList = db.Admins.Where(m => m.email == admin.email && m.password == admin.password).ToList();

            if (adminList.Count != 0)
                return RedirectToAction("Index");

            else
                return RedirectToAction("login");
        }



        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
