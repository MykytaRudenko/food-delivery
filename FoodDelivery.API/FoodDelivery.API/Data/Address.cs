using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

[PrimaryKey(nameof(Id))]
public class Address
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
    public int NumberOfBuilding { get; set; }

    [JsonIgnore]
    public IEnumerable<Order> Orders = new List<Order>();

    [JsonIgnore]
    public IEnumerable<Establishment> Establishments = new List<Establishment>();
}