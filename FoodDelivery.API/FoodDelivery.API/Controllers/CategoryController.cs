using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ApplicationContext _context;

    public CategoryController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        return Ok(await _context.Categories.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Category>>> UpdateCategory(Category category)
    {
        var dbCategory = await _context.Categories.FindAsync(category.Id);
        if (dbCategory == null)
        {
            return NotFound();
        }

        dbCategory.Title = category.Title;
        dbCategory.EstablishmentId = category.EstablishmentId;

        await _context.SaveChangesAsync();
        return Ok(await _context.Categories.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<Category>>> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok(await _context.Categories.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Category>>> DeleteCategory(int id)
    {
        var dbCategory = await _context.Categories.FindAsync(id);
        if (dbCategory == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(dbCategory);
        await _context.SaveChangesAsync();
        return Ok(await _context.Categories.ToListAsync());
    }
}