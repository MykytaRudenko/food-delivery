using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

[PrimaryKey(nameof(Id))]
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    [JsonIgnore]
    public IEnumerable<Order> Orders = new List<Order>();
}