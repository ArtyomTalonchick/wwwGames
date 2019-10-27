using System.Linq;
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
                    Name = "Team1",
                    UserId = 1
                };
                var team2 = new Team
                {
                    Name = "Team2",
                    UserId = 3
                };
                context.Teams.AddRange(team1, team2);

                var role1 = new Role
                {
                    Name = "teamLead"
                };
                var role2 = new Role
                {
                    Name = "player"
                };
                context.Roles.AddRange(role1, role2);

                context.Users.AddRange(
                    new User
                    {
                        Name = "name1",
                        Email = "email1",
                        Password = "1q2w3e4r",
                        Role = role1,
                        Team = team1
                    },
                    new User
                    {
                        Name = "name2",
                        Email = "email2",
                        Password = "1q2w3e4r",
                        Role = role2,
                        Team = team1
                    },
                    new User
                    {
                        Name = "name3",
                        Email = "email3",
                        Password = "1q2w3e4r",
                        Role = role1,
                        Team = team2
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
