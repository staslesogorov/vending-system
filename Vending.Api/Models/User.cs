using System;
using System.ComponentModel.DataAnnotations;

public enum UserRole { Администратор, Оператор }

public class User
{
    public int Id { get; set; }

    [Required] public string FullName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Phone { get; set; }

    [Required] public UserRole Role { get; set; }
}
