﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentityTemplate.Models;

namespace IdentityTemplate.Controllers
{
    [Authorize (Roles="Employee")]
    public class EmployeeReviewController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: EmployeeReview
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Book);
            return View(reviews.ToList());
        }

        // GET: EmployeeReview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: EmployeeReview/Create
        public ActionResult Create()
        {
            ViewBag.SKU = new SelectList(db.Books, "SKU", "Title");
            return View();
        }

        // POST: EmployeeReview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,SKU,CustomerReview,IsApproved,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SKU = new SelectList(db.Books, "SKU", "Title", review.SKU);
            return View(review);
        }

        // GET: EmployeeReview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.SKU = new SelectList(db.Books, "SKU", "Title", review.SKU);
            return View(review);
        }

        // POST: EmployeeReview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,SKU,CustomerReview,IsApproved,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(review).State = EntityState.Modified;
                db.Reviews.Attach(review);
                db.Entry(review).Property(r => r.IsApproved).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SKU = new SelectList(db.Books, "SKU", "Title", review.SKU);
            return View(review);
        }

        // GET: EmployeeReview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: EmployeeReview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
