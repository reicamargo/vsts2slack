using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSTStoSlack.Models
{
    public static class UsersRepository
    {
        public static List<User> GetUsers()
        {
            var users = new List<User>();

            users.Add(new User()
            {
                VstsName = "Arya Stark(mailto:aryastark@hotmail.com)",
                SlackName = "arya"
            });
            users.Add(new User()
            {
                VstsName = "Robb Stark(mailto:robbstark@hotmail.com)",
                SlackName = "thekingofthenorth"
            });
            users.Add(new User()
            {
                VstsName = "Reinaldo Camargo(mailto:camargo.reinaldo@hotmail.com)",
                SlackName = "reinaldo"
            });

            return users;
        }

    }
}