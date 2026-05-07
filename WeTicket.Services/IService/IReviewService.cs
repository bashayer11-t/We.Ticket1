using WeTicket.Data.Models;

namespace  WeTicket.Services.IService;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetAllAsync();
    // جلب التقييمات الخاصة بفعالية معينة
    Task<IEnumerable<Review>> GetByEventIdAsync(long eventId);
    Task<Review?> GetByIdAsync(long id);
    Task<Review> CreateAsync(Review review);
    Task<Review?> UpdateAsync(long id, Review review);
    Task<Review?> DeleteAsync(long id);
}