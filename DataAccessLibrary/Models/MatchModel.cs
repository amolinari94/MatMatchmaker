namespace DataAccessLibrary.Models;

    public class MatchModel
    {
        public int match_id { get; set; } // Maps to match_id in the database
        public int event_id { get; set; } // Maps to event_id in the database
        public int wrestler1_id { get; set; } // Maps to wrestler1_id in the database
        public int wrestler2_id { get; set; } // Maps to wrestler2_id in the database
        public string match_result { get; set; } // Maps to match_result in the database
        
        public MatchModel()
        {
            // Initialization logic here if needed
        }
    }

