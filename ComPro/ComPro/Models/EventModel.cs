using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComPro.Models
{
    public class EventModel
    {
            [Key]
            public int EventId { get; set; }

            [Required]
            public string Title { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            public DateTime Date { get; set; }

            public DateTime Creation { get; set; }
            public string CreatorId { get; set; }

            public string Place { get; set; }

            public bool EventStatus { get; set; }

            public bool IsApproved { get; set; }
            public DateTime ApprovalDate { get; set; }
            public bool IsPublic { get; set; }
            public DateTime End { get; set; }
            public string UniqueUrl { get; set; }
             
        }

    public class EventMember
    {
        [Key]
        public int Id { get; set; }

        //public string EventID { get; set; }
        public virtual ApplicationUser Member { get; set; }
        public string MemberID { get; set; }

        public string PerticipetingType { get; set; }
        public DateTime ResponseDate { get; set; }

        // Foreign key
        public int EventId { get; set; }

    }

    

    public class EventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Event Titel")]
        public string EventTitel { get; set; }
        public bool Approval { get; set; }
        public string CreatorID { get; set; }
        public string CreatorName { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string Place { get; set; }
        public List<SiteImage> Images { get; set; }
        public int TotalYes { get; set; }
        public ICollection<EventMember> Members { get; set; }
        public string UniqueUrl { get; set; }


    }

    public class DetailViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
                
        public string Description { get; set; }
                
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }

        public string Place { get; set; }

        public int Members { get; set; }
        public int Going { get; set; }
        public int NotGoing { get; set; }
        public int Maybe { get; set; }
        public int Seen { get; set; }


        [Display(Name = "Creation Date")]
        public DateTime Creation { get; set; }

        [Display(Name = "Creator Name")]
        public string CreatorName { get; set; }

        

        [Display(Name = "Approved On")]
        public DateTime ApprovalDate { get; set; }
        public string CreatorId { get; set; }

        public  List<EventMember> MembersList { get; set; }

        public bool IsApproved { get; set; }
        public string UserActivity { get; set; }
        public string UniqueUrl { get; set; }
        public List<SiteImage> Images { get; set; }


    }

    public class MemberViewModel
    {
        public string Name { get; set; }

        public string ID { get; set; }
    }

    public class EventCalanderViewModel
    {
        public int EventId { get; set; }
        public string EventTitel { get; set; }
        public DateTime Date { get; set; }
        public string Month { get; set; }

    }
}