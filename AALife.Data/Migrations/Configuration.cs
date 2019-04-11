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
            userRoleSet.AddOrUpdate(m => new { m.SystemName }, userRoles.ToArray());
            context.SaveChanges();

            #endregion

            #region 权限

            var permissions = new List<UserPermission>()
            {
                new UserPermission()
                {
                    Name = "用户管理",
                    AreaName = "Manage",
                    ControllerName = "Users",
                    ActionName = "Index",
                    Rank = 1,
                    OrderNo = "01"
                },
                new UserPermission()
                {
                    Name = "消费管理",
                    AreaName = "Manage",
                    ControllerName = "Items",
                    ActionName = "Index",
                    Rank = 2,
                    OrderNo = "02"
                },
                new UserPermission()
                {
                    Name = "权限管理",
                    AreaName = "Manage",
                    ControllerName = "Permissions",
                    ActionName = "Index",
                    Rank = 3,
                    OrderNo = "03"
                },
                new UserPermission()
                {
                    Name = "角色管理",
                    AreaName = "Manage",
                    ControllerName = "Roles",
                    ActionName = "Index",
                    Rank = 4,
                    OrderNo = "04"
                },
                new UserPermission()
                {
                    Name = "部门管理",
                    AreaName = "Manage",
                    ControllerName = "Deptments",
                    ActionName = "Index",
                    Rank = 5,
                    OrderNo = "05"
                },
                new UserPermission()
                {
                    Name = "岗位管理",
                    AreaName = "Manage",
                    ControllerName = "Positions",
                    ActionName = "Index",
                    Rank = 6,
                    OrderNo = "06"
                },
                new UserPermission()
                {
                    Name = "参数管理",
                    AreaName = "Manage",
                    ControllerName = "Parameters",
                    ActionName = "Index",
                    Rank = 7,
                    OrderNo = "07"
                },
                new UserPermission()
                {
                    Name = "邮件模板",
                    AreaName = "Manage",
                    ControllerName = "MessageTemplates",
                    ActionName = "Index",
                    Rank = 8,
                    OrderNo = "08"
                },
                new UserPermission()
                {
                    Name = "定时任务",
                    AreaName = "Manage",
                    ControllerName = "ScheduleTasks",
                    ActionName = "Index",
                    Rank = 9,
                    OrderNo = "09"
                },
                new UserPermission()
                {
                    Name = "网站管理",
                    AreaName = "Manage",
                    ControllerName = "Settings",
                    ActionName = "Index",
                    Rank = 10,
                    OrderNo = "10"
                }
            };
            var permissionSet = context.Set<UserPermission>();
            permissionSet.AddOrUpdate(m => new { m.Name }, permissions.ToArray());
            context.SaveChanges();

            #endregion

            #region 部门

            var userDept = new UserDeptment
            {
                Name = "总公司",
                Category = "Group"
            };
            var userDeptSet = context.Set<UserDeptment>();
            userDeptSet.AddOrUpdate(m => new { m.Name }, userDept);
            context.SaveChanges();

            #endregion

            #region 岗位

            var userPositions = new List<UserPosition>
            {
                new UserPosition { Name = "员工" },
                new UserPosition { Name = "部门负责人" },
                new UserPosition { Name = "业务副总" },
                new UserPosition { Name = "总经理" }
            };
            var userPositionSet = context.Set<UserPosition>();
            userPositionSet.AddOrUpdate(m => new { m.Name }, userPositions.ToArray());
            context.SaveChanges();

            #endregion

            #region 管理员

            var adminUser = new UserTable()
            {
                UserName = "admin",
                UserPassword = "password",
                UserNickName = "管理员",
                UserTheme = "main",
                UserLevel = 9,
                UserFrom = "web",
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                Synchronize = 1,
                IsAdmin = true
            };
            var userSet = context.Set<UserTable>();
            userSet.AddOrUpdate(m => new { m.UserName }, adminUser);
            context.SaveChanges();

            //设置角色和部门
            var admin = context.UserTables.First(a => a.UserName == "admin");
            var role = context.UserRoles.First(a => a.SystemName == UserRoleNames.Administrators);
            var dept = context.UserDeptments.First(a => a.Name == "开发部");
            admin.UserRoles.Add(role);
            admin.UserDeptments.Add(dept);
            context.SaveChanges();

            #endregion

            #region 默认权限

            var adminRole = context.UserRoles.First(a => a.SystemName == UserRoleNames.Administrators);
            var menus = context.UserPermissions.ToList();
            if (menus != null)
            {
                menus.ForEach(a =>
                {
                    adminRole.UserPermissions.Add(a);
                });
                context.SaveChanges();
            }

            #endregion
        }

    }
}
