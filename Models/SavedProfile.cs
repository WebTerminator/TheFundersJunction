namespace TheFundersJunction.Models  // This should match what you reference in UserDbContext
{
    // Define related entities to keep relationships clear:
    public class SavedProfile
    {
        public int SavedProfileId { get; set; }
        public int UserId { get; set; }
        public int SavedUserId { get; set; }
        public DateTime SavedAt { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public User? SavedUser { get; set; }
    }
}
