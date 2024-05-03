using System.Drawing;
using DataAccessLibrary.Models;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class WrestlerData : IWrestlerData
    {
        private readonly ISqlDataAccess _dba;

        public WrestlerData(ISqlDataAccess db)
        {
            _dba = db;
        }

        public Task<List<WrestlerModel>> GetWrestlers()
        {
            string sql = "SELECT wrestler_id, profile_id, firstName, lastName, weight, skillLevel, grade, gender, sameGenderOnly FROM dbo.Wrestlers";
            return _dba.LoadData<WrestlerModel, dynamic>(sql, new {});
        }
        
        public async Task<int> InsertWrestler(WrestlerModel wrestler)
        {
            string sql = @"INSERT INTO dbo.Wrestlers (profile_id, firstName, lastName, weight, skillLevel, grade, gender, sameGenderOnly) 
                           VALUES (@profile_id, @firstName, @lastName, @weight, @skillLevel, @grade, @gender, @sameGenderOnly);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            // Execute the SQL query and retrieve the generated WrestlerID
            var wrestlerID = await _dba.ExecuteScalar<int>(sql, wrestler);
        
            // Return the WrestlerID
            return wrestlerID;
        }
        
        
        public async Task<List<WrestlerModel>> GetWrestlersByProfileID(int profileId)
        {
            string sql = "SELECT * FROM dbo.Wrestlers WHERE profile_id = @ProfileId";
            return await _dba.LoadData<WrestlerModel, dynamic>(sql, new { ProfileId = profileId });
        }

        
        public Task UpdateWrestler(WrestlerModel wrestler)
        {
            string sql = @"UPDATE dbo.Wrestlers 
                           SET firstName = @firstName, 
                               lastName = @lastName, 
                               weight = @weight, 
                               skillLevel = @skillLevel, 
                               grade = @grade, 
                               gender = @gender, 
                               sameGenderOnly = @sameGenderOnly
                           WHERE wrestler_id = @wrestler_id;";

            return _dba.SaveData(sql, wrestler);
        }
        
        public async Task DeleteWrestler(int wrestlerID)
        {
            string sql = "DELETE FROM dbo.Wrestlers WHERE wrestler_id = @wrestler_id";
            await _dba.SaveData(sql, new { wrestler_id = wrestlerID });
        }
        
        public async Task<WrestlerModel> GetWrestlerById(int wrestlerId)
        {
            string sql = "SELECT * FROM dbo.Wrestlers WHERE wrestler_id = @WrestlerId";
            var wrestlers = await _dba.LoadData<WrestlerModel, dynamic>(sql, new { WrestlerId = wrestlerId });

            return wrestlers.FirstOrDefault(); // Return the first (or default) wrestler in the list
        }
        
        public async Task<List<MatchModel>> GetMatchesForEvent(int eventId)
        {
            string sql = @"
        SELECT m.match_id, m.event_id, m.wrestler1_id, m.wrestler2_id,
               w1.firstName AS Wrestler1FirstName, w1.lastName AS Wrestler1LastName, w1.weight AS Wrestler1Weight,
               w2.firstName AS Wrestler2FirstName, w2.lastName AS Wrestler2LastName, w2.weight AS Wrestler2Weight
        FROM dbo.Matches m
        JOIN dbo.Wrestlers w1 ON m.wrestler1_id = w1.wrestler_id
        JOIN dbo.Wrestlers w2 ON m.wrestler2_id = w2.wrestler_id
        WHERE m.event_id = @EventId;";

            var parameters = new { EventId = eventId };

            var matches = await _dba.LoadData<MatchModel, dynamic>(sql, parameters);

            return matches;
        }



    }
}
