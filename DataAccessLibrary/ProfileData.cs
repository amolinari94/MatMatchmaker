using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataAccessLibrary.Models;
using Microsoft.Data.SqlClient;

namespace DataAccessLibrary
{
    public class ProfileData : IProfileData
    {
        private readonly ISqlDataAccess _db;

        public ProfileData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<ProfileModel>> GetProfiles()
        {
            string sql = "SELECT * FROM dbo.Profile;";
            return await _db.LoadData<ProfileModel, dynamic>(sql, new { });
        }

        public async Task InsertProfile(ProfileModel profile)
        {
            string sql = @"INSERT INTO dbo.Profile (email, password, schoolName, city, state) 
                           VALUES (@email, @password, @schoolName, @city, @state);";

            await _db.SaveData(sql, profile);
        }

        public async Task<ProfileModel> AuthenticateUser(string Email, string Password)
        {
            string sql = @"SELECT * FROM dbo.Profile WHERE Email = @email AND Password = @password;";

            var parameters = new
            {
                email = Email,
                password = Password // For demo purposes; you should use password hashing for production
            };

            try
            {
                var userProfileList = await _db.LoadData<ProfileModel, dynamic>(sql, parameters);
                return userProfileList.FirstOrDefault(); // Return the first matching user profile, or null if not found
            }
            catch (Exception ex)
            {
                // Properly handle the exception (e.g., log, rethrow, return null, etc.)
                Console.WriteLine("Error in AuthenticateUser: " + ex.Message);
                return null;
            }
        }
        
        public async Task<List<ProfileModel>> GetProfilesBySchoolNames(List<string> schoolNames)
        {
            List<ProfileModel> profiles = new List<ProfileModel>();

            foreach (var schoolName in schoolNames)
            {
                string sql = "SELECT * FROM dbo.Profile WHERE schoolName = @SchoolName";
                var parameters = new { SchoolName = schoolName };

                ProfileModel profile = await _db.LoadSingleRecord<ProfileModel, dynamic>(sql, parameters);

                if (profile != null)
                {
                    profiles.Add(profile);
                }
                // Handle case where profile is not found or other error scenarios
            }

            return profiles;
        }
        
        
    }
}