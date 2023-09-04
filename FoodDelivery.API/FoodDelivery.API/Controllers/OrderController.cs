using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ApplicationContext _context;

    public OrderController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        return Ok(await _context.Orders.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<Order>>> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Ok(await _context.Orders.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Order>>> UpdateOrder(Order order)
    {
        var dbOrder = await _context.Orders.FindAsync(order.Id);
        if (dbOrder == null)
        {
            return NotFound();
        }

        dbOrder.Status = order.Status;
        dbOrder.DeliveredTime = order.DeliveredTime;
        dbOrder.PlannedTime = order.PlannedTime;
        dbOrder.TotalPrice = order.TotalPrice;
        dbOrder.AddressId = order.AddressId;

        await _context.SaveChangesAsync();
        return Ok(await _context.Orders.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
    {
        var dbOrder = await _context.Orders.FindAsync(id);
        if (dbOrder == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(dbOrder);
        await _context.SaveChangesAsync();
        return Ok(await _context.Orders.ToListAsync());
    }
}