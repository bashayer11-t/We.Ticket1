using Microsoft.AspNetCore.Mvc;

using WeTicket.Data.Models;
using WeTicket.Services.IService;

namespace WeTicket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    // GET: api/Reviews
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reviews = await _reviewService.GetAllAsync();
        return Ok(reviews);
    }

    // GET: api/Reviews/Event/5
    [HttpGet("Event/{eventId}")]
    public async Task<IActionResult> GetByEvent(long eventId)
    {
        var reviews = await _reviewService.GetByEventIdAsync(eventId);
        return Ok(reviews);
    }

    // GET: api/Reviews/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        if (review == null)
            return NotFound(new { message = $"لم يتم العثور على التقييم بالرقم: {id}" });

        return Ok(review);
    }

    // POST: api/Reviews
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Review review)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _reviewService.CreateAsync(review);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Reviews/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] Review review)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _reviewService.UpdateAsync(id, review);
        if (updated == null)
            return NotFound(new { message = $"تعذر التحديث، التقييم رقم {id} غير موجود" });

        return Ok(updated);
    }

    // DELETE: api/Reviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _reviewService.DeleteAsync(id);
        if (deleted == null)
            return NotFound(new { message = $"تعذر الحذف، التقييم رقم {id} غير موجود" });

        return Ok(new { message = "تم حذف التقييم بنجاح", data = deleted });
    }
}