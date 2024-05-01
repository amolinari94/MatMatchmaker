using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IProfileData
    {
        Task<List<ProfileModel>> GetProfiles();
        Task InsertProfile(ProfileModel profile);
        Task<ProfileModel> AuthenticateUser(string email, string password);
        
    }
}