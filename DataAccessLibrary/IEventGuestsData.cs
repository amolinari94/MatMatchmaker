using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IEventGuestsData
{
    Task<List<EventGuestsModel>> GetEventGuestsByEventId(int eventId);
    Task<List<EventGuestsModel>> GetEventGuestsByGuestProfileId(int guestProfileId);
    Task<int> InsertEventGuest(EventGuestsModel eventGuest);
    Task DeleteEventGuest(int eventId, int guestProfileId);
    Task InsertGuest(int eventId, int guestProfileId);
}