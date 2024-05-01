using System;
using DataAccessLibrary.Models;

public class EventModel
{
    public int event_id { get; set; } // Maps to event_id in the database
    public int host_profile_id { get; set; } // Maps to host_profile_id in the database
    public DateTime? event_date { get; set; } // Maps to event_date in the database
    

    
    public EventModel()
    {
        
    }
}

