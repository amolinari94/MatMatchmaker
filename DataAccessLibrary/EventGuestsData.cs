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
        
        public async Task InsertGuest(int eventId, int guestProfileId)
        {
            try
            {
                string sql = @"
                INSERT INTO dbo.EventGuests (event_id, guest_profile_id, accepted)
                VALUES (@EventID, @GuestProfileID, NULL);
            ";

                var parameters = new
                {
                    EventID = eventId,
                    GuestProfileID = guestProfileId
                };

                await _db.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error inserting guest: {ex.Message}");
                throw; // Optionally handle or rethrow the exception
            }
        }
        
        public async Task AcceptInvite(int eventGuestId)
        {
            try
            {
                string sql = @"
            UPDATE dbo.EventGuests
            SET accepted = 1
            WHERE id = @EventGuestID;
        ";

                var parameters = new
                {
                    EventGuestID = eventGuestId
                };

                await _db.ExecuteAsync(sql, parameters); // Execute the SQL query to update acceptance status
                Console.WriteLine("Guest invitation accepted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accepting guest invitation: {ex.Message}");
                throw; // Optionally handle or rethrow the exception
            }
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