using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace wwwGames.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }

        public int GameNameId { get; set; }
        public GameName GameName { get; set; }
    }

    public class GameName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
