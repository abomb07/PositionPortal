/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * Position API Controller
 **/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PositionPortal.Helpers;
using PositionPortal.Models;
using PositionPortal.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private PositionBusinessService pbs;

        public PositionController(PositionBusinessService posBs)
        {
            pbs = posBs;
        }

        /// <summary>
        /// Insert position from body
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        public IActionResult Insert([FromBody] Position position)
        {
            try
            {
                // create user
                pbs.Insert(position);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Returns list of all open positions
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("names/{userid}")]
        public IActionResult FindAllCurPos(int userid)
        {
            var res = pbs.FindAllCurNames(userid);
            return Ok(res);
        }


        /// <summary>
        /// Returns list of all open stock positions
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("names/stock/{userid}")]
        public IActionResult FindAllCurStock(int userid)
        {
            var res = pbs.FindAllCurStock(userid);
            return Ok(res);
        }

        /// <summary>
        /// Returns list of all open crypto positions
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("names/crypto/{userid}")]
        public IActionResult FindAllCurCrypto(int userid)
        {
            var res = pbs.FindAllCurCrypto(userid);
            return Ok(res);
        }

        /// <summary>
        /// Returns total account balance
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("quote/{userid}")]
        public IActionResult GetTotalQuote(int userid)
        {
            var res = pbs.GetTotalDetail(userid);
            return Ok(res);
        }

        /// <summary>
        /// Returns details of a specific positions
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("quote/{userid}/{name}/{type}")]
        public IActionResult GetQuote(int userid, string name, int type)
        {
            var res = pbs.GetPosDetail(name, userid, type);
            return Ok(res);
        }
    }
}
