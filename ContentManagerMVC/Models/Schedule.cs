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
        public Boolean Mon { get; set; }
        public Boolean Tue { get; set; }
        public Boolean Wed { get; set; }
        public Boolean Thr { get; set; }
        public Boolean Fri { get; set; }
        public Boolean Sat { get; set; }
        public Boolean Sun { get; set; }
        public virtual ICollection<ScheduledItem> Contents { get; set; }

        // public IEnumerable<Play> Contents { get; set; }
    }

}