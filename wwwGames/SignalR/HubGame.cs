using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wwwGames.Models;

namespace wwwGames.SignalR
{
    [Authorize]
    public class HubGame : Hub
    {
        ContextDb db;

        public HubGame(ContextDb context)
        {
            db = context;
        }

        public async Task SendMessage(string user, string message)
        {
            User _user = db.Users.Find(int.Parse(Context.User.Identity.Name));

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        
        public async void AddPlayer(int gameId)
        {
            User user = db.Users.Find(int.Parse(Context.User.Identity.Name));
            Game game = db.Games.Include(t => t.Users).FirstOrDefault(g => g.Id == gameId);
            if (game.CurrentUserId == -1)
            {
                game.Users.Clear();
                game.CurrentUserId = user.Id;
            }
            game.Users.Add(user);
            db.SaveChanges();

            await Clients.All.SendAsync("AddPlayer", user.Id);
        }

        public async void RemovePlayer(int gameId)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == int.Parse(Context.User.Identity.Name));
            Game game = db.Games.Include(t => t.Users).FirstOrDefault(g => g.Id == gameId);
            if (game.CurrentUserId == user.Id)
            {
                List<User> users = game.Users.ToList();
                int index = users.IndexOf(user);
                int newIndex = (index + 1) % users.Count;
                while (users[newIndex].TeamId != user.TeamId)
                {
                    newIndex = (newIndex + 1) % users.Count;
                    if (newIndex == index)
                    {
                        newIndex = -1;
                        break;
                    }
                }
                game.CurrentUserId = newIndex;
            }
            game.Users.Remove(user);
            db.SaveChanges();
            await Clients.All.SendAsync("RemovePlayer", user.Id);
        }

        public async void NextStep(int gameId, string value)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == int.Parse(Context.User.Identity.Name));
            Game game = db.Games.Include(t => t.Users).FirstOrDefault(g => g.Id == gameId);
            List<User> users = game.Users.ToList();
            int index = users.IndexOf(user);
            int newIndex = index;
            while (users[newIndex].TeamId == user.TeamId)
            {
                newIndex = (newIndex + 1) % users.Count;
                if (newIndex == index)
                {
                    newIndex = -1;
                    break;
                }
            }
            game.CurrentUserId = users[newIndex].Id;
            db.SaveChanges();

            int curretnTeamId = user.TeamId == game.Team2Id ? game.Team1Id : game.Team2Id;
            await Clients.All.SendAsync("UpdateStep", game.CurrentUserId, curretnTeamId, game.Id, value);
        }
    }
}
