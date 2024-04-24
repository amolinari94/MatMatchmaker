using System.Drawing;
using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary;

public class WrestlerData : IWrestlerData
{
    private readonly ISqlDataAccess _dba;

    public WrestlerData(ISqlDataAccess db)
    {
        _dba = db;
    }

    public Task<List<WrestlerModel>> GetWrestlers()
    {
        string sql = "select * from dbo.Wrestlers";
        return _dba.LoadData<WrestlerModel, dynamic>(sql, new { });
    }
    
    public Task InsertWrestler(WrestlerModel wrestler)
    {
        string sql = @"insert into dbo.Wrestlers (Email, FirstName, LastName, Grade, Skill, Gender, SchoolName) 
                      values (@Email, @FirstName, @LastName, @Grade, @Skill, @Gender, @SchoolName);";

        return _dba.SaveData(sql, wrestler);
    }
    
    public async Task<List<WrestlerModel>> GetWrestlersByEmail(string email)
    {
        string sql = "SELECT * FROM dbo.Wrestlers WHERE Email = @Email";
        return await _dba.LoadData<WrestlerModel, dynamic>(sql, new { Email = email });
    }
    
    public Task UpdateWrestler(WrestlerModel wrestler)
    {
        string sql = @"UPDATE dbo.Wrestlers 
                   SET FirstName = @FirstName, 
                       LastName = @LastName, 
                       Grade = @Grade, 
                       Skill = @Skill, 
                       Gender = @Gender, 
                       SchoolName = @SchoolName
                   WHERE Email = @Email;";

        return _dba.SaveData(sql, wrestler);
    }


}