using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.App.Models;
using backend.App.Helpers;
using System.Text;
using backend.App.Http.RequestModels;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace backend.App.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly backendContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;

        public UsersController(backendContext context, ILogger<UsersController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("password/reset"), Authorize]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest resetPassword)
        {
            try
            {
                var user = _context.User.Where(u => u.UserName == resetPassword.UserName).First();
                // check if the username is correct
                if (user == null)
                {
                    return BadRequest(new { status = false, message = "Username is not correct" });
                }

                // check if the provided password is the correct one
                if (Hashing.Compare(resetPassword.OldPassword, user.Password))
                {
                    user.Password = Hashing.HashPassword(resetPassword.NewPassword);

                    //Decrypting the Values that has been Encrypted using the old password
                    //And Encrypt it with the new password
                    user.BirthDate = Encryption.Decrypt(user.BirthDate, resetPassword.OldPassword);
                    user.Occupation = Encryption.Decrypt(user.Occupation, resetPassword.OldPassword);
                    user.Address = Encryption.Decrypt(user.Address, resetPassword.OldPassword);

                    //Encrypting with the new password
                    user.BirthDate = Encryption.Encryprt(user.BirthDate, resetPassword.NewPassword);
                    user.Occupation = Encryption.Encryprt(user.Occupation, resetPassword.NewPassword);
                    user.Address = Encryption.Encryprt(user.Address, resetPassword.NewPassword);

                    _context.User.Update(user);
                    _context.SaveChanges();
                    _logger.LogInformation("PASSWORD_UPDATED");
                    return Ok(new { message = "PASSWORD_UPDATED!", status = true });
                } else {
                    return BadRequest(new { status = false, message = "Old Password isn't correct !" });
                }
            } catch (Exception e) {
                // retrun error 500 response
                return StatusCode(500, new { message = "INTERNAL_ERROR", status = false, error_meesage = e.Message });
            }

        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(NewUserRequest request) 
        {
            try
            {
                // Check if the user name is already in use
                if(_context.User.Any(u => u.UserName == request.UserName))
                {
                    return BadRequest(new { status = false, message = "Username is already in use" });
                }

                var user = new User
                {
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    FatherName = request.FatherName,
                    FamilyName = request.FamilyName,
                    BirthDate = Encryption.Encryprt(request.BirthDate, request.BirthDate),
                    Occupation = Encryption.Encryprt(request.Occupation, request.Password),
                    Address = Encryption.Encryprt(request.Address, request.Password),
                    Password = Hashing.HashPassword(request.Password)
                };

                var newUser = _context.User.Add(user);
                _context.SaveChanges();
                return Ok(new { message = "USER_CREATED", data = newUser.Entity, status = true });
            }catch(Exception e)
            {
                return StatusCode(500, new { message = "INTERNAL_ERROR", status = false, error_meesage = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(GetUsersRequest request)
        {
            try
            {
                //Firstly we will check if the user exists 
                var user = _context.User.FirstOrDefault(u => u.UserName == request.UserName);
                if (user == null)
                {
                    return BadRequest(new { status = false, message = "The user is not exist" });
                }
                // check if the password is correct
                if (Hashing.Compare(request.Password, user.Password) == false)
                {
                    return BadRequest(new { status = false, message = "Password is not correct" });
                }

                // Decrypting the user data 
                user.BirthDate = Encryption.Decrypt(user.BirthDate, request.Password);
                user.Occupation = Encryption.Decrypt(user.Occupation, request.Password);
                user.Address = Encryption.Decrypt(user.Address, request.Password);
                _logger.LogInformation("USER_FETCHED");

                string token = CreateToken(user);
                return token;
                //return Ok(new { message = "USER_FETCHED", data = user, status = true });
            }
            catch(Exception e)
            {
                // retrun error 500 response
                return StatusCode(500, new { message = "INTERNAL_ERROR", status = false, error_meesage = e.Message });
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        } 
    }
}
