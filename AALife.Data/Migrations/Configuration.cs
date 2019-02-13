namespace AALife.Data.Migrations
{
    using AALife.Data.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AALife.Data.AALifeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AALife.Data.AALifeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var crAdministrators = new UserRole
            {
                Name = "����Ա",
                IsSystemRole = true,
                ModifyDate = DateTime.Now,
                Live = 1,
                SystemName = UserRoleNames.Administrators.ToString(),
            };
            var crRegistered = new UserRole
            {
                Name = "ע���û�",
                IsSystemRole = true,
                ModifyDate = DateTime.Now,
                Live = 1,
                SystemName = UserRoleNames.Registered.ToString(),
            };
            var crGuests = new UserRole
            {
                Name = "�ο�",
                IsSystemRole = true,
                ModifyDate = DateTime.Now,
                Live = 1,
                SystemName = UserRoleNames.Guests.ToString(),
            };
            var userRoles = new List<UserRole>
            {
                crAdministrators,
                crRegistered,
                crGuests
            };
            var userRoleSet = context.Set<UserRole>();
            userRoleSet.AddOrUpdate(m => new { m.Name }, userRoles.ToArray());
            context.SaveChanges();

        }
    }
}
