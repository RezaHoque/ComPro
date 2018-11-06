using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class PollingAndSyrvayModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }


        public DateTime Creation { get; set; }
        public string CreatorId { get; set; }

        public bool Status { get; set; }

        public bool IsApproved { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool IsPublic { get; set; }


    }

    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; }

        public string Question { get; set; }

    }

    public class AnswerModel
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public string Answer { get; set; }

    }

    public class PerticipentModel
    {
        [Key]
        public int Id { get; set; }

        public int ActivityId { get; set; }
        public string PerticipentId { get; set; }
        public int AnswerId { get; set; }

    }


    public class SurveyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       
        public List <QA> QA { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }

    public class QA
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Q_A { get; set; }
        public int Result { get; set; }

    }

   

    public class PollViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Question { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }




}