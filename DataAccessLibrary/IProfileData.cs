using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IProfileData
    {
        Task<List<ProfileModel>> GetProfiles();
        Task InsertProfile(ProfileModel profile);
        Task<ProfileModel> AuthenticateUser(string email, string password);
        Task<List<ProfileModel>> GetProfilesBySchoolNames(List<string> schoolNames);
        Task<bool> ResetPassword(string email, string newPassword);
        Task<ProfileModel> GetUserByEmail(string email);

    }
}