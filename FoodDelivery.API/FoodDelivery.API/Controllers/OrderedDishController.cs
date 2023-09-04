using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderedDishController : ControllerBase
{
    private readonly ApplicationContext _context;

    public OrderedDishController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderedDish>>> GetOrderedDishes()
    {
        return Ok(await _context.OrderedDishes.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<OrderedDish>>> CreateOrderedDish(OrderedDish orderedDish)
    {
        _context.OrderedDishes.Add(orderedDish);
        await _context.SaveChangesAsync();
        return Ok(await _context.OrderedDishes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<OrderedDish>>> UpdateOrderedDish(OrderedDish orderedDish)
    {
        var dbOrderedDish = await _context.OrderedDishes.FindAsync(orderedDish.Id);
        if (dbOrderedDish == null)
        {
            return NotFound();
        }

        dbOrderedDish.DishId = orderedDish.DishId;
        dbOrderedDish.OrderId = orderedDish.OrderId;
        
        await _context.SaveChangesAsync();
        return Ok(await _context.OrderedDishes.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<OrderedDish>>> DeleteOrderedDish(int id)
    {
        var dbOrderedDish = await _context.OrderedDishes.FindAsync(id);
        if (dbOrderedDish == null)
        {
            return NotFound();
        }

        _context.OrderedDishes.Remove(dbOrderedDish);
        await _context.SaveChangesAsync();
        return Ok(await _context.OrderedDishes.ToListAsync());
    }
}