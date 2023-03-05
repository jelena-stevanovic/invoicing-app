using System.ComponentModel.DataAnnotations;

namespace InvoicingApp.Api.Identity.Models;

public class RegisterRequest
{
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "First Name is required")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Last Name is required")]
    public string? LastName { get; set; }

    [Phone]
    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Phone number is required")]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Your password and confirm password do not match")]
    public string ConfirmPassword { get; set; }
}