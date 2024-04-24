namespace DataAccessLibrary;

public interface IEventData
{
    public  Task<List<EventModel>> GetEvents();
    public Task InsertEvent(EventModel eventModel);
    
}