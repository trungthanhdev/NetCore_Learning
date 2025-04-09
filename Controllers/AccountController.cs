using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCore_Learning.Dtos.Account.request;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Models;

namespace NetCore_Learning.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenServcie;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenServcie = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //b1: tao doi tuong User
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                //b2: createUserAsync(), hash Pass va luu doi tuong vao db
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                //b3: AddToRoleAsync()
                if (createdUser.Succeeded)
                {
                    var roles = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roles.Succeeded)
                    {
                        var access_token = _tokenServcie.CreateToken(appUser, "User", true);
                        var refresh_token = _tokenServcie.CreateToken(appUser, "User", false);
                        return Ok(new { access_token = access_token, refresh_token = refresh_token });
                    }
                    else
                    {
                        return StatusCode(500, roles.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception)
            {
                // return StatusCode(500, e);
                throw;
            }
        }
    }
}