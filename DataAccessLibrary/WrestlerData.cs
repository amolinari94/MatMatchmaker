using System.Drawing;
using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary;

public class WrestlerData : IWrestlerData
{
    private readonly ISqlDataAccess _db;

    public WrestlerData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<WrestlerModel>> GetWrestlers()
    {
        string sql = "select * from dbo.Wrestlers";
        return _db.LoadData<WrestlerModel, dynamic>(sql, new { });
    }
    
    public Task InsertWrestler(WrestlerModel wrestler)
    {
        string sql = @"insert into dbo.Wrestlers (Email, FirstName, LastName, Grade, Skill, Gender, SchoolName) 
                      values (@Email, @FirstName, @LastName, @Grade, @Skill, @Gender, @SchoolName);";

        return _db.SaveData(sql, wrestler);
    }
    
    public async Task<List<WrestlerModel>> GetWrestlersByEmail(string email)
    {
        string sql = "SELECT * FROM dbo.Wrestlers WHERE Email = @Email";
        return await _db.LoadData<WrestlerModel, dynamic>(sql, new { Email = email });
    }

}