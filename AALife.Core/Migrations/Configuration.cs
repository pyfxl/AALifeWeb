namespace AALife.Core.Migrations
{
    using AALife.Core.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AALife.Core.EfContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AALife.Core.EfContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //context.UserLevelTables.AddOrUpdate(
            //    u => u.UserLevel,
            //    new UserLevelTable { UserLevelName = "用户", UserLevel = 1 },
            //    new UserLevelTable { UserLevelName = "管理员", UserLevel = 9 }
            //);

            //context.UserFromTables.AddOrUpdate(
            //    u => u.UserFrom,
            //    new UserFromTable { UserFrom = "qz", UserFromName = "QQ空间", Rank = 1 },
            //    new UserFromTable { UserFrom = "qq", UserFromName = "QQ登录", Rank = 2 },
            //    new UserFromTable { UserFrom = "py", UserFromName = "朋友网", Rank = 3 },
            //    new UserFromTable { UserFrom = "sjqq", UserFromName = "手机QQ", Rank = 4 },
            //    new UserFromTable { UserFrom = "qw", UserFromName = "Web QQ", Rank = 5 },
            //    new UserFromTable { UserFrom = "web", UserFromName = "网站", Rank = 6 },
            //    new UserFromTable { UserFrom = "sj", UserFromName = "手机", Rank = 7 },
            //    new UserFromTable { UserFrom = "sjweb", UserFromName = "手机Web", Rank = 8 },
            //    new UserFromTable { UserFrom = "sjapp", UserFromName = "手机APP", Rank = 9 },
            //    new UserFromTable { UserFrom = "360", UserFromName = "360手机助手", Rank = 10 },
            //    new UserFromTable { UserFrom = "bd", UserFromName = "百度手机助手", Rank = 11 },
            //    new UserFromTable { UserFrom = "lx", UserFromName = "联想乐商店", Rank = 12 },
            //    new UserFromTable { UserFrom = "mi", UserFromName = "小米应用商店", Rank = 13 },
            //    new UserFromTable { UserFrom = "tx", UserFromName = "腾讯应用宝", Rank = 14 },
            //    new UserFromTable { UserFrom = "wdj", UserFromName = "豌豆荚", Rank = 15 },
            //    new UserFromTable { UserFrom = "yyh", UserFromName = "应用汇", Rank = 16 },
            //    new UserFromTable { UserFrom = "qt", UserFromName = "其它", Rank = 17 },
            //    new UserFromTable { UserFrom = "mz", UserFromName = "魅族应用商店", Rank = 18 },
            //    new UserFromTable { UserFrom = "az", UserFromName = "安卓市场", Rank = 19 },
            //    new UserFromTable { UserFrom = "upd", UserFromName = "APP升级", Rank = 20 }
            //);
        }
    }
}
