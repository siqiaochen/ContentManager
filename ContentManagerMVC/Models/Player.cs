using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ContentManagerMVC.Models
{
    public class Player
    {
        //public enum PlayerStatus {off = 0, on =1};
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string IP { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PlayerSchedule> Schedules { get; set; }
    }
    public class PlayerDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<MediaContent> Contents { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<PlayerSchedule> PlayerSchedules { get; set; }

        public DbSet<ScheduledItem> ScheduledItems { get; set; }
    }
}