using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentManagerMVC.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean Continuous { get; set; }
        public Boolean PlayOnePerRound { get; set; } 
        public IEnumerable<MediaContent> Contents { get; set; }
    }

}