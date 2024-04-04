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
        public String firstName, lastName, gender,schoolName;
        public bool editing;

        //made public for testing 
        public int grade, skill;
        //changed from protected internal to public for testing
        public Wrestler(String firstName, String lastName, int grade, int skill, String gender, String schoolName){
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
        //changed to public for testing 
        public int count;
        private String schoolName;
        
        //changed to public for testing
        public Dictionary<String, Wrestler>  rosterList;

        //changed to public for testing (from protected internal)
        public Roster (string schoolName){
            this.schoolName = schoolName;
            this.rosterList = new Dictionary<String, Wrestler>();
            int count = 0;
        }


        //Changed to public for testing
        public void addWrestler(String firstName,String lastName,int grade,int skill,String gender){
            Wrestler newWrestler = new Wrestler(firstName,lastName,grade,skill,gender, this.schoolName );
            try{
                rosterList.Add(lastName, newWrestler);
                count++;
            }catch (Exception){
                //Notify user of error 
            }
        }

        protected internal int getCount(){
            return this.count;
        }

        protected internal void printList(){
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
        Roster roster;
        String username, schoolName, city, state;
        public School(String username, String schoolName, String city, String state){
            this.username = username;
            this.schoolName = schoolName;
            this.city =city;
            this.state = state;
            Roster roster = new Roster(this.schoolName); 

        }
    }

    
    /*Class PROFILE
     *
     */
    public class Profile{
        String email,username,city,state,schoolName;
        School school;

        protected internal Profile(String email, /*String username,*/ String schoolName, String city, String state){
            this.email=email;
            //this.username = username;
            this.city=city;
            this.state=state;
            this.schoolName=schoolName;
            this.school = new School(this.username, this.schoolName,this.city,this.state);
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


