using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using NUnit.Framework.Internal.Execution;

namespace DataAccessLibrary
{
    public class EventData : IEventData
    {
        private readonly ISqlDataAccess _dbe;

        public EventData(ISqlDataAccess db)
        {
            _dbe = db;
        }
        
        public async Task<int> AddEvent(DateTime eventDate, int hostProfileId)
        {
            try
            {
                // Insert event into the database
                string sql = @"INSERT INTO Events (host_profile_id, event_date) 
                               VALUES (@host_profile_id, @event_date);
                               SELECT CAST(SCOPE_IDENTITY() AS INT);";

                // Parameters for SQL query
                var parameters = new
                {
                    host_profile_id = hostProfileId,
                    event_date = eventDate
                };

                int eventId = await _dbe.ExecuteScalar<int>(sql, parameters);

                if (eventId > 0)
                {
                    // Event successfully added, return the event ID
                    return eventId;
                }
                else
                {
                    // Failed to insert event
                    return -1;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error adding event: {ex.Message}");
                return -1;
            }
        }
        
        public async Task<List<EventModel>> GetEventsByHostId(int hostId)
        {
            try
            {
                string sql = "SELECT event_id, host_profile_id, event_date FROM Events WHERE host_profile_id = @HostId " +
                             "ORDER BY event_id DESC"; // Sort by event_id in descending order

                var parameters = new { HostId = hostId };

                // Retrieve events with sorting applied
                List<EventModel> events = await _dbe.LoadData<EventModel, object>(sql, parameters);

                return events;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving events by host ID: {ex.Message}");
                return new List<EventModel>(); // Return an empty list or handle error gracefully
            }
        }


    }
}

        /*public async Task<List<EventModel>> GetEvents()
        {
            string sql = "SELECT * FROM dbo.Events";
            var events = await _db.LoadData<EventModel, dynamic>(sql, new { });
            
            // Load matches for each event
            foreach (var ev in events)
            {
                ev.Matches = await GetMatchesForEvent(ev.EventID);
            }

            return events;
        }

        public async Task<int> InsertEvent(EventModel eventModel)
        {
            // Insert Event details into Events table
            string eventSql = @"
        INSERT INTO dbo.Events (EventDate, Location, Result, Wrestler1ID, Wrestler2ID)
        VALUES (@EventDate, @Location, @Result, @Wrestler1ID, @Wrestler2ID);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int eventId = await _db.ExecuteScalar<int>(eventSql, new
            {
                eventModel.EventDate,
                eventModel.Location,
                eventModel.Result,
                eventModel.Wrestler1ID,
                eventModel.Wrestler2ID
            });

            // Insert Matches associated with the Event
            foreach (var match in eventModel.Matches)
            {
                string matchSql = @"
            INSERT INTO dbo.Matches (EventID, Wrestler1ID, Wrestler2ID)
            VALUES (@EventID, @Wrestler1ID, @Wrestler2ID);";

                await _db.SaveData(matchSql, new
                {
                    EventID = eventId,
                    match.Wrestler1ID,
                    match.Wrestler2ID,
                    
                });
            }

            return eventId;
        }

        
         public async Task InsertMatch(int eventId, int wrestler1Id, int wrestler2Id, string matchResult)
        {
            string sql = @"INSERT INTO dbo.Matches (EventID, Wrestler1ID, Wrestler2ID, MatchResult) 
                           VALUES (@EventID, @Wrestler1ID, @Wrestler2ID, @MatchResult);";

            await _db.SaveData(sql, new { EventID = eventId, Wrestler1ID = wrestler1Id, Wrestler2ID = wrestler2Id, MatchResult = matchResult });
        }

        

        // Example method to retrieve events by MatchId (if needed)
        public async Task<List<EventModel>> GetEventsByMatchId(int matchId)
        {
            string sql = "SELECT * FROM dbo.Events WHERE MatchId = @MatchId";

            return await _db.LoadData<EventModel, dynamic>(sql, new { MatchId = matchId });
        }
        
        public async Task<List<MatchModel>> GetMatchesForEvent(int eventId)
        {
            string sql = "SELECT * FROM dbo.Matches WHERE EventID = @EventID";
            return await _db.LoadData<MatchModel, dynamic>(sql, new { EventID = eventId });
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
