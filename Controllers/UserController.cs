/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * User API Controller
 **/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PositionPortal.Helpers;
using PositionPortal.Models;
using PositionPortal.Models.Business;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PositionPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserBusinessService ubs;
        private readonly AppSettings appSettings;

        /// <summary>
        /// Non default constructor for DI
        /// </summary>
        /// <param name="userBS"></param>
        /// <param name="appSet"></param>
        public UserController(UserBusinessService userBS, IOptions<AppSettings> appSet)
        {
            ubs = userBS;
            appSettings = appSet.Value;
        }

        /// <summary>
        /// Get all records and return JSON array
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = ubs.FindAll();
            return Ok(users);
        }

        /// <summary>
        /// Get properties of any user with ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = ubs.FindById(id);
            return Ok(user);
        }

        /// <summary>
        /// Get email and pass from JSON body and try to login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            User foundUser = ubs.Authenticate(user);

            if (foundUser == null)
            {
                //returns a 400 bad request
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            //create bearer token using sectret phrase in appsettings.json
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //id is used to create a unique token
                    new Claim(ClaimTypes.Name, foundUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                //return http 200 with the generated token and user credentials
                Id = foundUser.Id,
                Email = foundUser.Email,
                FirstName = foundUser.FirstName,
                Token = tokenString
            });
        }

        /// <summary>
        /// Register user using JSON body
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                // create user
                ubs.Register(user);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Update user with given ID using JSON body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            try
            {
                //get orig user
                var origUser = ubs.FindById(id);
                //create new user
                //user keeps its properties if the FromBody is null
                var newUser = new User(id, user.Email ?? origUser.Email, user.Password ?? origUser.Password, user.FirstName ?? origUser.FirstName);
                // update user 
                ubs.UpdateUser(newUser);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete with ID from parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ubs.DeleteUser(id);
            return Ok();
        }
    }
}
