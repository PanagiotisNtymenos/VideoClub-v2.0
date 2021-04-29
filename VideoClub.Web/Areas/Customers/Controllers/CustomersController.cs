using PagedList;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Services.Interfaces;

namespace VideoClub.Web.Areas.Customers.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class CustomersController : Controller
    {
        private readonly ILoggingService _logger;
        private readonly IRentingService _rentingService;
        private readonly IUserService _userService;

        public CustomersController(IRentingService rentingService, IUserService userService, ILoggingService logger)
        {
            _rentingService = rentingService;
            _userService = userService;
            _logger = logger;
        }

        // GET: /customers
        public async Task<ActionResult> Index(int? page)
        {
            try
            {
                // rules of paging
                var PageSize = 5;
                var PageNumber = (page ?? 1);

                var allCustomers = await _userService.GetAllCustomers();

                return View(allCustomers.ToPagedList(PageNumber, PageSize));
            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "/customers View could not be loaded.");
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
                    var PageSize = 5;
                    var PageNumber = (page ?? 1);

                    ViewBag.Customer = customer;

                    var rentings = await _rentingService.GetUserRentings(customer);
                    return View(rentings.ToPagedList(PageNumber, PageSize));
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                _logger.Writer.Fatal(e, "/customers/rentings?customer={Customer} View could not be loaded.", customer);
                return View("Error");
            }
        }
    }
}