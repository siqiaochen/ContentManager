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
        public string IP { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }


    }
    public class PlayerDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<MediaContent> Contents { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
        
    }
}