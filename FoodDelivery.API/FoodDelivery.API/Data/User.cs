using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;
[PrimaryKey(nameof(Id))]
public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public Role Role { get; set; }

    [JsonIgnore]
    public IEnumerable<Order> Orders = new List<Order>();
}