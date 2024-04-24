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

        //made public for testing 
        public int grade, skill;
        //changed from protected internal to public for testing
        public Wrestler(String email, String firstName, String lastName, int grade, int skill, String gender, String schoolName) {
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
        protected internal void AddWrestler(String email,
            String firstName,
            String lastName,
            int grade,
            int skill,
            String gender){
            Wrestler newWrestler = new Wrestler(email, firstName,lastName,grade,skill,gender, this.schoolName );
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
    public class School
    {
        internal Roster roster;
        private string Address;
        private string  Name, City, State;
        public School( string schoolName, string city, string state, string address){
            
            this.Name = schoolName;
            this.City =city;
            this.State = state;
            this.roster = new Roster(this.Name);
            this.Address = "insert sql call";

        }
    }

    
    /*Class PROFILE
     *
     */
    public class Profile{
        private string email,city,state,schoolName;
        internal School school;
        private string address;

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


    }

    /*Class EVENT
     * Object for creating wrestling meet event
     */
    public class Event
    {
        School host;
        School[] guests;

        DateTime date;
        protected internal Event(School host, DateTime date){
            this.host=host;
            this.date=date;
        }

        private void addGuest(School guestSchool){
           guests.Append(guestSchool);
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


