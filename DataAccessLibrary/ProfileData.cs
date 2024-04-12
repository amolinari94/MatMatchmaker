using System.Drawing;
using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary;

public class ProfileData : IProfileData
{
    private readonly ISqlDataAccess _db;
    public ProfileData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<ProfileModel>> GetProfile()
    {
        string sql = "select * from dbo.Profile";

        return _db.LoadData<ProfileModel, dynamic>(sql, new { });
    }

    public Task InsertProfile(ProfileModel profile)
    {
        string sql = @"insert into dbo.Profile (Email, Username, City, State, SchoolName, PasswordHash) 
                      values (@Email, @Username, @City, @State, @SchoolName, @PasswordHash);";

        return _db.SaveData(sql, profile);
    }
    
    public async Task<ProfileModel> AuthenticateUser(string email, string password)
    {
        // Query the database to check if the provided email and password are valid
        string sql = @"SELECT COUNT(*) FROM dbo.Profile WHERE Email = @Email AND PasswordHash = @PasswordHash;";

        var parameters = new
        {
            Email = email,
            PasswordHash = password // Directly using the provided password for now, update this when  implement password hashing
        };

        try
        {
            var userProfile = await _db.LoadData<ProfileModel, dynamic>(sql, parameters);
            return userProfile.FirstOrDefault(); // Return the first matching user profile, or null if not found
        }
        catch (Exception ex)
        {
            // Handle any exceptions (e.g., database error)
            Console.WriteLine("Error in AuthenticateUser: " + ex.Message);
            return null;
        }
    }

}