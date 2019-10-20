using Microsoft.EntityFrameworkCore;
namespace wwwGames.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Scores { get; set; }


        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
