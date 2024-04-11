using System.Drawing;
using DataAccessLibrary.Models;

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
        string sql = @"insert into dbo.Profile (EmailAddress, Username, City, State, SchoolName, PasswordHash, SchoolId) 
                      values (@EmailAddress, @Username, @City, @State, @SchoolName, @PasswordHash, @SchoolId);";

        return _db.SaveData(sql, profile);
    }
}