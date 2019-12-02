using System.Collections.Generic;

namespace wwwGames.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Scores { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
