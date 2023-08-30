using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

[PrimaryKey(nameof(Id))]
public class Order
{
    public int Id { get; set; }  
    public Status Status { get; set; }
    public float TotalPrice { get; set; }
    public DateTime PlannedTime { get; set; }
    public DateTime DeliveredTime { get; set; }

    public int AddressId { get; set; }
    [JsonIgnore]
    public virtual Address? Address { get; set; }
    
    [JsonIgnore]
    public IEnumerable<OrderedDish> OrderedDishes = new List<OrderedDish>();
}