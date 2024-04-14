namespace DataAccessLibrary.Models;

public interface IWrestlerData
{
    Task<List<WrestlerModel>> GetWrestlers();
    public Task InsertWrestler(WrestlerModel wrestler);

}