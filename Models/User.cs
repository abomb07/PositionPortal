/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * User Model
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }

        public User(int id, string email, string password, string firstName)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
        }
        public User()
        {
        }
    }
}
