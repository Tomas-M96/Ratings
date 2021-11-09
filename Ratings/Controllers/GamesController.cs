using Microsoft.AspNetCore.Mvc;
using Ratings.Data;
using Ratings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ratings.Controllers
{
    public class GamesController : Controller
    {

        private readonly ApplicationDbContext _db;

        public GamesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Games> objList = _db.Games;
            return View(objList);
        }


        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost] //Tag to define that this is a post request
        [ValidateAntiForgeryToken] //Validates that this request is still valid using a token
        public IActionResult Create(Games game)
        {
            _db.Games.Add(game);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        /*----------------------------EDIT----------------------------*/

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Games game = _db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        //POST - EDIT
        [HttpPost] //Tag to define that this is a post request
        [ValidateAntiForgeryToken] //Validates that this request is still valid using a token
        public IActionResult Edit(Games game)
        {
            if (ModelState.IsValid)
            {
                _db.Games.Update(game);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(game);
        }

        /*----------------------------DELETE----------------------------*/

        //DELETE
        public IActionResult Delete(int? id)
        {
            Games game = _db.Games.Find(id);

            if (id == null || id == 0)
                return NotFound();

            _db.Games.Remove(game);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
