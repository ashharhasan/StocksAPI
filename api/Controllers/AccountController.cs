using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Register;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
             try{
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var appUser = new AppUser{
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser,registerDto.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser,"User");
                    if(roleResult.Succeeded){
                        return Ok(createdUser);
                    }
                    return StatusCode(500, roleResult.Errors);
                }
                return StatusCode (500, createdUser.Errors); 

             }
             catch(Exception ex){
                return StatusCode(500, ex);
             }
        }
    }
}