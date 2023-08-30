using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

[PrimaryKey(nameof(Id))]
public class Dish
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeSpan CookingTime { get; set; }
    public float Price { get; set; }
    
    public int CategoryId { get; set; }
    [JsonIgnore]
    public virtual Category? Category { get; set; }

    [JsonIgnore] 
    public IEnumerable<OrderedDish> OrderedDishes = new List<OrderedDish>();
}