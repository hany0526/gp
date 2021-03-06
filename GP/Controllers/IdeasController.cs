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
    public class IdeasController : Controller
    {
        private GPContext db = new GPContext();

        // GET: Ideas
        public ActionResult Index()
        {
            var ideas = db.Ideas.Include(i => i.leader).Include(i => i.professor1).Include(i => i.professor2).Include(i => i.professor3);
            return View(ideas.ToList());
        }

        // GET: Ideas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // GET: Ideas/Create
        public ActionResult Create()
        {
            ViewBag.leaderid = new SelectList(db.Students, "id", "name");
            ViewBag.professor1id = new SelectList(db.Professors, "id", "name");
            ViewBag.professor2id = new SelectList(db.Professors, "id", "name");
            ViewBag.professor3id = new SelectList(db.Professors, "id", "name");
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,leaderid,name,description,tools,professor1id,professor2id,professor3id,isApproved")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                db.Ideas.Add(idea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.leaderid = new SelectList(db.Students, "id", "name", idea.leaderid);
            ViewBag.professor1id = new SelectList(db.Professors, "id", "name", idea.professor1id);
            ViewBag.professor2id = new SelectList(db.Professors, "id", "name", idea.professor2id);
            ViewBag.professor3id = new SelectList(db.Professors, "id", "name", idea.professor3id);
            return View(idea);
        }

        // GET: Ideas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            ViewBag.leaderid = new SelectList(db.Students, "id", "name", idea.leaderid);
            ViewBag.professor1id = new SelectList(db.Professors, "id", "name", idea.professor1id);
            ViewBag.professor2id = new SelectList(db.Professors, "id", "name", idea.professor2id);
            ViewBag.professor3id = new SelectList(db.Professors, "id", "name", idea.professor3id);
            return View(idea);
        }

        // POST: Ideas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,leaderid,name,description,tools,professor1id,professor2id,professor3id,isApproved")] Idea idea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(idea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.leaderid = new SelectList(db.Students, "id", "name", idea.leaderid);
            ViewBag.professor1id = new SelectList(db.Professors, "id", "name", idea.professor1id);
            ViewBag.professor2id = new SelectList(db.Professors, "id", "name", idea.professor2id);
            ViewBag.professor3id = new SelectList(db.Professors, "id", "name", idea.professor3id);
            return View(idea);
        }

        // GET: Ideas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Idea idea = db.Ideas.Find(id);
            if (idea == null)
            {
                return HttpNotFound();
            }
            return View(idea);
        }

        // POST: Ideas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Idea idea = db.Ideas.Find(id);
            db.Ideas.Remove(idea);
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
