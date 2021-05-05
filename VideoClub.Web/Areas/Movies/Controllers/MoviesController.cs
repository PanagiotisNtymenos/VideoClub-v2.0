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
using System.Data.Entity;
using VideoClub.Infrastructure.Models.DTOs;
using VideoClub.Infrastructure.Models;

namespace VideoClub.Web.Areas.Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IPaginationService _pagination;
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IMovieService _movie;
        private readonly ICopyService _copy;
        private readonly IOMDbAPIService _OMDbAPI;

        public MoviesController(IMovieService movie, ICopyService copy, IMapper mapper, ILoggingService logger, IPaginationService pagination, IOMDbAPIService OMDbAPI)
        {
            _movie = movie;
            _copy = copy;
            _mapper = mapper;
            _logger = logger;
            _pagination = pagination;
            _OMDbAPI = OMDbAPI;
        }

        // GET: /movies
        [AllowAnonymous]
        public async Task<ActionResult> Index(string q, string genre, int? page)
        {
            try
            {
                var movies = _movie.GetAllMoviesAsQueryable();

                // filter with query
                if (!String.IsNullOrEmpty(q))
                {
                    ViewBag.currentQuery = q;
                    movies = movies.Where(m => m.Title.Contains(q));
                }

                // filter with genre
                if (!String.IsNullOrEmpty(genre))
                {
                    if (!Enum.IsDefined(typeof(Genres), genre)) return RedirectToAction("index", "movies");

                    ViewBag.currentGenre = genre;
                    int genreValue = (int)Enum.Parse(typeof(Genres), genre);
                    movies = movies.Where(m => m.MovieGenres.Any(mg => mg.Genre == genreValue));
                }


                ViewBag.Genres = new SelectList(Enum.GetNames(typeof(Genres)));
                return View(await GetPaginatedMovies(_pagination.GetMoviesPagination(page, 5), movies));
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
                ViewBag.Genres = new MultiSelectList(Enum.GetValues(typeof(Genres)));
                return View(model);
            }

            try
            {
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

                var i = 0;
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

        private async Task<PaginationModel<MovieViewModel>> GetPaginatedMovies(PaginationDto pagination, IQueryable<Movie> movies)
        {
            var moviesCount = await movies.CountAsync();

            var toSkip = (pagination.CurrentPage - 1) * pagination.PageSize;

            movies = movies
                    .Skip(toSkip)
                    .Take(pagination.PageSize);

            var movieViewModels = _mapper.Map<List<MovieViewModel>>(movies);
            foreach (var movie in movieViewModels)
            {
                movie.Genres = Movie.ConvertToGenres(movie.MovieGenres);
                movie.Poster = await _OMDbAPI.GetMovieArtworkByTitle(movie.Title);
            }

            return new PaginationModel<MovieViewModel>(movieViewModels, pagination.CurrentPage, pagination.PageSize, moviesCount);
        }
    }
}