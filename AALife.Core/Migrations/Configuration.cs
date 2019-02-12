namespace AALife.Core.Migrations
{
    using AALife.Core.Domain.Customers;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EfContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EfContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var crAdministrators = new CustomerRole
            {
                Name = "管理员",
                Active = true,
                IsSystemRole = true,
                SystemName = CustomerRoleNames.Administrators.ToString(),
            };
            var crRegistered = new CustomerRole
            {
                Name = "注册用户",
                Active = true,
                IsSystemRole = true,
                SystemName = CustomerRoleNames.Registered.ToString(),
            };
            var crGuests = new CustomerRole
            {
                Name = "游客",
                Active = true,
                IsSystemRole = true,
                SystemName = CustomerRoleNames.Guests.ToString(),
            };
            var customerRoles = new List<CustomerRole>
            {
                crAdministrators,
                crRegistered,
                crGuests
            };
            var customerRoleSet = context.Set<CustomerRole>();
            customerRoleSet.AddOrUpdate(m => new { m.Name }, customerRoles.ToArray());
            context.SaveChanges();

            //admin user
            var adminUser = new Customer
            {
                Username = "admin",
                ResetPassword = true,
                Active = true,
                CreatedDate = DateTime.Now
            };
            adminUser.CustomerRoles.Add(crAdministrators);
            var adminUserSet = context.Set<Customer>();
            adminUserSet.AddOrUpdate(m => new { m.Username }, adminUser);
            context.SaveChanges();

            //settings
            

            base.Seed(context);
        }
    }
}
