using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Customers;
using AALife.Core.Infrastructure;
using AALife.Core.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace AALife.Core.Initializers
{
    public class MyDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<EfContext>
    {
        protected override void Seed(EfContext context)
        {
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
            adminUserSet.Add(adminUser);
            context.SaveChanges();

            //settings
            var settingService = EngineContext.Current.Resolve<ISettingService>();
            settingService.SaveSetting(new CommonSettings
            {
                SiteName = Constant.SiteName,
                SiteAuthor = Constant.SiteAuthor,
                SiteKeywords = Constant.SiteKeywords,
                SiteDescription = Constant.SiteDescription,
                PageNumber = Constant.PageNumber,
                PageNumberArrays = Constant.PageNumberArrays
            });

            base.Seed(context);
        }
    }
}
