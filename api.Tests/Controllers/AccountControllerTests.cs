using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using api.Dtos.AccountDto;
using api.Interfaces;
using api.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.FakeItEasy;
using Shouldly;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace api.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _userManager = A.Fake<UserManager<AppUser>>();
            _tokenService = A.Fake<ITokenService>();
            _signInManager = A.Fake<SignInManager<AppUser>>();

            //SUT
            _accountController = new AccountController(_userManager, _tokenService, _signInManager);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginDto { UserName = "user1", Password = "Password123" };
            var appUser = new AppUser { UserName = loginDto.UserName, Email = "user1@example.com" };
            var users = new List<AppUser> { appUser}.AsQueryable();

            
            A.CallTo(() => _userManager.Users)
                .Returns(users.BuildMockDbSet());

            A.CallTo(() => _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false))
                .Returns(SignInResult.Success);

            A.CallTo(() => _tokenService.CreateToken(appUser))
                .Returns("fake-jwt-token");

            // Act
            var result = await _accountController.Login(loginDto);

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            var newUserDto = okResult.Value.ShouldBeOfType<NewUserDto>();
            newUserDto.UserName.ShouldBe(loginDto.UserName);
            newUserDto.Token.ShouldBe("fake-jwt-token");
        }
    }
}