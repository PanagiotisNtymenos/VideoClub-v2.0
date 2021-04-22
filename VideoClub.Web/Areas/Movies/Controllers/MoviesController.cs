using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClub.Core.Interfaces;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;
using VideoClub.Infrastructure.Data;
using VideoClub.Web.Areas.Movies.Models;
using System.Threading.Tasks;
using VideoClub.Common.Services;
using AutoMapper;
using VideoClub.Web.Mappings;

namespace VideoClub.Web.Areas.Movies.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
    //    private IMapper Mapper => MapperInit.Init();
        private readonly IMovieService _movieService;
        private readonly ICopyService _copyService;

        public MoviesController(IMovieService movieService, ICopyService copyService)
        {
            _movieService = movieService;
            _copyService = copyService;
        }

        // GET: /movies
        public async Task<ActionResult> Index(string q, string genre, string currentQuery, int? page)
        {
            try
            {
                // keep query for the next pages
                if (q != null)
                {
                    page = 1;
                }
                else
                {
                    q = currentQuery;
                }
                ViewBag.currentFilter = q;

                // rules of paging
                int PageSize = 3;
                int PageNumber = (page ?? 1);

                List<MovieViewModel> allMovies = new List<MovieViewModel>();
                List<Movie> movies = new List<Movie>();

                if (!String.IsNullOrEmpty(q))
                {
                    // in case of search, get only searched movies
                    movies = await _movieService.GetMoviesByQuery(q);
                }
                else
                {
                    // get every movie in DB
                    movies = await _movieService.GetAllMovies();
                }


                // filter with genre
                if (!String.IsNullOrEmpty(genre))
                {
                    if (Enum.IsDefined(typeof(Genres), genre))
                    {
                        ViewBag.currentGenre = genre;
                        List<Movie> FilteredMovies = new List<Movie>();
                        foreach (Movie movie in movies)
                        {
                            foreach (MovieGenre movieGenre in movie.MovieGenres)
                            {
                                if (movieGenre.Genre == ((int)Enum.Parse(typeof(Genres), genre)))
                                {
                                    FilteredMovies.Add(movie);
                                    break;
                                }
                            }

                        }
                        movies = FilteredMovies;
                    }
                    else
                    {
                        allMovies = allMovies.OrderBy(m => m.Movie.Title)
                                     .Take(allMovies.Count())
                                     .ToList();

                        ViewBag.Genres = new SelectList(Enum.GetNames(typeof(Genres)));
                        return View(allMovies.ToPagedList(PageNumber, PageSize));
                    }
                }


                // form ViewModel
                foreach (Movie movie in movies)
                {

                    List<Copy> availableCopies = movie.Copies.Where(c => c.IsAvailable).ToList();

                    List<Genres> genres = new List<Genres>();

                    foreach (MovieGenre mg in movie.MovieGenres)
                    {
                        genres.Add((Genres)mg.Genre);
                    }

                    allMovies.Add(new MovieViewModel(movie, genres, availableCopies.Count()));
                }

                // apply OrderBy and Take method
                allMovies = allMovies.OrderBy(m => m.Movie.Title)
                                     .Take(allMovies.Count())
                                     .ToList();

                ViewBag.Genres = new SelectList(Enum.GetNames(typeof(Genres)));
                return View(allMovies.ToPagedList(PageNumber, PageSize));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return View("Error");
            }
        }

        // Get: /movies/create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.Genres = new MultiSelectList(Enum.GetValues(typeof(Genres)));
            return View();
        }

        // POST: movies/create
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieBindModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Συμπληρώστε όλα τα στοιχεία!");
                ViewBag.Genres = new MultiSelectList(Enum.GetValues(typeof(Genres)));
                return View(model);
            }

            try
            {
                if (model.copiesNumber <= 0)
                {
                    ModelState.AddModelError("", "Δημιουργήστε τουλάχιστον μια κόπια!");
                    ViewBag.Genres = new MultiSelectList(Enum.GetValues(typeof(Genres)));
                    return View(model);
                }

                Movie movie = new Movie(model.Title, model.Summary);

                await _movieService.AddMovie(movie, model.Genres, model.copiesNumber);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                ModelState.AddModelError("", "Λάθος στοιχεία!");
                ViewBag.Genres = new MultiSelectList(Enum.GetValues(typeof(Genres)));
                return View(model);
            }

            return RedirectToAction("index", "movies");
        }

        public async Task<JsonResult> MoviesAutoComplete(string term)
        {
            List<Movie> movies = await _movieService.GetMoviesByQuery(term);
            if (movies.Count() > 0)
            {
                var moviesAndIds = new Object[movies.Count()];

                int i = 0;
                foreach (Movie movie in movies)
                {
                    moviesAndIds[i] = new
                    {
                        label = movie.Title,
                        movieId = movie.Id
                    };
                    i++;
                }
                return Json(moviesAndIds, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}