using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class SiteFeaturePlan
    {
        public string Id { get; set; }
        public string FeatureDetails { get; set; }
        public bool IsOngoing { get; set; }
        public string ContributingUserId { get; set; }
        public virtual ApplicationUser ContributingUser { get; set; }

    }
}