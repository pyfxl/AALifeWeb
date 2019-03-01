namespace AALife.Core.Migrations
{
    using AALife.Core.Domain.Configuration;
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

            var theme = new List<Parameter>
            {
                new Parameter{ Id = 1, Name = "主题", SystemName = "Theme", Value = "Theme", Rank = 1, OrderNo = "1"  },
                new Parameter{ Id = 2, Name = "低调红", SystemName = "main".ToUpper(), Value = "main", Rank = 1, OrderNo = "1.1", ParentId = 1, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 3, Name = "土豪金", SystemName = "gold".ToUpper(), Value = "gold", Rank = 2, OrderNo = "1.2", ParentId = 1, IsLeaf = true },
                new Parameter{ Id = 4, Name = "屌丝蓝", SystemName = "blue".ToUpper(), Value = "blue", Rank = 3, OrderNo = "1.3", ParentId = 1, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, theme.ToArray());
            context.SaveChanges();

            var itemType = new List<Parameter>
            {
                new Parameter{ Id = 5, Name = "消费分类", SystemName = "ItemType", Value = "ItemType", Rank = 2, OrderNo = "2"  },
                new Parameter{ Id = 6, Name = "收入", SystemName = "sr".ToUpper(), Value = "sr", Rank = 1, OrderNo = "2.1", ParentId = 5, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 7, Name = "支出", SystemName = "zc".ToUpper(), Value = "zc", Rank = 2, OrderNo = "2.2", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 8, Name = "借入", SystemName = "jr".ToUpper(), Value = "jr", Rank = 3, OrderNo = "2.3", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 9, Name = "借出", SystemName = "jc".ToUpper(), Value = "jc", Rank = 4, OrderNo = "2.4", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 10, Name = "还入", SystemName = "hr".ToUpper(), Value = "hr", Rank = 5, OrderNo = "2.5", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 11, Name = "还出", SystemName = "hc".ToUpper(), Value = "hc", Rank = 6, OrderNo = "2.6", ParentId = 5, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, itemType.ToArray());
            context.SaveChanges();

            var regionType = new List<Parameter>
            {
                new Parameter{ Id = 12, Name = "区间分类", SystemName = "RegionType", Value = "RegionType", Rank = 3, OrderNo = "3"  },
                new Parameter{ Id = 13, Name = "每日", SystemName = "d".ToUpper(), Value = "d", Rank = 1, OrderNo = "3.1", ParentId = 12, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 14, Name = "每周", SystemName = "w".ToUpper(), Value = "w", Rank = 2, OrderNo = "3.2", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 15, Name = "每月", SystemName = "m".ToUpper(), Value = "m", Rank = 3, OrderNo = "3.3", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 16, Name = "每季", SystemName = "j".ToUpper(), Value = "j", Rank = 4, OrderNo = "3.4", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 17, Name = "每年", SystemName = "y".ToUpper(), Value = "y", Rank = 5, OrderNo = "3.5", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 18, Name = "工作日", SystemName = "b".ToUpper(), Value = "b", Rank = 6, OrderNo = "3.6", ParentId = 12, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, regionType.ToArray());
            context.SaveChanges();

            var workDay = new List<Parameter>
            {
                new Parameter{ Id = 19, Name = "工作日", SystemName = "WorkDay", Value = "WorkDay", Rank = 4, OrderNo = "4"  },
                new Parameter{ Id = 20, Name = "只周一上班", SystemName = "1", Value = "1", Rank = 1, OrderNo = "4.1", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 21, Name = "一到二上班", SystemName = "2", Value = "2", Rank = 2, OrderNo = "4.2", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 22, Name = "一到三上班", SystemName = "3", Value = "3", Rank = 3, OrderNo = "4.3", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 23, Name = "一到四上班", SystemName = "4", Value = "4", Rank = 4, OrderNo = "4.4", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 24, Name = "周末双休", SystemName = "5", Value = "5", Rank = 5, OrderNo = "4.5", ParentId = 19, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 25, Name = "周末单休", SystemName = "6", Value = "6", Rank = 6, OrderNo = "4.6", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 26, Name = "全周无休", SystemName = "7", Value = "7", Rank = 7, OrderNo = "4.7", ParentId = 19, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, workDay.ToArray());
            context.SaveChanges();

            var userFrom = new List<Parameter>
            {
                new Parameter{ Id = 27, Name = "用户来自", SystemName = "UserFrom", Value = "UserFrom", Rank = 5, OrderNo = "5"  },
                new Parameter{ Id = 28, Name = "QQ空间", SystemName = "qz".ToUpper(), Value = "qz", Rank = 1, OrderNo = "5.1", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 29, Name = "QQ登录", SystemName = "qq".ToUpper(), Value = "qq", Rank = 2, OrderNo = "5.2", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 30, Name = "朋友网", SystemName = "py".ToUpper(), Value = "py", Rank = 3, OrderNo = "5.3", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 31, Name = "手机QQ", SystemName = "sjqq".ToUpper(), Value = "sjqq", Rank = 4, OrderNo = "5.4", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 32, Name = "Web QQ", SystemName = "qw".ToUpper(), Value = "qw", Rank = 5, OrderNo = "5.5", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 33, Name = "网站", SystemName = "web".ToUpper(), Value = "web", Rank = 6, OrderNo = "5.6", ParentId = 27, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 34, Name = "手机", SystemName = "sj".ToUpper(), Value = "sj", Rank = 7, OrderNo = "5.7", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 35, Name = "手机Web", SystemName = "sjweb".ToUpper(), Value = "sjweb", Rank = 8, OrderNo = "5.8", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 36, Name = "手机APP", SystemName = "sjapp".ToUpper(), Value = "sjapp", Rank = 9, OrderNo = "5.9", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 37, Name = "360手机助手", SystemName = "360".ToUpper(), Value = "360", Rank = 10, OrderNo = "5.10", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 38, Name = "百度手机助手", SystemName = "bd".ToUpper(), Value = "bd", Rank = 11, OrderNo = "5.11", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 39, Name = "联想乐商店", SystemName = "lx".ToUpper(), Value = "lx", Rank = 12, OrderNo = "5.12", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 40, Name = "小米应用商店", SystemName = "mi".ToUpper(), Value = "mi", Rank = 13, OrderNo = "5.13", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 41, Name = "腾讯应用宝", SystemName = "tx".ToUpper(), Value = "tx", Rank = 14, OrderNo = "5.14", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 42, Name = "豌豆荚", SystemName = "wdj".ToUpper(), Value = "wdj", Rank = 15, OrderNo = "5.15", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 43, Name = "应用汇", SystemName = "yyh".ToUpper(), Value = "yyh", Rank = 16, OrderNo = "5.16", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 44, Name = "其它", SystemName = "qt".ToUpper(), Value = "qt", Rank = 17, OrderNo = "5.17", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 45, Name = "魅族应用商店", SystemName = "mz".ToUpper(), Value = "mz", Rank = 18, OrderNo = "5.18", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 46, Name = "安卓市场", SystemName = "az".ToUpper(), Value = "az", Rank = 19, OrderNo = "5.19", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 47, Name = "APP升级", SystemName = "upd".ToUpper(), Value = "upd", Rank = 20, OrderNo = "5.20", ParentId = 27, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userFrom.ToArray());
            context.SaveChanges();

            var userLevel = new List<Parameter>
            {
                new Parameter{ Id = 48, Name = "用户等级", SystemName = "UserLevel", Value = "UserLevel", Rank = 6, OrderNo = "6"  },
                new Parameter{ Id = 49, Name = "用户", SystemName = "1", Value = "1", Rank = 1, OrderNo = "6.1", ParentId = 48, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 50, Name = "管理员", SystemName = "9", Value = "9", Rank = 2, OrderNo = "6.2", ParentId = 48, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userLevel.ToArray());
            context.SaveChanges();
        }
    }
}
