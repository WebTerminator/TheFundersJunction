namespace TheFundersJunction.Models
{
    public class Connection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ConnectedUserId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
        public User? ConnectedUser { get; set; }
    }
}
