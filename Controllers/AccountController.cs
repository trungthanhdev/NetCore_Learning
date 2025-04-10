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
                    var getRole = await _userManager.GetRolesAsync(appUser);
                    var role = getRole.FirstOrDefault();
                    if (roles.Succeeded)
                    {
                        var user = new
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            role = getRole
                        };
                        var access_token = _tokenServcie.CreateToken(appUser, role!, true);
                        var refresh_token = _tokenServcie.CreateToken(appUser, role!, false);
                        return Ok(new
                        {
                            user = user,
                            access_token = access_token,
                            refresh_token = refresh_token
                        });
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userManager.FindByNameAsync(loginDto.Username);

                if (user == null)
                {
                    return NotFound("User not found!");
                }
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "User";
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!isPasswordValid)
                    return BadRequest("Password is wrong!");

                var access_token = _tokenServcie.CreateToken(user, role, true);
                var refresh_token = _tokenServcie.CreateToken(user, role, false);
                return Ok(new
                {
                    access_token,
                    refresh_token
                });


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}