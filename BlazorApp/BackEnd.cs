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
        private string email,city,state,schoolName;
        internal School school;
        private string address;
        private List<string> Events;

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


        private void addEvent(string eventID){
            Events.Add(eventID);
            
        }


    }

    /*Class EVENT
     * Object for creating wrestling meet event
     */
    public class Event {
        protected string ID;
        School host;
        string guestListID;
        protected School[] guests;
        public List<string> guestList = new List<string>();
        DateTime date;
        
        protected internal Event(String Id, School host, string guestListId, DateTime date) {
            this.ID = Id;
            this.host=host;
            this.guestListID = guestListId;
            this.date=date;
            
        }

        protected internal void addToGuestList(string guestName) {
            guestList.Add(guestName);
        }

        protected internal void addGuest(School guestSchool){
           guests.Append(guestSchool);
           guestListID = $"{host.Name}_EVNT_{host.totalEvents + 1}";//sets the event name to "SchoolName_EVNT_1"
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


