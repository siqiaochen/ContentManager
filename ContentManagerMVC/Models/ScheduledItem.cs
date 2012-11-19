using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentManagerMVC.Models
{
    public class ScheduledItem
    {
        public int ID { get; set; }
        public virtual MediaContent Content { get; set; }
    }
}