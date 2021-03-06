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
    public class BookController : Controller
    {
        private AppDbContext db = new AppDbContext();

     [Authorize]
        // GET: /Book/
        public ActionResult Index(string option, string search, string AllBooks)
        {
            var books = from m in db.Books
                        select m;

            if (option == "Title")
            {
                return View(db.Books.Where(x => x.Title.Contains(search) || search == null).ToList());
            }
            //} else if (option == "SKU") {
            //    return View(db.Books.Where(x=>x.SKU == search || search == null).ToList());
            //}
            else if (option == "Genre")
            {
                return View(db.Books.Where(x => x.Genre.Contains(search) || search == null).ToList());
            }
            else if (option == "AuthorFirst,AuthorLast")
            {
                return View(db.Books.Where(x => x.AuthorFirst.Contains(search) || x.AuthorLast.Contains(search) || search == null).ToList());
            }
            else if (option == "AuthorFirst,AuthorLast")
            {
                return View(db.Books.Where(x => x.AuthorFirst.Contains(search) || x.AuthorLast.Contains(search) || x.Title.Contains(search) || search == null).ToList());
            }
            else if (option == null)
            {
                return View(db.Books);
            }
            if (AllBooks == null)
            {
                return View(db.Books);
            }
            return View();
        }
        //if (!String.IsNullOrEmpty(searchString))
        //{
        //    books = books.Where(s => s.Title.Contains(searchString));

        //}


        //return View(books);

        [Authorize(Roles = "Manager")]
        // GET: /Book/ManageBooks
        public ActionResult ManageBooks(string option, string search, string AllBooks)
        {
            var books = from m in db.Books
                        select m;

            if (option == "Title")
            {
                return View(db.Books.Where(x => x.Title.Contains(search) || search == null).ToList());
            }
            //} else if (option == "SKU") {
            //    return View(db.Books.Where(x=>x.SKU == search || search == null).ToList());
            //}
            else if (option == "Genre")
            {
                return View(db.Books.Where(x => x.Genre.Contains(search) || search == null).ToList());
            }
            else if (option == "AuthorFirst,AuthorLast")
            {
                return View(db.Books.Where(x => x.AuthorFirst.Contains(search) || x.AuthorLast.Contains(search) || search == null).ToList());
            }
            else if (option == "AuthorFirst,AuthorLast")
            {
                return View(db.Books.Where(x => x.AuthorFirst.Contains(search) || x.AuthorLast.Contains(search) || x.Title.Contains(search) || search == null).ToList());
            }
            else if (option == null)
            {
                return View(db.Books);
            }
            if (AllBooks == null)
            {
                return View(db.Books);
            }
            return View();
        }
        //if (!String.IsNullOrEmpty(searchString))
        //{
        //    books = books.Where(s => s.Title.Contains(searchString));

        //}


        //return View(books);
            
        

        // GET: /Book/Details/5
        public ActionResult Details(int? id)
        {
            var sum = (from a in db.Reviews
                       where a.SKU == id && a.IsApproved == true
                       select a.Rating).Sum();

            var count = (from a in db.Reviews
                         where a.SKU == id && a.IsApproved == true
                         select a.Rating).Count();

            decimal average = Convert.ToDecimal (sum) / Convert.ToDecimal( count);

            ViewBag.Ave = average.ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);

}
        [Authorize(Roles = "Manager")]
        // GET: /Book/Create
        public ActionResult Create()
        {
            return View();
        }


       [Authorize(Roles = "Manager")]
        // POST: /Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SKU,Title,AuthorFirst,AuthorLast,Genre,PublicationDate,Price,PriceLastPaid,Inventory,ReorderPoint,Discontinued")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }


        [Authorize(Roles = "Manager")]
        // GET: /Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize(Roles = "Manager")]
        // POST: /Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SKU,Title,AuthorFirst,AuthorLast,Genre,PublicationDate,Price,PriceLastPaid,Inventory,ReorderPoint,Discontinued")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: /Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
