using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SampleMvcApp.Models;
using SampleMvcApp.Services;

namespace SampleMvcApp.Controllers
{
    [Route("/balances")]
    public class BalancesController : Controller
    {

        private readonly IBalancesService _balancesService;
        private readonly ICustomerService _customerService;

        public BalancesController(IBalancesService balancesService, ICustomerService customerService)
        {
            _balancesService = balancesService;
            _customerService = customerService;
        }

        [Route("/balances")]
        public async Task<IActionResult> Index()
        {
            var auth0_id = await GetAuth0_IdAsync();
            var viewModel = await _balancesService.GetBalancesAsync(auth0_id);
            ViewData["BalanceViewModel"] = viewModel;
            return View();
        }

        [Route("/balances/id/{id}")]
        public IActionResult ById(int id)
        {
            return RedirectToAction("Index", id);
        }

        [Route("/balances/{id}")]
        public async Task<IActionResult> IndexAsync(int id)
        {
            var viewModel = await _balancesService.GetBalancesAsync(id);
            ViewData["BalanceViewModel"] = viewModel;
            return View();
        }

        private async Task<string> GetAuth0_IdAsync()
        {
            string idToken = await HttpContext.GetTokenAsync("id_token");
            var token = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            string sub = token.Claims.ToList().Where(x => x.Type == "sub").FirstOrDefault().Value;
            string auth0_id = sub.Replace("auth0|", "");
            return auth0_id;
        }
    }
}