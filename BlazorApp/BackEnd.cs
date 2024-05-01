using System;
using Microsoft.AspNetCore.Components;
//backend
namespace Structure
{
    
    /* Class WRESTLER
     * Holds information for each student/wrestler
     */
    public class Wrestler{
        //made public for testing
        public String firstName, lastName, gender,schoolName, email;
        public bool editing;
        public int wrestlerID;
        public double weight;
        public bool sameGenderOnly;

        //made public for testing 
        public int grade, skill;
        //changed from protected internal to public for testing
        public Wrestler(int wrestlerId, String email, String firstName, String lastName, int grade, int skill, String gender, String schoolName)
        {
            this.wrestlerID = wrestlerId;
            this.email = email;
            this.firstName=firstName;
            this.lastName=lastName;
            this.grade=grade;
            this.skill=skill;
            this.gender=gender;
            this.schoolName=schoolName;
            this.editing = false;
        }
        
        

    }

    public class WrestlerEventInstance {
        protected internal Wrestler wrestler;
        protected internal int allowedMatches;

        public WrestlerEventInstance(Wrestler wrestler, int allowedMatches) {
            this.wrestler = wrestler;
            this.allowedMatches = allowedMatches;
        }
    }
    
    /* Class ROSTER
     * Contains members size, schoolName and rosterList, where wrestlers are stored as 
     */
    public class Roster{
        
        private int count;
        internal string schoolName { get; set; }
        protected internal Dictionary<string, Wrestler>  rosterList;
        
        //Constructor
        public Roster (string schoolName){
            this.schoolName = schoolName;
            this.rosterList = new Dictionary<String, Wrestler>();
            int count = 0;
        }

        //Add Wrestler Method
        protected internal void AddWrestler(int wrestlerID,
            String email,
            String firstName,
            String lastName,
            int grade,
            int skill,
            String gender){
            Wrestler newWrestler = new Wrestler(wrestlerID, email, firstName,lastName,grade,skill,gender, this.schoolName );
            try{
                rosterList.Add(lastName, newWrestler);
                count++;
            }catch (Exception){
                //Notify user of error 
            }
        }
        

        //Get Number of Wrestlers in Roster
        protected internal int getCount(){
            return this.count;
        }

        //Print list of Wrestlers
        protected internal void PrintList(){
            foreach (var item in rosterList){
                Console.WriteLine(item.Value);
            }
        }
        
        public void Editing(string key){
            rosterList[key].editing=true;
            
        }
        
        public void DoneEditing(string key){
            rosterList[key].editing=false;
        }

    }

    /*Class SCHOOL
     * 
     */
    public class School {
        internal int totalEvents;
        internal Roster roster;
        private string Address;
        protected internal string  Name, City, State;
        public School( string schoolName, string city, string state, string address){
            
            this.Name = schoolName;
            this.City =city;
            this.State = state;
            this.roster = new Roster(this.Name);
            this.Address = "insert sql call";
            this.totalEvents = 0;

        }
    }

    
    /*Class PROFILE
     *
     */
    public class Profile{
        internal string email,city,state,schoolName;
        internal School school;
        private string address;
        public LinkedList<Event> Events;

        protected internal Profile(string email, string schoolName, string city, string state, string address) {
            this.email = email;
            this.city=city;
            this.state=state;
            this.schoolName=schoolName;
            this.address = address;
            school = new School(this.schoolName,this.city,this.state, this.address);
        }

        protected internal void emailVerification(){
            //TODO:
            //email verification func
        }

        /*TODO:
        function for setting/resetting/storing password for each account
        */


        protected void addEvent(Event e) {
            Events.Append(e);
        }


    }

    public class Match {

        protected internal string EventID;
        protected internal int ID { get; set; }
        protected internal WrestlerEventInstance wrestler1;
        protected internal WrestlerEventInstance wrestler2;

        public Match(String EventID,int id, WrestlerEventInstance wrestler1, WrestlerEventInstance wrestler2) {
            this.EventID = EventID;
            this.ID = id;
            this.wrestler1 = wrestler1;
            this.wrestler2 = wrestler2;
        }
    }

    /*Class EVENT
     * Object for creating wrestling meet event
     */
    public class Event {
        protected string EventID;
        School host;
        string guestListID;
        protected internal Roster[] guestRosters;
        public List<string> guestList = new List<string>();
        DateTime date;
        protected internal int allowedMatches;

        protected internal double weightDiff;
        protected internal int numMats, skillGap, gradeGap, minMatches, maxMatches;
        protected internal bool internalMatches;

        protected internal string matchListID;
        protected internal LinkedList<Match> matchList;
        
        
        
        protected internal Event(String Id, School host, string guestListId, DateTime date) {
            this.EventID = Id;
            this.host=host;
            this.guestListID = guestListId;
            this.date=date;
            
        }

        protected internal void addToGuestList(string guestName) {
            guestList.Add(guestName);
            
            
            //addGuest(/*TODO: db call to get School Object from guest name (see method below)*/);
        }

        protected internal void addGuest(Roster guestSchoolRoster){
           guestRosters.Append(guestSchoolRoster);
           guestListID = $"{host.Name}_EVENT_{host.totalEvents + 1}";//sets the event name to "SchoolName_EVNT_1"
           /*add db call to add guest email/info to guest list table in DB*/
        }

        private void sendEventNotice(School[] schools){
            foreach (var item in schools)
            {
                //send notification (email/in-app notification) to each school in the guest school list
                //confirmation emails for schools that sign up or general event notices
                
            }
        }
        
    }
}


