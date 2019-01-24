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
            //    new UserLevelTable { UserLevelName = "�û�", UserLevel = 1 },
            //    new UserLevelTable { UserLevelName = "����Ա", UserLevel = 9 }
            //);

            //context.UserFromTables.AddOrUpdate(
            //    u => u.UserFrom,
            //    new UserFromTable { UserFrom = "qz", UserFromName = "QQ�ռ�", Rank = 1 },
            //    new UserFromTable { UserFrom = "qq", UserFromName = "QQ��¼", Rank = 2 },
            //    new UserFromTable { UserFrom = "py", UserFromName = "������", Rank = 3 },
            //    new UserFromTable { UserFrom = "sjqq", UserFromName = "�ֻ�QQ", Rank = 4 },
            //    new UserFromTable { UserFrom = "qw", UserFromName = "Web QQ", Rank = 5 },
            //    new UserFromTable { UserFrom = "web", UserFromName = "��վ", Rank = 6 },
            //    new UserFromTable { UserFrom = "sj", UserFromName = "�ֻ�", Rank = 7 },
            //    new UserFromTable { UserFrom = "sjweb", UserFromName = "�ֻ�Web", Rank = 8 },
            //    new UserFromTable { UserFrom = "sjapp", UserFromName = "�ֻ�APP", Rank = 9 },
            //    new UserFromTable { UserFrom = "360", UserFromName = "360�ֻ�����", Rank = 10 },
            //    new UserFromTable { UserFrom = "bd", UserFromName = "�ٶ��ֻ�����", Rank = 11 },
            //    new UserFromTable { UserFrom = "lx", UserFromName = "�������̵�", Rank = 12 },
            //    new UserFromTable { UserFrom = "mi", UserFromName = "С��Ӧ���̵�", Rank = 13 },
            //    new UserFromTable { UserFrom = "tx", UserFromName = "��ѶӦ�ñ�", Rank = 14 },
            //    new UserFromTable { UserFrom = "wdj", UserFromName = "�㶹��", Rank = 15 },
            //    new UserFromTable { UserFrom = "yyh", UserFromName = "Ӧ�û�", Rank = 16 },
            //    new UserFromTable { UserFrom = "qt", UserFromName = "����", Rank = 17 },
            //    new UserFromTable { UserFrom = "mz", UserFromName = "����Ӧ���̵�", Rank = 18 },
            //    new UserFromTable { UserFrom = "az", UserFromName = "��׿�г�", Rank = 19 },
            //    new UserFromTable { UserFrom = "upd", UserFromName = "APP����", Rank = 20 }
            //);
        }
    }
}
