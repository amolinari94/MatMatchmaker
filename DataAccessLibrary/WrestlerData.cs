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
        string sql = "SELECT WrestlerID, FirstName, LastName, Grade, Skill, Gender, SchoolName, Email FROM dbo.Wrestlers";
        return _dba.LoadData<WrestlerModel, dynamic>(sql, new{});
    }
    
    public async Task<int> InsertWrestler(WrestlerModel wrestler)
    {
        string sql = @"INSERT INTO dbo.Wrestlers (Email, FirstName, LastName, Grade, Skill, Gender, SchoolName) 
                   VALUES (@Email, @FirstName, @LastName, @Grade, @Skill, @Gender, @SchoolName);
                   SELECT CAST(SCOPE_IDENTITY() AS INT);";

        // Execute the SQL query and retrieve the generated WrestlerID
        var wrestlerID = await _dba.ExecuteScalar<int>(sql, wrestler);
    
        // Return the WrestlerID
        return wrestlerID;
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
                   WHERE WrestlerID = @WrestlerID;";

        return _dba.SaveData(sql, wrestler);
    }
    
    public async Task DeleteWrestler(int wrestlerID)
    {
        string sql = "DELETE FROM dbo.Wrestlers WHERE WrestlerID = @WrestlerID";
        await _dba.SaveData(sql, new { WrestlerID = wrestlerID });
    }


}