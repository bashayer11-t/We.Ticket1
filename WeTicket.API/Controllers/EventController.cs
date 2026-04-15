using Microsoft.AspNetCore.Mvc;

using WeTicket.Data.Models;
using WeTicket.Services.IService;

namespace WeTicket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    // GET: api/Events
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllAsync();
        return Ok(events);
    }

    // GET: api/Events/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var @event = await _eventService.GetByIdAsync(id);

        if (@event == null)
            return NotFound(new { message = $"لم يتم العثور على الفعالية بالرقم: {id}" });

        return Ok(@event);
    }

    // POST: api/Events
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Event @event)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _eventService.CreateAsync(@event);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Events/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] Event @event)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _eventService.UpdateAsync(id, @event);

        if (updated == null)
            return NotFound(new { message = $"تعذر تحديث الفعالية، الرقم {id} غير موجود" });

        return Ok(updated);
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _eventService.DeleteAsync(id);

        if (deleted == null)
            return NotFound(new { message = $"تعذر الحذف، الفعالية رقم {id} غير موجودة" });

        return Ok(new { message = "تم حذف الفعالية بنجاح", data = deleted });
    }
}