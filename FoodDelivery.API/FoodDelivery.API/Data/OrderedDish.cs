using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

[PrimaryKey(nameof(Id))]
public class OrderedDish
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    [JsonIgnore]
    public virtual Order? Order { get; set; }
    
    public int DishId { get; set; }
    [JsonIgnore]
    public virtual Dish? Dish { get; set; }
}