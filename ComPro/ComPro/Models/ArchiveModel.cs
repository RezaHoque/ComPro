using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class ArchiveModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
       // public int? EventId { get; set; }
        //public virtual EventModel Event { get; set; }
        
        public DateTime ActionDate { get; set; }
        public bool IsVisible { get; set; }
        public DateTime UploadDate { get; set; }

        public ICollection<ArchiveFilesModel> Files { get; set; }

    }

    public class ArchiveFilesModel
    {
        public string Id { get; set; }
        public string FilePath { get; set; }
        public string ArchiveId { get; set; }
        public virtual ArchiveModel Archive { get; set; }
    }
}