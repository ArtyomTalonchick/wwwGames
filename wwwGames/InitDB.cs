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
                var team1 = new Team
                {
                    Name = "Team1"
                };
                var team2 = new Team
                {
                    Name = "Team2"
                };
                context.Teams.AddRange(team1, team2);

                context.Users.AddRange(
                    new User
                    {
                        Name = "name1",
                        Email = "email1",
                        Password = "1q2w3e4r",
                        Team = team1
                    },
                    new User
                    {
                        Name = "name2",
                        Email = "email2",
                        Password = "1q2w3e4r",
                        Team = team1
                    },
                    new User
                    {
                        Name = "name3",
                        Email = "email3",
                        Password = "1q2w3e4r",
                        Team = team2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
