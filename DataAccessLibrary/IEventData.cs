using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IEventData
{
    public  Task<List<EventModel>> GetEvents();
    public Task<int> InsertEvent(EventModel eventModel);
    Task InsertMatch(int eventId, int wrestler1Id, int wrestler2Id, string matchResult);
    Task<List<MatchModel>> GetMatchesForEvent(int eventId);
    
}