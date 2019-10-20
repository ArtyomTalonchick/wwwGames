using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wwwGames.Models;

namespace wwwGames
{
    public class InitDB
    {
        public static void Initialize(ContextDb context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Name = "name1",
                        Email = "email1",
                        Password = "1q2w3e4r"
                    },
                    new User
                    {
                        Name = "name2",
                        Email = "email2",
                        Password = "1q2w3e4r"
                    },
                    new User
                    {
                        Name = "name3",
                        Email = "email3",
                        Password = "1q2w3e4r"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
