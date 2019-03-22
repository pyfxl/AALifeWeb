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
                new Parameter{ Id = 1, Name = "根目录", SystemName = "Root", Value = "Root", Rank = 1, OrderNo = "1"  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, root.ToArray());
            context.SaveChanges();

            var theme = new List<Parameter>
            {
                new Parameter{ Id = 2, Name = "主题", SystemName = "Theme", Value = "Theme", Rank = 1, OrderNo = "1.1", ParentId = 1  },
                new Parameter{ Id = 3, Name = "低调红", SystemName = "main", Value = "main", Rank = 1, OrderNo = "2.1", ParentId = 2, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 4, Name = "土豪金", SystemName = "gold", Value = "gold", Rank = 2, OrderNo = "2.2", ParentId = 2, IsLeaf = true },
                new Parameter{ Id = 5, Name = "屌丝蓝", SystemName = "blue", Value = "blue", Rank = 3, OrderNo = "2.3", ParentId = 2, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, theme.ToArray());
            context.SaveChanges();

            var itemType = new List<Parameter>
            {
                new Parameter{ Id = 6, Name = "消费分类", SystemName = "ItemType", Value = "ItemType", Rank = 2, OrderNo = "1.2", ParentId = 1  },
                new Parameter{ Id = 7, Name = "收入", SystemName = "sr", Value = "sr", Rank = 1, OrderNo = "6.1", ParentId = 6, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 8, Name = "支出", SystemName = "zc", Value = "zc", Rank = 2, OrderNo = "6.2", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 9, Name = "借入", SystemName = "jr", Value = "jr", Rank = 3, OrderNo = "6.3", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 10, Name = "借出", SystemName = "jc", Value = "jc", Rank = 4, OrderNo = "6.4", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 11, Name = "还入", SystemName = "hr", Value = "hr", Rank = 5, OrderNo = "6.5", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 12, Name = "还出", SystemName = "hc", Value = "hc", Rank = 6, OrderNo = "6.6", ParentId = 6, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, itemType.ToArray());
            context.SaveChanges();

            var regionType = new List<Parameter>
            {
                new Parameter{ Id = 13, Name = "区间分类", SystemName = "RegionType", Value = "RegionType", Rank = 3, OrderNo = "1.3", ParentId = 1  },
                new Parameter{ Id = 14, Name = "每日", SystemName = "d", Value = "d", Rank = 1, OrderNo = "13.1", ParentId = 13, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 15, Name = "每周", SystemName = "w", Value = "w", Rank = 2, OrderNo = "13.2", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 16, Name = "每月", SystemName = "m", Value = "m", Rank = 3, OrderNo = "13.3", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 17, Name = "每季", SystemName = "j", Value = "j", Rank = 4, OrderNo = "13.4", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 18, Name = "每年", SystemName = "y", Value = "y", Rank = 5, OrderNo = "13.5", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 19, Name = "工作日", SystemName = "b", Value = "b", Rank = 6, OrderNo = "13.6", ParentId = 13, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, regionType.ToArray());
            context.SaveChanges();

            var workDay = new List<Parameter>
            {
                new Parameter{ Id = 20, Name = "工作日", SystemName = "WorkDay", Value = "WorkDay", Rank = 4, OrderNo = "1.4", ParentId = 1  },
                new Parameter{ Id = 21, Name = "只周一上班", SystemName = "1", Value = "1", Rank = 1, OrderNo = "20.1", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 22, Name = "一到二上班", SystemName = "2", Value = "2", Rank = 2, OrderNo = "20.2", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 23, Name = "一到三上班", SystemName = "3", Value = "3", Rank = 3, OrderNo = "20.3", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 24, Name = "一到四上班", SystemName = "4", Value = "4", Rank = 4, OrderNo = "20.4", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 25, Name = "周末双休", SystemName = "5", Value = "5", Rank = 5, OrderNo = "20.5", ParentId = 20, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 26, Name = "周末单休", SystemName = "6", Value = "6", Rank = 6, OrderNo = "20.6", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 27, Name = "全周无休", SystemName = "7", Value = "7", Rank = 7, OrderNo = "20.7", ParentId = 20, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, workDay.ToArray());
            context.SaveChanges();

            var userFrom = new List<Parameter>
            {
                new Parameter{ Id = 28, Name = "用户来自", SystemName = "UserFrom", Value = "UserFrom", Rank = 5, OrderNo = "1.5", ParentId = 1  },
                new Parameter{ Id = 29, Name = "QQ空间", SystemName = "qz", Value = "qz", Rank = 1, OrderNo = "28.1", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 30, Name = "QQ登录", SystemName = "qq", Value = "qq", Rank = 2, OrderNo = "28.2", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 31, Name = "朋友网", SystemName = "py", Value = "py", Rank = 3, OrderNo = "28.3", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 32, Name = "手机QQ", SystemName = "sjqq", Value = "sjqq", Rank = 4, OrderNo = "28.4", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 33, Name = "Web QQ", SystemName = "qw", Value = "qw", Rank = 5, OrderNo = "28.5", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 34, Name = "网站", SystemName = "web", Value = "web", Rank = 6, OrderNo = "28.6", ParentId = 28, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 35, Name = "手机", SystemName = "sj", Value = "sj", Rank = 7, OrderNo = "28.7", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 36, Name = "手机Web", SystemName = "sjweb", Value = "sjweb", Rank = 8, OrderNo = "28.8", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 37, Name = "手机APP", SystemName = "sjapp", Value = "sjapp", Rank = 9, OrderNo = "28.9", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 38, Name = "360手机助手", SystemName = "360", Value = "360", Rank = 10, OrderNo = "28.10", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 39, Name = "百度手机助手", SystemName = "bd", Value = "bd", Rank = 11, OrderNo = "28.11", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 40, Name = "联想乐商店", SystemName = "lx", Value = "lx", Rank = 12, OrderNo = "28.12", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 41, Name = "小米应用商店", SystemName = "mi", Value = "mi", Rank = 13, OrderNo = "28.13", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 42, Name = "腾讯应用宝", SystemName = "tx", Value = "tx", Rank = 14, OrderNo = "28.14", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 43, Name = "豌豆荚", SystemName = "wdj", Value = "wdj", Rank = 15, OrderNo = "28.15", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 44, Name = "应用汇", SystemName = "yyh", Value = "yyh", Rank = 16, OrderNo = "28.16", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 45, Name = "其它", SystemName = "qt", Value = "qt", Rank = 17, OrderNo = "28.17", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 46, Name = "魅族应用商店", SystemName = "mz", Value = "mz", Rank = 18, OrderNo = "28.18", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 47, Name = "安卓市场", SystemName = "az", Value = "az", Rank = 19, OrderNo = "28.19", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 48, Name = "APP升级", SystemName = "upd", Value = "upd", Rank = 20, OrderNo = "28.20", ParentId = 28, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userFrom.ToArray());
            context.SaveChanges();

            var userLevel = new List<Parameter>
            {
                new Parameter{ Id = 49, Name = "用户等级", SystemName = "UserLevel", Value = "UserLevel", Rank = 6, OrderNo = "1.6", ParentId = 1  },
                new Parameter{ Id = 50, Name = "用户", SystemName = "1", Value = "1", Rank = 1, OrderNo = "49.1", ParentId = 49, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 51, Name = "管理员", SystemName = "9", Value = "9", Rank = 2, OrderNo = "49.2", ParentId = 49, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userLevel.ToArray());
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
            context.Set<MessageTemplate>().AddOrUpdate(m => new { m.Id }, template);
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
            context.Set<ScheduleTask>().AddOrUpdate(m => new { m.Id }, tasks.ToArray());
            context.SaveChanges();

            #endregion
        }
    }
}
