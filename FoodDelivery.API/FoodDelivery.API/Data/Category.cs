﻿using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data;

[PrimaryKey(nameof(Id))]
public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;

    public int EstablishmentId { get; set; }
    [JsonIgnore]
    public virtual Establishment? Establishment { get; set; }

    [JsonIgnore]
    public IEnumerable<Dish> Dishes = new List<Dish>();
}