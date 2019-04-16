namespace AALife.Core.Migrations
{
    using AALife.Core.Domain.Configuration;
    using AALife.Core.Domain.Messages;
    using AALife.Core.Domain.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AALife.Core.EfContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AALife.Core.EfContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            #region 系统参数

            var root = new List<Parameter>
            {
                new Parameter{ Id = 1, Name = "根目录", Value = "Root", Rank = 1, OrderNo = "01"  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Name }, root.ToArray());
            context.SaveChanges();

            var theme = new List<Parameter>
            {
                new Parameter{ Id = 2, Name = "主题", Value = "Theme", Rank = 1, OrderNo = "01.01", ParentId = 1  },
                new Parameter{ Id = 3, Name = "低调红", Value = "main", Rank = 1, OrderNo = "01.01.01", ParentId = 2, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 4, Name = "土豪金", Value = "gold", Rank = 2, OrderNo = "01.01.02", ParentId = 2, IsLeaf = true },
                new Parameter{ Id = 5, Name = "屌丝蓝", Value = "blue", Rank = 3, OrderNo = "01.01.03", ParentId = 2, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Name }, theme.ToArray());
            context.SaveChanges();

            var itemType = new List<Parameter>
            {
                new Parameter{ Id = 6, Name = "消费分类", Value = "ItemType", Rank = 2, OrderNo = "01.02", ParentId = 1  },
                new Parameter{ Id = 7, Name = "收入", Value = "sr", Rank = 1, OrderNo = "01.02.01", ParentId = 6, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 8, Name = "支出", Value = "zc", Rank = 2, OrderNo = "01.02.02", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 9, Name = "借入", Value = "jr", Rank = 3, OrderNo = "01.02.03", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 10, Name = "借出", Value = "jc", Rank = 4, OrderNo = "01.02.04", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 11, Name = "还入", Value = "hr", Rank = 5, OrderNo = "01.02.05", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 12, Name = "还出", Value = "hc", Rank = 6, OrderNo = "01.02.06", ParentId = 6, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Name }, itemType.ToArray());
            context.SaveChanges();

            var regionType = new List<Parameter>
            {
                new Parameter{ Id = 13, Name = "区间分类", Value = "RegionType", Rank = 3, OrderNo = "01.03", ParentId = 1  },
                new Parameter{ Id = 14, Name = "每日", Value = "d", Rank = 1, OrderNo = "01.03.01", ParentId = 13, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 15, Name = "每周", Value = "w", Rank = 2, OrderNo = "01.03.02", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 16, Name = "每月", Value = "m", Rank = 3, OrderNo = "01.03.03", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 17, Name = "每季", Value = "j", Rank = 4, OrderNo = "01.03.04", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 18, Name = "每年", Value = "y", Rank = 5, OrderNo = "01.03.05", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 19, Name = "工作日", Value = "b", Rank = 6, OrderNo = "01.03.06", ParentId = 13, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, regionType.ToArray());
            context.SaveChanges();

            var workDay = new List<Parameter>
            {
                new Parameter{ Id = 20, Name = "工作日", Value = "WorkDay", Rank = 4, OrderNo = "01.04", ParentId = 1  },
                new Parameter{ Id = 21, Name = "只周一上班", Value = "1", Rank = 1, OrderNo = "01.04.01", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 22, Name = "一到二上班", Value = "2", Rank = 2, OrderNo = "01.04.02", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 23, Name = "一到三上班", Value = "3", Rank = 3, OrderNo = "01.04.03", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 24, Name = "一到四上班", Value = "4", Rank = 4, OrderNo = "01.04.04", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 25, Name = "周末双休", Value = "5", Rank = 5, OrderNo = "01.04.05", ParentId = 20, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 26, Name = "周末单休", Value = "6", Rank = 6, OrderNo = "01.04.06", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 27, Name = "全周无休", Value = "7", Rank = 7, OrderNo = "01.04.07", ParentId = 20, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, workDay.ToArray());
            context.SaveChanges();

            var userFrom = new List<Parameter>
            {
                new Parameter{ Id = 28, Name = "用户来自", Value = "UserFrom", Rank = 5, OrderNo = "01.05", ParentId = 1  },
                new Parameter{ Id = 29, Name = "QQ空间", Value = "qz", Rank = 1, OrderNo = "01.05.01", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 30, Name = "QQ登录", Value = "qq", Rank = 2, OrderNo = "01.05.02", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 31, Name = "朋友网", Value = "py", Rank = 3, OrderNo = "01.05.03", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 32, Name = "手机QQ", Value = "sjqq", Rank = 4, OrderNo = "01.05.04", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 33, Name = "Web QQ", Value = "qw", Rank = 5, OrderNo = "01.05.05", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 34, Name = "网站", Value = "web", Rank = 6, OrderNo = "01.05.06", ParentId = 28, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 35, Name = "手机", Value = "sj", Rank = 7, OrderNo = "01.05.07", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 36, Name = "手机Web", Value = "sjweb", Rank = 8, OrderNo = "01.05.08", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 37, Name = "手机APP", Value = "sjapp", Rank = 9, OrderNo = "01.05.09", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 38, Name = "360手机助手", Value = "360", Rank = 10, OrderNo = "01.05.10", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 39, Name = "百度手机助手", Value = "bd", Rank = 11, OrderNo = "01.05.11", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 40, Name = "联想乐商店", Value = "lx", Rank = 12, OrderNo = "01.05.12", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 41, Name = "小米应用商店", Value = "mi", Rank = 13, OrderNo = "01.05.13", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 42, Name = "腾讯应用宝", Value = "tx", Rank = 14, OrderNo = "01.05.14", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 43, Name = "豌豆荚", Value = "wdj", Rank = 15, OrderNo = "01.05.15", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 44, Name = "应用汇", Value = "yyh", Rank = 16, OrderNo = "01.05.16", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 45, Name = "其它", Value = "qt", Rank = 17, OrderNo = "01.05.17", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 46, Name = "魅族应用商店", Value = "mz", Rank = 18, OrderNo = "01.05.18", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 47, Name = "安卓市场", Value = "az", Rank = 19, OrderNo = "01.05.19", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 48, Name = "APP升级", Value = "upd", Rank = 20, OrderNo = "01.05.20", ParentId = 28, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userFrom.ToArray());
            context.SaveChanges();

            var userLevel = new List<Parameter>
            {
                new Parameter{ Id = 49, Name = "用户等级", Value = "UserLevel", Rank = 6, OrderNo = "01.06", ParentId = 1  },
                new Parameter{ Id = 50, Name = "用户", Value = "1", Rank = 1, OrderNo = "01.06.01", ParentId = 49, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 51, Name = "管理员", Value = "9", Rank = 2, OrderNo = "01.06.02", ParentId = 49, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userLevel.ToArray());
            context.SaveChanges();

            var deptmentCategory = new List<Parameter>
            {
                new Parameter{ Id = 52, Name = "组织类型", Value = "OrgType", Rank = 7, OrderNo = "01.07", ParentId = 1  },
                new Parameter{ Id = 53, Name = "集团", Value = "Group", Rank = 1, OrderNo = "01.07.01", ParentId = 52, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 54, Name = "板块", Value = "Area", Rank = 2, OrderNo = "01.07.02", ParentId = 52, IsLeaf = true  },
                new Parameter{ Id = 55, Name = "公司", Value = "Company", Rank = 3, OrderNo = "01.07.03", ParentId = 52, IsLeaf = true  },
                new Parameter{ Id = 56, Name = "部门", Value = "Deptment", Rank = 4, OrderNo = "01.07.04", ParentId = 52, IsLeaf = true  },
                new Parameter{ Id = 57, Name = "小组", Value = "Class", Rank = 5, OrderNo = "01.07.05", ParentId = 52, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, deptmentCategory.ToArray());
            context.SaveChanges();

            #endregion

            #region 邮件模板

            var template = new MessageTemplate
            {
                Id = 1,
                Name = "测试邮件",
                SystemName = "TestEmail",
                Subject = "这是一封测试邮件",
                Body = "您好，我是一封测试邮件。收到此邮件，说明邮件服务是正常工作的。",
                IsActive = true
            };
            context.Set<MessageTemplate>().AddOrUpdate(m => new { m.SystemName }, template);
            context.SaveChanges();

            #endregion

            #region 定时任务

            var taskMessages = new ScheduleTask
            {
                Id = 1,
                Name = "消息邮件",
                SystemName = "QueuedMessages",
                Type = "AALife.Core.Services.Messages.QueuedMessagesSendTask, AALife.Core",
                Seconds = 60,
                Enabled = true,
                StopOnError = false
            };
            var taskKeepAlive = new ScheduleTask
            {
                Id = 2,
                Name = "保持在线",
                SystemName = "KeepAlive",
                Type = "AALife.Core.Services.Common.KeepAliveTask, AALife.Core",
                Seconds = 300,
                Enabled = true,
                StopOnError = false
            };
            var tasks = new List<ScheduleTask>
            {
                taskMessages,
                taskKeepAlive
            };
            context.Set<ScheduleTask>().AddOrUpdate(m => new { m.SystemName }, tasks.ToArray());
            context.SaveChanges();

            #endregion
        }
    }
}
