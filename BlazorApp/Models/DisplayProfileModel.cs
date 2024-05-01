using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models;

public class DisplayProfileModel
{
    //[Required]
    //[EmailAddress]
    public string email { get; set; }
    
    public string city { get; set; }
    public string state { get; set; }
    public string schoolName { get; set; }
    public string password { get; set; }
    
}