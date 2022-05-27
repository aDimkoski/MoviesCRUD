using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesCRUD.Data;
using MoviesCRUD.Models;

namespace MoviesCRUD.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
              return _context.Movies != null ? 
                          View(await _context.Movies.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            List<Person> personsFromDb = _context.Persons.ToList();
            List<Genre> genresFromDb = _context.Genres.ToList();

            ViewBag.persons = personsFromDb;
            ViewBag.genres = genresFromDb;
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<int> persons, List<int> genres, Movie movie)
        {
            if (ModelState.IsValid)
            {
                List<Person> personsList = new List<Person>();
                foreach (int person in persons)
                {
                    Person person1 = new Person();
                    if ((person1 = _context.Persons.Find(person)) != null)
                    {
                        personsList.Add(person1);
                    }
                }
                List<Genre> genresList = new List<Genre>();
                foreach (int gen in genres)
                {
                    Genre genre = new Genre();
                    if ((genre = _context.Genres.Find(gen)) != null)
                    {
                        genresList.Add(genre);
                    }
                }
                movie.genres = genresList;
                movie.persons = personsList;
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var movieFromDb = _context.Movies.Find(id);
            List<Person> personsFromDb = _context.Persons.ToList();
            List<Genre> genresFromDb = _context.Genres.ToList();

            ViewBag.persons = personsFromDb;
            ViewBag.genres = genresFromDb;
            if (movieFromDb == null)
            {
                return NotFound();
            }

            return View(movieFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var movieFromDb = _context.Movies.Find(id);

            if (movieFromDb == null)
            {
                return NotFound();
            }

            return View(movieFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _context.Movies.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
