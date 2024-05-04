namespace DataAccessLibrary.Models;

    public class MatchModel
    {
        public int match_id { get; set; } // Maps to match_id in the database
        public int event_id { get; set; } // Maps to event_id in the database
        public int wrestler1_id { get; set; } // Maps to wrestler1_id in the database
        public int wrestler2_id { get; set; } // Maps to wrestler2_id in the database
        public string Wrestler1FirstName { get; set; }
        public string Wrestler1LastName { get; set; }
        public int Wrestler1Weight { get; set; }
        public string Wrestler2FirstName { get; set; }
        public string Wrestler2LastName { get; set; }
        public int Wrestler2Weight { get; set; }
        
        
        public MatchModel()
        {
            
        }
    }

