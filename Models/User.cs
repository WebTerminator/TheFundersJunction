namespace TheFundersJunction.Models  // This should match what you reference in UserDbContext
{
    public class User
    {
        public int Id { get; set; }

        public required string Username { get; set; }  // Marked as required
        public required string Email { get; set; }     // Marked as required
    }
}
