using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public class EventData : IEventData
    {
        private readonly ISqlDataAccess _db;

        public EventData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<EventModel>> GetEvents()
        {
            string sql = "SELECT * FROM dbo.Events";

            return await _db.LoadData<EventModel, dynamic>(sql, new { });
        }

        public Task InsertEvent(EventModel eventModel)
        {
            string sql = @"INSERT INTO dbo.Events (EventDate, Location, Result, Wrestler1ID, Wrestler2ID) 
                   VALUES (@EventDate, @Location, @Result, @Wrestler1ID, @Wrestler2ID)";

            // Define the SQL parameters using an anonymous type
            var parameters = new
            {
                eventModel.EventDate,
                eventModel.Location,
                eventModel.Result,
                eventModel.Wrestler1ID,
                eventModel.Wrestler2ID
            };

            // Execute the SQL query with the parameters
            return _db.SaveData(sql, parameters);
        }

        // Add additional methods as needed (e.g., UpdateEvent, DeleteEvent)

        // Example method to retrieve events by MatchId (if needed)
        public async Task<List<EventModel>> GetEventsByMatchId(int matchId)
        {
            string sql = "SELECT * FROM dbo.Events WHERE MatchId = @MatchId";

            return await _db.LoadData<EventModel, dynamic>(sql, new { MatchId = matchId });
        }
        
    }
        
        /*public Task CreateNewEvent(EventModel eventModel) 
        {
            string sql = @"INSERT INTO dbo.Events ( eventID, Host, Guests, Date) 
                           VALUES (@EventID, @Host, @Guests, @Date)";

            return _db.SaveData(sql, eventModel);
        }

        // You can add other methods here for updating, deleting, or querying events
    }*/
}