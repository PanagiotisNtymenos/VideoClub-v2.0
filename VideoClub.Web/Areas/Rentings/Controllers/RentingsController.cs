using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using System.Diagnostics;
using VideoClub.Web.Areas.Rentings.Models;

namespace VideoClub.Web.Areas.Rentings.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RentingsController : Controller
    {
        private readonly IRentingService _rentingService;
        private readonly ICopyService _copyService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;

        public RentingsController(IRentingService rentingService, ICopyService copyService, IUserService userService, IMovieService movieService)
        {
            _rentingService = rentingService;
            _copyService = copyService;
            _userService = userService;
            _movieService = movieService;
        }

        // GET: /rentings
        public async Task<ActionResult> Index(int? page)
        {
            try
            {
                // rules of paging
                int PageSize = 3;
                int PageNumber = (page ?? 1);

                List<Renting> allRentings = await _rentingService.GetAllActiveRentings();

                return View(allRentings.ToPagedList(PageNumber, PageSize));
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
                try
                {
                    if (movieId != null)
                    {
                        Copy copy = await _copyService.GetAvailableCopyById((int)movieId);
                        if (copy != null) title = copy.Movie.Title;
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    //return View("Error");
                }

                try
                {
                    User user = _userService.GetUserByUserName(customer);
                    if (user != null) username = user.UserName;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    //return View("Error");
                }

                RentingBindModel rentingBindModel = new RentingBindModel(username, title, movieId);

                return View(rentingBindModel);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
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
                ModelState.AddModelError("", "Συμπληρώστε όλα τα απαραίτητα πεδία!");
                model.Username = null;
                model.Title = null;
                model.MovieId = 0;
                return View(model);
            }
            try
            {
                User user;
                Copy copy;
                try
                {
                    user = _userService.GetUserByUserName(model.Username);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    ModelState.AddModelError("", "Δεν υπάρχει αυτός ο πελάτης!");
                    model.Username = null;
                    return View(model);
                }

                try
                {
                    copy = await _copyService.GetAvailableCopyById((int)model.MovieId);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    ModelState.AddModelError("", "Δεν υπάρχει αυτή η ταινία!");
                    model.Title = null;
                    model.MovieId = 0;
                    return View(model);
                }

                Renting renting = new Renting(model.RentingNotes);

                await _rentingService.AddRenting(renting, user, copy);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                ModelState.AddModelError("", "Λάθος στοιχεία!");
                model.Username = null;
                model.Title = null;
                model.MovieId = 0;
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

                Renting renting = await _rentingService.GetRentingById(rentingId);

                if (renting.IsActive)
                    return View(renting);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return View("Error");
            }
            return View("Error");
        }

        // POST: /rentings/return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Return(Renting model)
        {
            try
            {
                Renting renting;
                try
                {
                    renting = await _rentingService.GetRentingById(model.Id);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    ModelState.AddModelError("", "Δεν υπάρχει αυτή η κράτηση!");
                    return View(model);
                }

                renting.IsActive = false;
                renting.ReturnNotes = model.ReturnNotes;
                renting.ReturnDate = DateTime.Now;
                renting.Copy.IsAvailable = true;

                await _rentingService.ReturnRenting(renting);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                ModelState.AddModelError("", "Λάθος στοιχεία!");
                return View(model);
            }

            return RedirectToAction("index", "rentings");
        }

        public async Task<JsonResult> MoviesAutoComplete(string term)
        {
            List<Movie> movies = await _movieService.GetAvailableMoviesByQuery(term);

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
            List<string> users = await _userService.GetUserNameByQuery(term);

            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}