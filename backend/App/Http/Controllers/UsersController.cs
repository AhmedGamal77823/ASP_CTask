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

namespace backend.App.Http.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly backendContext _context;

        public UsersController(backendContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> NewUser(NewUserRequest request)
        {
            try
            {
                // check if the username is already in use
                if (_context.User.Any(u => u.UserName == request.UserName))
                {
                    return BadRequest(new { status = false, message = "Username is already in use" });
                }
                // create a new user
                var user = new User
                {
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    FamilyName = request.FamilyName,
                    FatherName = request.FatherName,
                    Password = Hashing.HashPassword(request.Password),
                    Address = Encryption.Encryprt(request.Address, request.Password),
                    Occupation = Encryption.Encryprt(request.Occupation, request.Password),
                    BirthDate = Encryption.Encryprt(request.BirthDate, request.Password),
                };
                // add the user to the database
                var new_user = _context.User.Add(user);
                _context.SaveChanges();
                return Ok(new { message = "USER_CREATED", data = new_user.Entity, status = true });
            }
            catch (Exception e)
            {
                // retrun error 500 response
                return StatusCode(500, new { message = "INTERNAL_ERROR", status = false, error_meesage = e.Message });
            }
        }

        [HttpPost]
        [Route("password/reset")]
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
                    return Ok(new { message = "PASSWORD_UPDATED!", status = true });
                } else {
                    return BadRequest(new { status = false, message = "Old Password isn't correct !" });
                }
            } catch (Exception e) {
                // retrun error 500 response
                return StatusCode(500, new { message = "INTERNAL_ERROR", status = false, error_meesage = e.Message });
            }

        }

        [HttpPost]
        [Route("getData")]
        public async Task<ActionResult> GetUser(GetUsersRequest request)
        {
            try
            {
                var user = _context.User.FirstOrDefault(u => u.UserName == request.UserName);             
                // check if the username is correct
                if (user == null) {
                   return BadRequest(new { status = false, message = "Username is not correct" });
               }
                // check if the username is correct
                if (Hashing.Compare(request.Password, user.Password) == false)
                {
                    return BadRequest(new { status = false, message = "Password is not correct" });
                }

                // Then we will decrypt the personal data 
                user.BirthDate = Encryption.Decrypt(user.BirthDate, request.Password);
                user.Occupation = Encryption.Decrypt(user.Occupation, request.Password);
                user.Address = Encryption.Decrypt(user.Address, request.Password);

                return Ok(new { message = "USER_FETCHED", data = user, status = true });
            }
            catch (Exception e)
            {
                // retrun error 500 response
                return StatusCode(500, new { message = "INTERNAL_ERROR", status = false, error_meesage = e.Message });
            }
        }
    }
}
