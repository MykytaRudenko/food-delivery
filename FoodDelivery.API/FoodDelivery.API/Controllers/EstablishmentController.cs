using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstablishmentController : ControllerBase
{
    private readonly ApplicationContext _context;

    public EstablishmentController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Establishment>>> GetEstablishments()
    {
        return Ok(await _context.Establishments.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<Establishment>>> CreateEstablishment(Establishment establishment)
    {
        _context.Establishments.Add(establishment);
        await _context.SaveChangesAsync();
        return Ok(await _context.Establishments.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Establishment>>> UpdateEstablishment(Establishment establishment)
    {
        var dbEstablishment = await _context.Establishments.FindAsync(establishment.Id);
        if (dbEstablishment == null)
        {
            return NotFound();
        }

        dbEstablishment.Title = establishment.Title;
        dbEstablishment.AddressId = establishment.AddressId;

        await _context.SaveChangesAsync();
        return Ok(await _context.Establishments.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Establishment>>> DeleteEstablishment(int id)
    {
        var dbEstablishment = await _context.Establishments.FindAsync(id);
        if (dbEstablishment == null)
        {
            return NotFound();
        }

        _context.Establishments.Remove(dbEstablishment);
        await _context.SaveChangesAsync();
        return Ok(await _context.Establishments.ToListAsync());
    }
}