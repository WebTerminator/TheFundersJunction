namespace TheFundersJunction.Models  // This should match what you reference in UserDbContext
{
    public class User
    {
        public int Id { get; set; }

        // Marked as required fields
        public required string Username { get; set; }
        public required string Email { get; set; }
        
        // Membership type (Standard for now, can be expanded)
        public string MembershipType { get; set; } = "Standard";  // Default is Standard

        // Relationships (Navigation properties)
        public List<SavedProfile> SavedProfiles { get; set; } = new List<SavedProfile>();
        // public List<Review> Reviews { get; set; } = new List<Review>();
        // public List<Connection> Connections { get; set; } = new List<Connection>();
        // public List<Message> Messages { get; set; } = new List<Message>();
        // public List<ProfileView> ProfileViews { get; set; } = new List<ProfileView>();
    }

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
