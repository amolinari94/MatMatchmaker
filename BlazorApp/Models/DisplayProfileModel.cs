using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models;

public class DisplayProfileModel
{
    [Required]
    [EmailAddress]
    public string email { get; set; }
    
    public string city { get; set; }
    public string state { get; set; }
    public string schoolName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string password { get; set; }
    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    
}