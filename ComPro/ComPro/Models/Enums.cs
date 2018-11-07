using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class Enums
    {
        public enum UserRole
        {
            
            Administrator,
            User,
            NewUser,
           
        }

        

        public enum Searching
        {
            Priority0 =0,
            Priority1 = 1,

        }

        public enum EventType
        {
            All,
            Admin,
            Creator,

        }

        public enum PerticipentType
        {
            NotGoing =0,
            Seen =3,
            Maybe=2,
            Going=1, 
            NotResponsed=5,
            Public=4,

        }

        public enum Gender
        {
            Male,
            Female,
        }
       
    }

    public enum Status
    {
        Student,
        On_Job,
        Job_Seeker,
        Planning
    }
}