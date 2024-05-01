namespace DataAccessLibrary.Models;

    public class EventGuestsModel
    {
        public int event_id { get; set; } // Maps to event_id in the database
        public int guest_profile_id { get; set; } // Maps to guest_profile_id in the database

        
    }
