namespace DataAccessLibrary.Models;

public class WrestlerModel
{
    public int wrestler_id { get; set; } // Maps to wrestler_id in the database
    public int profile_id { get; set; } // Maps to profile_id in the database
    public string firstName { get; set; } // Maps to firstName in the database
    public string lastName { get; set; } // Maps to lastName in the database
    public int weight { get; set; } // Maps to weight in the database
    public int skillLevel { get; set; } // Maps to skillLevel in the database
    public int grade { get; set; } // Maps to grade in the database
    public string gender { get; set; } // Maps to gender in the database
    public bool sameGenderOnly { get; set; } // Maps to genderPreference in the database
}