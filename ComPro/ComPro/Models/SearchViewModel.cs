using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class SearchViewModel
    {
       public int ResultId { get; set; }

        public string ResultName { get; set; }

        public string Description { get; set; }

        public string ResultCatagory { get; set; }

        public string MatchedText { get; set; }

        public int Priority { get; set; }

        public string SearchText { get; set; }

    }
}