namespace wwwGames.Models
{
    public class InvitationCode
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
