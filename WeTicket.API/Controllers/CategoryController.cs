using Microsoft.AspNetCore.Mvc;
using WeTicket.Data.Models;
using WeTicket.Services.IService;

namespace WeTicket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    // GET: api/Categories
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound(new { message = $"لم يتم العثور على التصنيف بالرقم: {id}" });

        return Ok(category);
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _categoryService.CreateAsync(category);

        // يعيد استجابة 201 مع رابط العنصر الجديد
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _categoryService.UpdateAsync(id, category);

        if (updated == null)
            return NotFound(new { message = $"تعذر التحديث، التصنيف رقم {id} غير موجود" });

        return Ok(updated);
    }

    // DELETE: api/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _categoryService.DeleteAsync(id);

        if (deleted == null)
            return NotFound(new { message = $"تعذر الحذف، التصنيف رقم {id} غير موجود" });

        return Ok(new { message = "تم حذف التصنيف بنجاح", data = deleted });
    }
}