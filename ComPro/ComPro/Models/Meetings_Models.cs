using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class Meetings_Models
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Creator_Id { get; set; }
        public string Creator_Name { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Meeting_Date { get; set; }
        public String Description { get; set; }
        
        public string Perticipents_Id { get; set; }
        public string Perticipents_Name { get; set; }


        
    }


    public class MeetingViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Meeting_Date { get; set; }
        public string Creator_Id { get; set; }

    }


    public class MeetingDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator_Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Creation_Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Meeting_Date { get; set; }
        public String Description { get; set; }
        public string Perticipents_Name { get; set; }
    }

    public class MeetingEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Meeting_Date { get; set; }
        public String Description { get; set; }
        
    }

}