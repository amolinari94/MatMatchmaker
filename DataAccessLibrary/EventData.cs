using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            return _db.SaveData(sql, eventModel);
        }

        // You can add other methods here for updating, deleting, or querying events
    }
}