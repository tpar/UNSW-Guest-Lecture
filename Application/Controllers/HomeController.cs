using System;
using System.Globalization;
using System.Threading.Tasks;
using BankingApp.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleMvcApp.Services;

namespace SampleMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService) 
        {
            _customerService = customerService;
        }

        [Route("/")]

        public async Task<IActionResult> Index()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                string idToken = await HttpContext.GetTokenAsync("id_token");
                ViewBag.Name = (await _customerService.GetAsync(TokenHelper.ExtractAuth0_Id(idToken))).FullName;

            }
            ViewBag.IsAuthenticated = isAuthenticated;
            return View();
        }

        [Authorize]
        [Route("/home/showBalances")]
        public async Task<IActionResult> ShowBalancesAsync()
        {
            string idToken = await HttpContext.GetTokenAsync("id_token");
            var user = await _customerService.GetAsync(TokenHelper.ExtractAuth0_Id(idToken));

            //Navigate to "/balances/{id}"
            return RedirectToAction("Index", "Balances", new
            {
                Id = user.Id
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
