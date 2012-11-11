using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ContentManagerMVC.Models
{
    public class PlayerInitializer
    {
        public class MovieInitializer : DropCreateDatabaseIfModelChanges<PlayerDBContext>
        { 
        }
    }
}