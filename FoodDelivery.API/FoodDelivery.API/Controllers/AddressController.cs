using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly ApplicationContext _context;

    public AddressController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Address>>> GetAddresses()
    {
        return Ok(await _context.Addresses.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<Address>>> CreateAddress(Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return Ok(await _context.Addresses.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Address>>> UpdateAddress(Address address)
    {
        var dbAddress = await _context.Addresses.FindAsync(address.Id);
        if (dbAddress == null)
        {
            return NotFound();
        }

        dbAddress.Country = address.Country;
        dbAddress.District = address.District;
        dbAddress.City = address.City;
        dbAddress.Street = address.Street;
        dbAddress.NumberOfBuilding = address.NumberOfBuilding;

        return Ok(await _context.Addresses.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Address>>> DeleteAddress(int id)
    {
        var dbAddress = await _context.Addresses.FindAsync(id);
        if (dbAddress == null)
        {
            return NotFound();
        }

        _context.Addresses.Remove(dbAddress);
        await _context.SaveChangesAsync();
        return Ok(await _context.Users.ToListAsync());
    }
}