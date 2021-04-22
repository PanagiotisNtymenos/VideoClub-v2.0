using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;

namespace VideoClub.Web.Areas.Customers.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class CustomersController : Controller
    {
        private readonly IRentingService _rentingService;
        private readonly IUserService _userService;

        public CustomersController(IRentingService rentingService, IUserService userService)
        {
            _rentingService = rentingService;
            _userService = userService;
        }

        // GET: /customers
        public async Task<ActionResult> Index(int? page)
        {
            try
            {
                // rules of paging
                int PageSize = 5;
                int PageNumber = (page ?? 1);

                List<User> allCustomers = await _userService.GetAllCustomers();

                return View(allCustomers.ToPagedList(PageNumber, PageSize));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return View("Error");
            }
        }

        // GET: /customers/rentings?customer
        public async Task<ActionResult> Rentings(string customer, int? page)
        {
            try
            {
                if (!String.IsNullOrEmpty(customer))
                {
                    // rules of paging
                    int PageSize = 5;
                    int PageNumber = (page ?? 1);

                    ViewBag.Customer = customer;

                    List<Renting> rentings = await _rentingService.GetUserRentings(customer);
                    return View(rentings.ToPagedList(PageNumber, PageSize));
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return View("Error");
            }
        }
    }
}