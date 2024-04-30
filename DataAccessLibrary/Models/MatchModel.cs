namespace DataAccessLibrary.Models;

public class MatchModel
{
    public int MatchId { get; set; } // Unique identifier for the match
    public int EventId { get; set; } // Reference to the event associated with this match
    public int Wrestler1ID { get; set; } // Wrestler 1 ID (foreign key to WrestlerModel)
    public int Wrestler2ID { get; set; } // Wrestler 2 ID (foreign key to WrestlerModel)
    public int WinnerID { get; set; } // ID of the winning wrestler
    public string Result { get; set; } // Match result description (e.g., score, outcome)
    
    // unsure if these are needed
    public WrestlerModel Wrestler1 { get; set; } // Navigation property for Wrestler 1
    public WrestlerModel Wrestler2 { get; set; } // Navigation property for Wrestler 2
}