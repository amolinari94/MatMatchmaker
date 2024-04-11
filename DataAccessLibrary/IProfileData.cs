using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IProfileData
    {
        Task<List<ProfileModel>> GetProfile();
        Task InsertProfile(ProfileModel profile);
    }
}