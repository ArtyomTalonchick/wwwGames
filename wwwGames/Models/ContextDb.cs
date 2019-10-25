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
        public DbSet<Role> Roles{ get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<InvitationCode> InvitationCodes { get; set; }

        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
