using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DishController: ControllerBase
{
    private readonly ApplicationContext _context;

    public DishController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Dish>>> GetDishes()
    {
        return Ok(await _context.Dishes.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<Dish>>> CreateDish(Dish dish)
    {
        _context.Dishes.Add(dish);
        await _context.SaveChangesAsync();
        return Ok(await _context.Dishes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Dish>>> UpdateDish(Dish dish)
    {
        var dbDish = await _context.Dishes.FindAsync(dish.Id);
        if (dbDish == null)
        {
            return NotFound();
        }

        dbDish.Title = dish.Title;
        dbDish.CookingTime = dish.CookingTime;
        dbDish.Price = dish.Price;
        dbDish.CategoryId = dish.CategoryId;
        await _context.SaveChangesAsync();
        return Ok(await _context.Dishes.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Dish>>> DeleteDish(int id)
    {
        var dbDish = await _context.Dishes.FindAsync(id);
        if (dbDish == null)
        {
            return NotFound();
        }

        _context.Dishes.Remove(dbDish);
        await _context.SaveChangesAsync();
        return Ok(await _context.Dishes.ToListAsync());
    }
}