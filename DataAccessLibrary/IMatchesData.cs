using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IMatchesData
{
    Task<List<MatchModel>> GetMatches();
    Task<int> InsertMatch(MatchModel match);
    Task<List<MatchModel>> GetMatchesByEvent(int eventId);
    Task UpdateMatch(MatchModel match);
    Task DeleteMatch(int matchId);
}