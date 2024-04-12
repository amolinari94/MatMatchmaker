namespace DataAccessLibrary.Models;

public class ProfileModel
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string SchoolName { get; set; }
    public string PasswordHash { get; set; }

    public ProfileModel() { }

    public ProfileModel(string email, string username, string city, string state, string schoolName,
        string passwordHash) {
        this.Email = email;
        this.Username = username;
        this.City = city;
        this.State = state;
        this.SchoolName = schoolName;
        this.PasswordHash = passwordHash;
    }
}