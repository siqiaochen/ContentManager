namespace ContentManagerMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebMatrix.WebData;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<ContentManagerMVC.Models.PlayerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ContentManagerMVC.Models.PlayerDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            

            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Administrator"))
                roles.CreateRole("Administrator");
            if (!roles.RoleExists("Manager"))
                roles.CreateRole("Manager");
            if (!roles.RoleExists("GraphicCreater"))
                roles.CreateRole("GraphicCreater");
            if (!roles.RoleExists("Banned"))
                roles.CreateRole("Banned");
            if (membership.GetUser("admin", false) == null)
            {
                membership.CreateUserAndAccount("admin", "password123");
            }
            if (!roles.GetRolesForUser("admin").Contains("Administrator"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Administrator" });
               
            }

        }
    }
}
