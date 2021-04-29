using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Services.Interfaces;
using VideoClub.Web.Areas.Rentings.Models;

namespace VideoClub.Web.Areas.Rentings.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RentingsController : Controller
    {
        private readonly ILoggingService _logger;
        private readonly IMapper _mapper;
        private readonly IRentingService _rentingService;
        private readonly ICopyService _copyService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;

        public RentingsController(IRentingService rentingService, ICopyService copyService, IUserService userService, IMovieService movieService, IMapper mapper, ILoggingService logger)
        {
            _rentingService = rentingService;
            _copyService = copyService;
            _userService = userService;
            _movieService = movieService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: /rentings
        public async Task<ActionResult> Index(int? page)
        {
            try
            {
                // rules of paging
                var PageSize = 5;
                var PageNumber = (page ?? 1);

                var allRentings = await _rentingService.GetAllActiveRentings();

                var rentingViewModels = _mapper.Map<List<RentingViewModel>>(allRentings);

                return View(rentingViewModels.ToPagedList(PageNumber, PageSize));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return View("Error");
            }
        }

        // GET: /rentings/create
        public async Task<ActionResult> Create(string customer, int? movieId)
        {
            try
            {
                string title = null;
                string username = null;

                if (movieId != null)
                {
                    var copy = await _copyService.GetAvailableCopyById((int)movieId);
                    if (copy != null) title = copy.Movie.Title;
                }

                if (!String.IsNullOrEmpty(customer))
                {
                    var user = _userService.GetUserByUserName(customer);
                    if (user != null) username = user.UserName;
                }
                // TODO mapping(?)
                var rentingBindModel = new RentingBindModel(username, title, movieId);

                return View(rentingBindModel);
            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "/rentings/create View could not be loaded.");
                return View("Error");
            }
        }

        // POST: /rentings/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RentingBindModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Username = null; model.Title = null; model.MovieId = null;
                return View(model);
            }
            try
            {
                var user = _userService.GetUserByUserName(model.Username);
                var copy = await _copyService.GetAvailableCopyById((int)model.MovieId);

                if (user == null)
                {
                    ModelState.AddModelError("", "Δεν υπάρχει αυτός ο πελάτης!");
                    model.Username = null;
                    return View(model);
                }

                if (copy == null)
                {
                    ModelState.AddModelError("", "Δεν υπάρχει αυτή η ταινία!");
                    model.Title = null; model.MovieId = 0;
                    return View(model);
                }

                var renting = _mapper.Map<Renting>(model);
                renting.RentingDate = DateTime.UtcNow;
                renting.ScheduledReturnDate = DateTime.UtcNow.AddDays(7);
                renting.ReturnDate = DateTime.UtcNow.AddDays(7);
                renting.IsActive = true;

                await _rentingService.AddRenting(renting, user, copy);
                _logger.Writer.Information("Renting was created for User: {Username} and Movie: {Title} with Copy: {CopyID}", user.UserName, copy.Movie.Title, copy.Id);
            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "Renting could not be created.");
                model.Username = null; model.Title = null; model.MovieId = 0;
                return View(model);
            }

            return RedirectToAction("index", "rentings");
        }


        // GET: /rentings/return?returnId
        public async Task<ActionResult> Return(int rentingId)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            try
            {
                var renting = await _rentingService.GetRentingById(rentingId);
                var returnRentingBindModel = _mapper.Map<ReturnRentingBindModel>(renting);
                if (renting.IsActive)
                    return View(returnRentingBindModel);

            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "/rentings/return?returnId={RentingID} View could not be loaded.", rentingId);

            }
            return View("Error");
        }

        // POST: /rentings/return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Return(ReturnRentingBindModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var renting = _mapper.Map<Renting>(model);
                renting.IsActive = false;
                renting.ReturnNotes = model.ReturnNotes;
                renting.ReturnDate = DateTime.UtcNow;
                renting.Copy.IsAvailable = true;

                await _rentingService.ReturnRenting(renting);
                _logger.Writer.Information("Renting: {RentingID} with User: {Username}, Copy: {CopyID} and Movie: {Title} was returned.", renting.Id, renting.User.UserName, renting.Copy.Id, renting.Copy.Movie.Title);
            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "Return didn't complete.");
                return View(model);
            }

            return RedirectToAction("index", "rentings");
        }

        public async Task<JsonResult> MoviesAutoComplete(string term)
        {
            var movies = await _movieService.GetAvailableMoviesByQuery(term);

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

        public async Task<JsonResult> UsersAutoComplete(string term)
        {
            var users = await _userService.GetUserNameByQuery(term);

            return Json(users, JsonRequestBehavior.AllowGet);
        }

    }
}