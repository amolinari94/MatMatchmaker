using DataAccessLibrary.Models;
using NUnit.Framework.Internal.Execution;

namespace DataAccessLibrary;

public interface IEventData
{
    Task<int> AddEvent(DateTime eventDate, int hostProfileId);

    Task<List<EventModel>> GetEventsByHostId(int hostId);
    /* public  Task<List<EventModel>> GetEvents();
     public Task<int> InsertEvent(EventModel eventModel);
     Task InsertMatch(int eventId, int wrestler1Id, int wrestler2Id, string matchResult);
     Task<List<MatchModel>> GetMatchesForEvent(int eventId);*/

}