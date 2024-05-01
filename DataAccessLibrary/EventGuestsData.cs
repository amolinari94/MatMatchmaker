using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public class EventGuestsData : IEventGuestsData
    {
        private readonly ISqlDataAccess _db;

        public EventGuestsData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<EventGuestsModel>> GetEventGuestsByEventId(int eventId)
        {
            string sql = "SELECT * FROM EventGuests WHERE event_id = @EventId";
            return await _db.LoadData<EventGuestsModel, dynamic>(sql, new { EventId = eventId });
        }

        public async Task<List<EventGuestsModel>> GetEventGuestsByGuestProfileId(int guestProfileId)
        {
            string sql = "SELECT * FROM EventGuests WHERE guest_profile_id = @GuestProfileId";
            return await _db.LoadData<EventGuestsModel, dynamic>(sql, new { GuestProfileId = guestProfileId });
        }

        public async Task<int> InsertEventGuest(EventGuestsModel eventGuest)
        {
            string sql = @"INSERT INTO EventGuests (event_id, guest_profile_id) 
                   VALUES (@EventId, @GuestProfileId);
                   SELECT CAST(SCOPE_IDENTITY() AS INT);";

            // Execute the SQL query and retrieve the generated ID
            var insertedId = await _db.ExecuteScalar<int>(sql, eventGuest);

            // Return the generated ID
            return insertedId;
        }


        public async Task DeleteEventGuest(int eventId, int guestProfileId)
        {
            string sql = "DELETE FROM EventGuests WHERE event_id = @EventId AND guest_profile_id = @GuestProfileId";
            await _db.SaveData(sql, new { EventId = eventId, GuestProfileId = guestProfileId });
        }

        

    }
}