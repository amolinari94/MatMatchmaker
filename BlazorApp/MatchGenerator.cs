using Structure;

namespace BlazorApp;


public class MatchGenerator {
    private Roster hostRoster;
    private Roster[] guestRosters;
    private LinkedList<WrestlerEventInstance> visitingWrestlers;
    private double weightDiff;
    private int numMats, matchMin, matchMax, skillGap, gradeGap;
    private bool internalMatches;
    private string eventID;
    private int allowedMatches;
    private int matchCount;

    private LinkedList<Match> matchList;
    
    public MatchGenerator(Roster hostRoster, Roster[] guestRosters, double weightDiff, int numMats, int matchMin,
        int matchMax, int skillGap, int gradeGap, bool internalMatches, string eventID, int allowedMatches) {
        this.hostRoster = hostRoster;
        this.guestRosters = guestRosters;
        this.weightDiff = weightDiff;
        this.numMats = numMats;
        this.matchMin = matchMin;
        this.matchMax = matchMax;
        this.skillGap = skillGap;
        this.gradeGap = gradeGap;
        this.internalMatches = internalMatches;
        this.eventID = eventID;
        this.allowedMatches = allowedMatches;
        this.matchCount = 0;
        matchList = new LinkedList<Match>();
        visitingWrestlers = new LinkedList<WrestlerEventInstance>();
        for (int i = 0; i < guestRosters.Length; i++) {
            foreach (var wrestler in guestRosters[i].rosterList.Values) {
                WrestlerEventInstance wrestlerInstance = new WrestlerEventInstance(wrestler, allowedMatches);
                visitingWrestlers.Append(wrestlerInstance);
            }
        }
    }
    
    private bool checkAvailability(WrestlerEventInstance wrestlerInst) {
        int duplicates = 0;
        bool available = true;
        foreach (Match match in matchList) {
            if (match.wrestler1.wrestler.firstName == wrestlerInst.wrestler.firstName 
                && match.wrestler1.wrestler.lastName == wrestlerInst.wrestler.lastName) {
                duplicates++;
            }
            else if (match.wrestler2.wrestler.firstName ==wrestlerInst.wrestler.firstName 
                     && match.wrestler2.wrestler.lastName == wrestlerInst.wrestler.lastName) {
                duplicates++;
            }
        }
        if (duplicates == wrestlerInst.allowedMatches) {
            available = false;
        }

        return available;
    }


    protected internal LinkedList<Match> GenerateMatches(bool randomize, int allowedMatches) {

        if (randomize) {/*randomize visitors wrestler list before generating */ }

        int MatchesGeneratedPerHomeWrestler = 0;
        while (matchCount < matchMin) {
                try {
                    while (allowedMatches > MatchesGeneratedPerHomeWrestler) {
                        /*
                    foreach (var wrestler in hostRoster.rosterList.Values) {
                        WrestlerEventInstance homeWrestler = new WrestlerEventInstance(wrestler, allowedMatches);
                        foreach (var visitor in visitingWrestlers) {
                            
                            if (double.Abs(visitor.wrestler.weight - wrestler.weight) <= weightDiff) {//if weight diff is valid.
                                if (!wrestler.sameGenderOnly) {//if wrestlers have no gender preference.
                                    matchCount++;//increment match count.

                                    if (checkAvailability(homeWrestler) && checkAvailability(visitor)) {
                                        matchList =(LinkedList<Match>) matchList.Append(new Match(eventID, matchCount, homeWrestler, visitor));//add match.
                                    }
                                }
                                else {
                                    //if preference is same gender only.
                                    if (homeWrestler.wrestler.gender != visitor.wrestler.gender) {//if genders are not same. 
                                        continue;//skips code below 
                                    }
                                    //if genders are same
                                    matchCount++;//increment match count.
                                    matchList.Append(new Match(eventID,matchCount, homeWrestler, visitor));//add match.
                                }
                            }
                        
                        }
            
                    }
                    */
                    
                    //trying different approach>>
                        foreach (var wrestler in hostRoster.rosterList.Values) {
                            WrestlerEventInstance homeWrestler = new WrestlerEventInstance(wrestler, allowedMatches);
                            foreach (var visitor in visitingWrestlers) {
                                if (double.Abs(visitor.wrestler.weight - homeWrestler.wrestler.weight) > weightDiff) {
                                    continue;
                                }
                                if (homeWrestler.wrestler.sameGenderOnly && homeWrestler.wrestler.gender != visitor.wrestler.gender) {
                                    continue;
                                }
                                if (visitor.wrestler.sameGenderOnly &&
                                    homeWrestler.wrestler.gender != visitor.wrestler.gender) {
                                    continue;
                                }
                                if (!checkAvailability(visitor)) {
                                    continue;
                                }
                                if (int.Abs(visitor.wrestler.skillLevel - homeWrestler.wrestler.skillLevel) > skillGap) {
                                    continue;
                                }
                                if (int.Abs(visitor.wrestler.grade - homeWrestler.wrestler.grade) > gradeGap) {
                                    continue;
                                }
                                matchCount++;//increment match count.
                                matchList.Append(new Match(eventID,matchCount, homeWrestler, visitor));//add match.
                            }
                            
                        }
                        MatchesGeneratedPerHomeWrestler++;
                    }
                }catch (Exception e) {
                    Console.WriteLine(e);
                }
        }

        return matchList;
    }
    
    
    
}