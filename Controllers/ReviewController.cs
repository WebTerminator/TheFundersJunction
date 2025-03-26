using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheFundersJunction.Data;
using TheFundersJunction.Models;

namespace TheFundersJunction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly UserDbContext _context;

        public ReviewController(UserDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Post a review for a connected user.
        /// </summary>
        [HttpPost("add")]
        public async Task<ActionResult<Review>> AddReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review cannot be null.");
            }

            if (review.Rating < 1 || review.Rating > 10)
            {
                return BadRequest("Rating must be between 1 and 5.");
            }

            if (string.IsNullOrWhiteSpace(review.Title) || string.IsNullOrWhiteSpace(review.Content))
            {
                return BadRequest("Title and Content are required.");
            }

            // Prevent user from reviewing themselves
            if (review.ReviewerId == review.ReviewedUserId)
            {
                return BadRequest("You cannot review your own profile.");
            }

            // Check if the reviewer and the reviewed user are connected (Standard Membership Condition)
            var connection = await _context.Connections
                                           .FirstOrDefaultAsync(c =>
                                               (c.UserId == review.ReviewerId && c.ConnectedUserId == review.ReviewedUserId && c.IsAccepted) ||
                                               (c.UserId == review.ReviewedUserId && c.ConnectedUserId == review.ReviewerId && c.IsAccepted));

            if (connection == null)
            {
                return BadRequest("You can only review users you are connected with.");
            }

            // Save the review
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }

        /// <summary>
        /// Get all reviews for a user.
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int userId)
        {
            var reviews = await _context.Reviews
                                        .Where(r => r.ReviewedUserId == userId)
                                        .ToListAsync();

            if (!reviews.Any())
            {
                return NotFound("No reviews found for this user.");
            }

            return Ok(reviews);
        }
    }
}