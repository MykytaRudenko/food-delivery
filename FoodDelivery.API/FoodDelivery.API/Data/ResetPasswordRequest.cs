﻿using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.API.Data;

public class ResetPasswordRequest
{
    [Required]
    public string Token { get; set; } = String.Empty;
    
    [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = String.Empty;
}