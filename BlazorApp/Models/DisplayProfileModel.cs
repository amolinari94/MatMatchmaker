using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models;

public class DisplayProfileModel
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    public string Username { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string SchoolName { get; set; }
    public string PasswordHash { get; set; }
    public string SchoolId { get; set; }
}