/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * User Business Layer
 **/

using PositionPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Models.Business
{
    public class UserBusinessService
    {
        UserDataService uds = new UserDataService();

        public User Authenticate(User user)
        {
            return uds.Authenticate(user);
        }

        public bool Register(User user)
        {
            return uds.Insert(user);
        }

        public User FindById(int Id)
        {
            return uds.FindById(Id);
        }

        public List<User> FindAll()
        {
            return uds.FindAll();
        }

        public bool UpdateUser(User user)
        {
            return uds.UpdateUser(user);
        }

        public bool DeleteUser(int Id)
        {
            return uds.DeleteUser(Id);
        }
    }
}
