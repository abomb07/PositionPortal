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

        public UserController(UserBusinessService userBS, IOptions<AppSettings> appSet)
        {
            ubs = userBS;
            appSettings = appSet.Value;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = ubs.FindAll();
            return Ok(users);
        }

        /// <summary>
        /// Get specific user by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = ubs.FindById(id);
            return Ok(user);
        }

        /// <summary>
        /// Auth user by email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            User foundUser = ubs.Authenticate(user);

            if (foundUser == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            //create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, foundUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                Id = foundUser.Id,
                Email = foundUser.Email,
                FirstName = foundUser.FirstName,
                Token = tokenString
            });
        }

        /// <summary>
        /// Insert user to db
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            try
            {
                //get orig user
                var origUser = ubs.FindById(id);
                //find user with id
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ubs.DeleteUser(id);
            return Ok();
        }
    }
}
