using System.Threading.Tasks;
using DataAccessLibrary.Models;
using BCrypt.Net;

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
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(profile.password);

            string sql = @"INSERT INTO dbo.Profile (email, password, schoolName, city, state) 
                       VALUES (@Email, @Password, @SchoolName, @City, @State);";

            var parameters = new
            {
                Email = profile.email,
                Password = hashedPassword,
                SchoolName = profile.schoolName,
                City = profile.city,
                State = profile.state
            };

            await _db.SaveData(sql, parameters);
        }

        public async Task<ProfileModel> AuthenticateUser(string email, string password)
        {
            string sql = @"SELECT * FROM dbo.Profile WHERE email = @Email;";

            var parameters = new
            {
                Email = email
            };

            try
            {
                var userProfile = await _db.LoadData<ProfileModel, dynamic>(sql, parameters);
        
                if (userProfile != null && userProfile.Any())
                {
                    var storedHashedPassword = userProfile.First().password;

                    // Verify the plaintext password against the stored hashed password
                    if (BCrypt.Net.BCrypt.Verify(password, storedHashedPassword))
                    {
                        return userProfile.First(); // Return the user profile if authentication succeeds
                    }
                }

                return null; // User not found or password does not match
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
                
            }

            return profiles;
        }
        public async Task<ProfileModel> GetProfileByID(int id) {
            ProfileModel profile = new ProfileModel();

            
            string sql = "SELECT * FROM dbo.Profile WHERE profile_id = @id;";
            var parameters = new { ID = id};

            profile = await _db.LoadSingleRecord<ProfileModel, dynamic>(sql, parameters);

            if (profile != null)
            {
                return profile;
            }
            // Handle case where profile is not found or other error scenarios

            return null;
            

        }
        
        public async Task<bool> ResetPassword(string email, string newPassword)
        {
            string sql = @"
            UPDATE dbo.Profile
            SET password = @NewPassword
            WHERE email = @Email";

            int rowsAffected = await _db.ExecuteAsync(sql, new { NewPassword = newPassword, Email = email });
            return rowsAffected > 0;
        }
        
        public async Task<ProfileModel> GetUserByEmail(string email)
        {
            string sql = @"SELECT * FROM dbo.Profile WHERE email = @Email;";

            var parameters = new
            {
                Email = email
            };

            try
            {
                var userProfile = await _db.LoadData<ProfileModel, dynamic>(sql, parameters);
                return userProfile.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetUserByEmail: " + ex.Message);
                return null;
            }
        }
    }
}