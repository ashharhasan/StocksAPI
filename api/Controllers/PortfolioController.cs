using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Migrations;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PortfolioController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;

        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepo)
        {
            _userManager= userManager;
            _stockRepo = stockRepository;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPortfolio()
        {
            var username = User.GetUserName();
            var appUser= await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);
            return Ok(userPortfolio);

        }

        [HttpPost("{Symbol}")]
        [Authorize]
        public async Task<IActionResult> CreatePortfolio([FromRoute]string Symbol)
        {
            var username = User.GetUserName();
            var appUser= await _userManager.FindByNameAsync(username);

            if(appUser == null)
            {
                return Unauthorized("Can't find the current User Id");
            }

            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            if(userPortfolio.Any(x=> x.Symbol.ToLower() == Symbol.ToLower()))
            {
                return BadRequest("This stock is already available to this user");
            }

            var createdPortfolio = await _portfolioRepo.CreatePortfolioAsync(appUser.Id,Symbol);

            if(createdPortfolio == null)
            {
                return NotFound(Symbol);
            }
            return Ok("Portfolio Created");
        }

        [HttpDelete("{Symbol}")]
        [Authorize]
        public async Task<IActionResult?> DeletePortfolio ([FromRoute]string Symbol)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            if(appUser == null)
            {
                return Unauthorized("Can't find the current User Id");
            }

            var portfolio = await _portfolioRepo.DeletePortfolioAsync(appUser.Id, Symbol);

            if(portfolio == null)
            {
                return NotFound("Can't delete, Stock not found");
            }
            return Ok("Portfolio deleted");
        }
    }
}