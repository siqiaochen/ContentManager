using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ContentManagerMVC.Models
{
    public class MediaContent
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string ContentType { get; set; }
        public decimal Duration { get; set; }
        public string Path { get; set; }
    }
}