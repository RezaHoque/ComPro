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

        [Required]
        public string Title { get; set; }

       
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

        public string PerticipentId { get; set; }
        public int AnswerId { get; set; }

    }

    //public class ResultViewModel
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    public string Title { get; set; }

    //   List<QuestionAndAnswerViewModel> QA  { get; set; }



    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }

    //    [Display(Name = "Creation Date")]
    //    public DateTime Creation { get; set; }

    //    [Display(Name = "Creator Name")]
    //    public string CreatorName { get; set; }


    //    [Display(Name = "Approved On")]
    //    public DateTime ApprovalDate { get; set; }
    //    public string CreatorId { get; set; }

    //    public List<EventMember> MembersList { get; set; }



    //}

    //public class QuestionAndAnswerViewModel
    //{
    //    public string Question { get; set; }
    //    public string Answer1 { get; set; }
    //    public string Agreed1 { get; set; }

    //    public string Answer2 { get; set; }
    //    public string Agreed2 { get; set; }



    //}

    public class SurveyViewModel
    {
       
        public string Title { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


}