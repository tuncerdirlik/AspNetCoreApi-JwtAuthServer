using JwtAuthServer.Core.Dtos;
using JwtAuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            //throw new CustomException("db ile ilgilli hata oluştu");

            var user = await userService.CreateUserAsync(createUserDto);

            return ActionResultInstance(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            return ActionResultInstance(user);
        }
    }
}
