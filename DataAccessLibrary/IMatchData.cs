using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IMatchData
{
    Task<List<MatchModel>> GetMatches();
    Task<int> InsertMatch(MatchModel match);
    Task<List<MatchModel>> GetMatchesByEvent(int eventId);
    Task UpdateMatch(MatchModel match);
    Task DeleteMatch(int matchId);
}