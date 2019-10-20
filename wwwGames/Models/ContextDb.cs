using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wwwGames.Models;

namespace wwwGames
{
    public class ContextDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Team { get; set; }

        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
