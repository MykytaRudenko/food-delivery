using System.Security.Cryptography;
using FoodDelivery.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Controllers;
//TODO: delete CreateUser method
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly ApplicationContext _context;
    
    public UserController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
        {
            return BadRequest("User already exists.");
        }

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            Email = request.Email,
            Telephone = request.Telephone,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            VerificationToken = CreateRandomToken()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User successfully created!");
    }
    
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Password is incorrect.");
        }
        
        if (user.VerifiedAt == null)
        {
            return BadRequest("Not verified!");
        }

        return Ok($"Welcome back, {user.Name} {user.Surname}!");
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using(var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
    
    [HttpPost("verify")]
    public async Task<IActionResult> Verify(string token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
        if (user == null)
        {
            return BadRequest("Invalid token.");
        }

        user.VerifiedAt = DateTime.Now;
        await _context.SaveChangesAsync();

        return Ok("User verified!");
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        user.PasswordResetToken = CreateRandomToken();
        user.ResetTokenExpires = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();

        return Ok("You may now reset your password.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
        if (user == null || user.ResetTokenExpires < DateTime.Now)
        {
            return BadRequest("Invalid token.");
        }
        
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.PasswordResetToken = null;
        user.ResetTokenExpires = null;
        
        await _context.SaveChangesAsync();

        return Ok("Password successfully reset.");
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