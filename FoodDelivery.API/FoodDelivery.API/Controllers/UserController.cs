using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly ApplicationContext _context;
    
    public UserController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return Ok(await _context.Users.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<User>>> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(await _context.Users.ToListAsync());
    }
    
    [HttpPut]
    public async Task<ActionResult<List<User>>> UpdateUser(User user)
    {
        var dbUser = await _context.Users.FindAsync(user.Id);
        if (dbUser == null)
        {
            return NotFound();
        }

        dbUser.Name = user.Name;
        dbUser.Surname = user.Surname;
        dbUser.Email = user.Email;
        dbUser.Telephone = user.Telephone;
        dbUser.Password = user.Password;
        dbUser.Role = user.Role;

        await _context.SaveChangesAsync();
        return Ok(await _context.Users.ToListAsync());
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<List<User>>> DeleteUser(int id)
    {
        var dbUser = await _context.Users.FindAsync(id);
        if (dbUser == null)
        {
            return NotFound();
        }

        _context.Users.Remove(dbUser);
        await _context.SaveChangesAsync();
        return Ok(await _context.Users.ToListAsync());
    }
}