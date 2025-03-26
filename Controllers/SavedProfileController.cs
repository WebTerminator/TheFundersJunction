using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheFundersJunction.Data;
using TheFundersJunction.Models;

namespace TheFundersJunction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavedProfileController : ControllerBase
    {
        private readonly UserDbContext _context;

        public SavedProfileController(UserDbContext context)
        {
            _context = context;
        }

        // Save a profile (Create a saved profile entry)
        [HttpPost("{userId}/save/{savedUserId}")]
        public async Task<ActionResult<SavedProfile>> SaveProfile(int userId, int savedUserId)
        {
            if (userId == savedUserId)
                return BadRequest("You cannot save your own profile.");

            var savedProfile = new SavedProfile
            {
                UserId = userId,
                SavedUserId = savedUserId,
                SavedAt = DateTime.Now
            };

            _context.SavedProfiles.Add(savedProfile);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile saved successfully!" });
        }

        // Get all saved profiles for a user
        [HttpGet("{userId}/saved")]
        public async Task<ActionResult<List<SavedProfile>>> GetSavedProfiles(int userId)
        {
            var savedProfiles = await _context.SavedProfiles
                                              .Where(sp => sp.UserId == userId)
                                              .Include(sp => sp.SavedUser)
                                              .ToListAsync();

            if (savedProfiles == null || !savedProfiles.Any())
                return NotFound("No saved profiles found.");

            return Ok(savedProfiles);
        }

        // Delete a saved profile (Unsave a profile)
        [HttpDelete("{userId}/unsave/{savedProfileId}")]
        public async Task<ActionResult> UnsaveProfile(int userId, int savedProfileId)
        {
            var savedProfile = await _context.SavedProfiles
                                             .FirstOrDefaultAsync(sp => sp.SavedProfileId == savedProfileId && sp.UserId == userId);

            if (savedProfile == null)
                return NotFound("Saved profile not found.");

            _context.SavedProfiles.Remove(savedProfile);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile unsaved successfully." });
        }
    }
}
