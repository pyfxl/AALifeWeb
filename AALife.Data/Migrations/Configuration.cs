namespace AALife.Data.Migrations
{
    using AALife.Core.Infrastructure;
    using AALife.Data.Domain;
    using AALife.Data.Services;
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

            #region 角色

            var crAdministrators = new UserRole
            {
                Name = "管理员",
                SystemName = UserRoleNames.Administrators
            };
            var crRegistered = new UserRole
            {
                Name = "注册用户",
                SystemName = UserRoleNames.Registered
            };
            var crGuests = new UserRole
            {
                Name = "游客",
                SystemName = UserRoleNames.Guests
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

            #endregion

            #region 权限

            var permissions = new List<PermissionRecord>()
            {
                new PermissionRecord()
                {
                    Id = 1,
                    Name = "用户管理",
                    AreaName = "Manage",
                    ControllerName = "Users",
                    ActionName = "Index",
                    Rank = 1,
                    OrderNo = "01"
                },
                new PermissionRecord()
                {
                    Id = 2,
                    Name = "角色管理",
                    AreaName = "Manage",
                    ControllerName = "Roles",
                    ActionName = "Index",
                    Rank = 4,
                    OrderNo = "04"
                },
                new PermissionRecord()
                {
                    Id = 4,
                    Name = "权限管理",
                    AreaName = "Manage",
                    ControllerName = "Permissions",
                    ActionName = "Index",
                    Rank = 3,
                    OrderNo = "03"
                },
                new PermissionRecord()
                {
                    Id = 3,
                    Name = "消费管理",
                    AreaName = "Manage",
                    ControllerName = "Items",
                    ActionName = "Index",
                    Rank = 2,
                    OrderNo = "02"
                }
            };

            var permissionSet = context.Set<PermissionRecord>();
            permissionSet.AddOrUpdate(m => new { m.Id }, permissions.ToArray());
            context.SaveChanges();

            #endregion

            #region 管理员

            var admin = new UserTable()
            {
                Id = 2,
                UserName = "admin",
                UserPassword = "admin",
                UserNickName = "管理员",
                UserTheme = "main",
                UserLevel = 9,
                UserFrom = "web",
                CreateDate = DateTime.Now,
                Synchronize = 1,
                UserRoles = userRoles
            };

            var userSet = context.Set<UserTable>();
            userSet.AddOrUpdate(m => m.Id, admin);
            context.SaveChanges();

            #endregion

            #region 默认权限

            var adminRole = context.UserRoles.First(a => a.SystemName == UserRoleNames.Administrators);
            var menus = context.PermissionRecords.ToList();
            if (menus != null)
            {
                menus.ForEach(a =>
                {
                    adminRole.PermissionRecords.Add(a);
                });
                context.SaveChanges();
            }

            #endregion
        }

    }
}
