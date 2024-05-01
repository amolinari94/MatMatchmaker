namespace DataAccessLibrary.Models;

public class ProfileModel
{
    public int profile_id { get; set; } // Maps to profile_id in the database
    public string email { get; set; }
    public string password { get; set; }
    public string schoolName { get; set; }
    public string city { get; set; }
    public string state { get; set; }
}