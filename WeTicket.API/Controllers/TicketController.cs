using Microsoft.AspNetCore.Mvc;
using WeTicket.Data.Models;
using WeTicket.Services.IService;

namespace WeTicket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(ITicketService ticketService) : ControllerBase
{
    private readonly ITicketService _ticketService = ticketService;

    // GET: api/Tickets
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _ticketService.GetAllAsync();
        return Ok(tickets);
    }

    // GET: api/Tickets/User/5
    [HttpGet("User/{userId}")]
    public async Task<IActionResult> GetByUser(long userId)
    {
        var tickets = await _ticketService.GetUserTicketsAsync(userId);
        return Ok(tickets);
    }

    // GET: api/Tickets/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);
        if (ticket == null)
            return NotFound(new { message = $"لم يتم العثور على التذكرة بالرقم: {id}" });

        return Ok(ticket);
    }

    // POST: api/Tickets
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Ticket ticket)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _ticketService.CreateAsync(ticket);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Tickets/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] Ticket ticket)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _ticketService.UpdateAsync(id, ticket);
        if (updated == null)
            return NotFound(new { message = $"تعذر التحديث، التذكرة رقم {id} غير موجودة" });

        return Ok(updated);
    }

    // DELETE: api/Tickets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _ticketService.DeleteAsync(id);
        if (deleted == null)
            return NotFound(new { message = $"تعذر الحذف، التذكرة رقم {id} غير موجودة" });

        return Ok(new { message = "تم حذف التذكرة بنجاح", data = deleted });
    }
}