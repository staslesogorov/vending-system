using System;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    public string FIO { get; set; }

    public string Email { get; set; }

    public string? Phone { get; set; }

    public string Role { get; set; }
}