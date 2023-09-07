using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

[PrimaryKey(nameof(Id))]
public class Establishment
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    
    public int AddressId { get; set; }
    [JsonIgnore]
    public virtual Address? Address { get; set; }

    [JsonIgnore]
    public IEnumerable<Category> Categories = new List<Category>();
}