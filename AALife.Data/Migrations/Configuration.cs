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

            #region ��ɫ

            var crAdministrators = new UserRole
            {
                Name = "����Ա",
                SystemName = UserRoleNames.Administrators
            };
            var crRegistered = new UserRole
            {
                Name = "ע���û�",
                SystemName = UserRoleNames.Registered
            };
            var crGuests = new UserRole
            {
                Name = "�ο�",
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

            #region Ȩ��

            var permissions = new List<UserPermission>()
            {
                new UserPermission()
                {
                    Name = "�û�����",
                    AreaName = "Manage",
                    ControllerName = "Users",
                    ActionName = "Index",
                    Rank = 1,
                    OrderNo = "01"
                },
                new UserPermission()
                {
                    Name = "���ѹ���",
                    AreaName = "Manage",
                    ControllerName = "Items",
                    ActionName = "Index",
                    Rank = 2,
                    OrderNo = "02"
                },
                new UserPermission()
                {
                    Name = "Ȩ�޹���",
                    AreaName = "Manage",
                    ControllerName = "Permissions",
                    ActionName = "Index",
                    Rank = 3,
                    OrderNo = "03"
                },
                new UserPermission()
                {
                    Name = "��ɫ����",
                    AreaName = "Manage",
                    ControllerName = "Roles",
                    ActionName = "Index",
                    Rank = 4,
                    OrderNo = "04"
                },
                new UserPermission()
                {
                    Name = "���Ź���",
                    AreaName = "Manage",
                    ControllerName = "Deptments",
                    ActionName = "Index",
                    Rank = 5,
                    OrderNo = "05"
                },
                new UserPermission()
                {
                    Name = "��λ����",
                    AreaName = "Manage",
                    ControllerName = "Positions",
                    ActionName = "Index",
                    Rank = 6,
                    OrderNo = "06"
                },
                new UserPermission()
                {
                    Name = "��������",
                    AreaName = "Manage",
                    ControllerName = "Parameters",
                    ActionName = "Index",
                    Rank = 7,
                    OrderNo = "07"
                },
                new UserPermission()
                {
                    Name = "�ʼ�ģ��",
                    AreaName = "Manage",
                    ControllerName = "MessageTemplates",
                    ActionName = "Index",
                    Rank = 8,
                    OrderNo = "08"
                },
                new UserPermission()
                {
                    Name = "��ʱ����",
                    AreaName = "Manage",
                    ControllerName = "ScheduleTasks",
                    ActionName = "Index",
                    Rank = 9,
                    OrderNo = "09"
                },
                new UserPermission()
                {
                    Name = "��վ����",
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

            #region ����

            var userDept = new UserDeptment
            {
                Name = "�ܹ�˾",
                Category = "Group"
            };
            var userDeptSet = context.Set<UserDeptment>();
            userDeptSet.AddOrUpdate(m => new { m.Name }, userDept);
            context.SaveChanges();

            #endregion

            #region ��λ

            var userPositions = new List<UserPosition>
            {
                new UserPosition { Name = "Ա��" },
                new UserPosition { Name = "���Ÿ�����" },
                new UserPosition { Name = "ҵ����" },
                new UserPosition { Name = "�ܾ���" }
            };
            var userPositionSet = context.Set<UserPosition>();
            userPositionSet.AddOrUpdate(m => new { m.Name }, userPositions.ToArray());
            context.SaveChanges();

            #endregion

            #region ����Ա

            var adminUser = new UserTable()
            {
                UserName = "admin",
                UserPassword = "password",
                UserNickName = "����Ա",
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

            //���ý�ɫ�Ͳ���
            var admin = context.UserTables.First(a => a.UserName == "admin");
            var role = context.UserRoles.First(a => a.SystemName == UserRoleNames.Administrators);
            var dept = context.UserDeptments.First(a => a.Name == "������");
            admin.UserRoles.Add(role);
            admin.UserDeptments.Add(dept);
            context.SaveChanges();

            #endregion

            #region Ĭ��Ȩ��

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
