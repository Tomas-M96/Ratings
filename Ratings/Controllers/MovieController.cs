using Microsoft.AspNetCore.Mvc;
using Ratings.Data;
using Ratings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ratings.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        /*----------------------------GET----------------------------*/

        public IActionResult Index()
        {
            IEnumerable<Movies> objList = _db.Movies;
            return View(objList);
        }

        /*----------------------------CREATE----------------------------*/

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost] //Tag to define that this is a post request
        [ValidateAntiForgeryToken] //Validates that this request is still valid using a token
        public IActionResult Create(Movies movie)
        {
            if (ModelState.IsValid)
            {
                _db.Movies.Add(movie);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(movie);
        }

        /*----------------------------EDIT----------------------------*/

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Movies movie = _db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        //POST - EDIT
        [HttpPost] //Tag to define that this is a post request
        [ValidateAntiForgeryToken] //Validates that this request is still valid using a token
        public IActionResult Edit(Movies movie)
        {
            if (ModelState.IsValid)
            {
                _db.Movies.Update(movie);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(movie);
        }

        /*----------------------------DELETE----------------------------*/

        //DELETE
        public IActionResult Delete(int? id)
        {
            Movies movie = _db.Movies.Find(id);


            _db.Movies.Remove(movie);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
