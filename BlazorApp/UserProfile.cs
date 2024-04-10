using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Components
{

    public class UserProfile
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string SchoolName { get; set; }
    }
}