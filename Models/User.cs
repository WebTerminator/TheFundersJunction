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

        public string PasswordHash { get; set; } = string.Empty;  // Store hashed password

        // Relationships (Navigation properties)
        public List<SavedProfile>? SavedProfiles { get; set; }
        public List<Review>? Reviews { get; set; }  // Add this line to fix the error
        public List<Connection>? Connections { get; set; }

    }
}
