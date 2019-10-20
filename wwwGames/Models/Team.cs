using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wwwGames.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
