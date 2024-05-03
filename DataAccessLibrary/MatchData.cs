using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public class MatchData : IMatchData
    {
        private readonly ISqlDataAccess _dba;

        public MatchData(ISqlDataAccess db)
        {
            _dba = db;
        }

        public async Task<List<MatchModel>> GetMatches()
        {
            string sql = "SELECT MatchId, EventId, Wrestler1Id, Wrestler2Id, WinnerId, Result FROM dbo.Matches";
            return await _dba.LoadData<MatchModel, dynamic>(sql, new {});
        }
        

        public async Task<int> InsertMatch(MatchModel match)
        {
            string sql = @"
                INSERT INTO dbo.Matches (EventId, Wrestler1Id, Wrestler2Id, WinnerId, Result) 
                VALUES (@EventId, @Wrestler1Id, @Wrestler2Id, @WinnerId, @Result);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var matchId = await _dba.ExecuteScalar<int>(sql, match);
            return matchId;
        }

        public async Task<List<MatchModel>> GetMatchesByEvent(int eventId)
        {
            string sql = "SELECT * FROM dbo.Matches WHERE event_id = @EventId";
            return await _dba.LoadData<MatchModel, dynamic>(sql, new { EventId = eventId });
        }
        
        

        public async Task UpdateMatch(MatchModel match)
        {
            string sql = @"
                UPDATE dbo.Matches 
                SET EventId = @EventId, 
                    Wrestler1Id = @Wrestler1Id, 
                    Wrestler2Id = @Wrestler2Id, 
                    WinnerId = @WinnerId, 
                    Result = @Result
                WHERE MatchId = @MatchId;";

            await _dba.SaveData(sql, match);
        }

        public async Task DeleteMatch(int matchId)
        {
            string sql = "DELETE FROM dbo.Matches WHERE MatchId = @MatchId";
            await _dba.SaveData(sql, new { MatchId = matchId });
        }
    }
}