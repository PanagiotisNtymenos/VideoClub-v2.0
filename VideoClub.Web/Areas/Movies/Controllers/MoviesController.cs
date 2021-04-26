using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VideoClub.Core.Interfaces;
using VideoClub.Core.Entities;
using VideoClub.Core.Enumerations;
using VideoClub.Web.Areas.Movies.Models;
using System.Threading.Tasks;
using AutoMapper;
using VideoClub.Infrastructure.Services.Interfaces;

namespace VideoClub.Web.Areas.Movies.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IMovieService _movie;
        private readonly ICopyService _copy;

        public MoviesController(IMovieService movie, ICopyService copy, IMapper mapper, ILoggingService logger)
        {
            _movie = movie;
            _copy = copy;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: /movies
        public async Task<ActionResult> Index(string q, string genre, int? page)
        {
            try
            {
                // keep query for the next pages
                if (!String.IsNullOrEmpty(q))
                    ViewBag.currentQuery = q;

                // rules of paging
                int PageSize = 5;
                int PageNumber = (page ?? 1);

                var allMovies = new List<MovieViewModel>();
                var movies = new List<Movie>();

                if (!String.IsNullOrEmpty(q))
                {
                    // in case of search, get only searched movies
                    movies = await _movie.GetAvailableMoviesByQuery(q);
                }
                else
                {
                    // get every movie in DB
                    movies = await _movie.GetAllAvailableMovies();
                }


                // filter with genre
                if (!String.IsNullOrEmpty(genre))
                {
                    if (!Enum.IsDefined(typeof(Genres), genre)) return RedirectToAction("index", "movies");

                    ViewBag.currentGenre = genre;
                    var filteredMovies = new List<Movie>();
                    foreach (var movie in movies)
                    {
                        foreach (var movieGenre in movie.MovieGenres)
                        {
                            if (movieGenre.Genre == ((int)Enum.Parse(typeof(Genres), genre)))
                            {
                                filteredMovies.Add(movie);
                                break;
                            }
                        }

                    }
                    movies = filteredMovies;
                }


                // form ViewModel
                allMovies = _mapper.Map<List<MovieViewModel>>(movies);
                foreach (var movie in allMovies)
                {
                    movie.Genres = Movie.ConvertToGenres(movie.MovieGenres);
                }

                // apply OrderBy and Take method
                allMovies = allMovies.OrderBy(m => m.Id)
                                     .Take(allMovies.Count())
                                     .ToList();

                ViewBag.Genres = new SelectList(Enum.GetNames(typeof(Genres)));
                return View(allMovies.ToPagedList(PageNumber, PageSize));
            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "/movies View could not be loaded.");
                return View("Error");
            }

        }

        // GET: /movies/create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.Genres = new MultiSelectList(Enum.GetValues(typeof(Genres)));
            return View();
        }

        // POST: /movies/create
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
                if (model.CopiesNumber <= 0)
                {
                    ModelState.AddModelError("", "Δημιουργήστε τουλάχιστον μια κόπια!");
                    ViewBag.Genres = new MultiSelectList(Enum.GetValues(typeof(Genres)));
                    return View(model);
                }

                var movie = _mapper.Map<Movie>(model);

                await _movie.AddMovie(movie, model.Genres, model.CopiesNumber);
                _logger.Writer.Information("Movie '{Title}' was created.", movie.Title);

            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "Movie could not be created.");
                return View("Error");
            }

            return RedirectToAction("index", "movies");
        }

        public async Task<JsonResult> MoviesAutoComplete(string term)
        {
            var movies = await _movie.GetMoviesByQuery(term);
            if (movies.Count() > 0)
            {
                var moviesAndIds = new Object[movies.Count()];

                int i = 0;
                foreach (var movie in movies)
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