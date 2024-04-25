namespace DataAccessLibrary.Models;

public interface IWrestlerData
{
    Task<List<WrestlerModel>> GetWrestlers();
    public Task<int> InsertWrestler(WrestlerModel wrestler);
    
    Task<List<WrestlerModel>> GetWrestlersByEmail(string email);
    public Task UpdateWrestler(WrestlerModel wrestler);
    public Task DeleteWrestler(int wrestlerID);
}