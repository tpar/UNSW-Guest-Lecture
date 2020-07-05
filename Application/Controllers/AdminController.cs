using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleMvcApp.Services;

namespace BankingApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICustomerService _customerService;

        public AdminController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var custoemrs = await _customerService.GetCustomersAsync();

            ViewData["customers"] = custoemrs;

            return View();
        }
    }
}