using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoClub.Web.Areas.Movies.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Movies
        public ActionResult Index()
        {
            return View();
        }
    }
}