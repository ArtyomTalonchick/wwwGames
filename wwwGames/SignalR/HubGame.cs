using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async void AddPlayer(int gameId)
        {
            User user = db.Users.Find(int.Parse(Context.User.Identity.Name));
            Game game = db.Games.Include(t => t.Users).FirstOrDefault(g => g.Id == gameId);
            if (game.CurrentUserIndex == -1)
            {
                game.PreviousUserIndex = -1;
                game.CurrentUserIndex = 0;
                game.Key = GameHelper.getNewKey();
                await Clients.All.SendAsync("UpdateStep", gameId, user, null);
            }
            else
            {
                await Clients.User(game.Users.ToList()[0].Id.ToString()).SendAsync("ShareSteps", user.Id);
            }
            game.Users.Add(user);
            db.SaveChanges();

            //await Clients.All.SendAsync("AddPlayer", user.Id);
        }

        public async void TransferSteps(int userId, List<StepDto> steps) => await Clients.User(userId.ToString()).SendAsync("UpdateSteps", steps);


        public async void RemovePlayer(int gameId)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == int.Parse(Context.User.Identity.Name));
            Game game = db.Games.Include(t => t.Users).FirstOrDefault(g => g.Id == gameId);
            if (game == null)
            {
                return;
            }

            List<User> currentUsers = game.Users.Where(u => u.TeamId == user.TeamId).OrderBy(u => u.Id).ToList();
            if (currentUsers.IndexOf(user) == game.CurrentUserIndex)
            {
                if (currentUsers.Count > 1)
                {
                    int newUserIndex = (game.CurrentUserIndex + 1) % currentUsers.Count;
                    User newUser = currentUsers[newUserIndex];
                    await Clients.All.SendAsync("UpdateStep", gameId, newUser, null);
                }
                else
                {
                    // одна команда полностью ушла --- требует решения
                    game.CurrentUserIndex = -1;
                }
            }

            game.Users.Remove(user);
            db.SaveChanges();
            //await Clients.All.SendAsync("RemovePlayer", user.Id);
        }

        public async void NextStep(int gameId, string value)
        {
            Game game = db.Games.Include(t => t.Users).FirstOrDefault(g => g.Id == gameId);
            User currentUser = db.Users.FirstOrDefault(u => u.Id == int.Parse(Context.User.Identity.Name));
            StepDto step = new StepDto { value = value, result = GameHelper.checkValue(value, game.Key) };
            if (step.result[0] == 4)
            {
                db.Games.Remove(game);
                db.SaveChanges();
                await Clients.All.SendAsync("UpdateResult", gameId, currentUser.TeamId, game.Key);
            }
            else
            {
                // нужно расмотреть вариант когда nextUsers.Count == 0
                List<User> nextUsers = game.Users.Where(u => u.TeamId != currentUser.TeamId).OrderBy(u => u.Id).ToList();
                int nextUserIndex = (game.PreviousUserIndex + 1) % nextUsers.Count;
                game.PreviousUserIndex = game.CurrentUserIndex;
                game.CurrentUserIndex = nextUserIndex;

                db.SaveChanges();

                User nextUser = nextUsers[nextUserIndex];
                await Clients.All.SendAsync("UpdateStep", gameId, nextUser, step);
            }

        }
    }

    public class StepDto
    {
        public string value { get; set; }
        public int[] result { get; set; }
    }
}
