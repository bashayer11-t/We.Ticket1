using Microsoft.EntityFrameworkCore;
using WeTicket.Data.Data;
using WeTicket.Data.Models;
using WeTicket.Services.IService;

namespace WeTicket.Services.Service;

public class ReviewService(AppDbContext context) : IReviewService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        // جلب التقييمات مع بيانات الفعالية والمستخدم المرتبطين بها
        return await _context.Review
            .Include(r => r.Event)
            .Include(r => r.User) // إذا كان لديكِ مودل للمستخدم
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByEventIdAsync(long eventId)
    {
        return await _context.Review
            .Where(r => r.EventId == eventId)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(long id)
    {
        return await _context.Review.FindAsync(id);
    }

    public async Task<Review> CreateAsync(Review review)
    {
     

        await _context.Review.AddAsync(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<Review?> UpdateAsync(long id, Review review)
    {
        var existingReview = await _context.Review.FindAsync(id);
        if (existingReview == null) return null;

        // تحديث محتوى التقييم والدرجة فقط
        existingReview.Comment = review.Comment;
        existingReview.Rating = review.Rating;

        await _context.SaveChangesAsync();
        return existingReview;
    }

    public async Task<Review?> DeleteAsync(long id)
    {
        var review = await _context.Review.FindAsync(id);
        if (review == null) return null;

        _context.Review.Remove(review);
        await _context.SaveChangesAsync();
        return review;
    }
}