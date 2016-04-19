﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Movies
        public ActionResult Index(string movieGenre, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            //Create a LINQ query to select the movies
            var movies = from m in db.Movies
                         select m;

            //run query against DB
            if (!String.IsNullOrEmpty(searchString))
            {
                // s=>s.name is a Lambda Expression
                // LINQ queries are not executed when they are defined or when they are modified by calling a method such as Where or OrderBy. Instead, query execution is deferred, which means that the evaluation of an expression is delayed until its realized value is actually iterated over or the "ToList" method is called.
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies);

            //Search using URL query method
            //http://localhost:xxxx/SpartanCompany/Index?searchString=Car

            //return View(db.Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //"All the methods that create, edit, delete, or otherwise modify data do so in the HttpPost overload of the method."

        /*Calls ModelState.IsValid to check whether the movie has any validation errors 
        * Calling this method evaluates any validation attributes that have been applied  to the object
        * If there are no errors, the method saves the new  movie in the database. In our movie example,
        * the form is not posted  to the server when there are validation errors detected on the client side; 
        * the second Create method is never called.
        */
        [HttpPost] 
        
        [ValidateAntiForgeryToken] //Validates XSRF token generated by the @Html.AntiForgeryToken() all in the view

        //ASP.Net MVC model binder takes the posted form values and creats a SpartanCompany obj that's pased as the spartanCompany parameter
        //View (user input) -> Controller (stored in temp spartanCompany instance through Bind) -> Model (saved in DB)
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid) //Verify that the data submitted in the form can be used to modify (edit or update) a Movie object
            {
                db.Movies.Add(movie);  //add a new spartanCompany obj
                db.SaveChanges(); //save the DB
                return RedirectToAction("Index"); //redirects to the INDEX, in this case you see the results on the index page
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        /*Gives the ActionResult the ActionName "Delete" in order to perform the corrct mapping for the routing system.
        * This is needed because overloaded methods require unique parameters in CLR 
        * however we want two ActionResults (One for POST and one for GET) that use the same parameter count and type i.e. just one int
        */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
