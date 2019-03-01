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
                new Parameter{ Id = 1, Name = "����", SystemName = "Theme", Value = "Theme", Rank = 1, OrderNo = "1"  },
                new Parameter{ Id = 2, Name = "�͵���", SystemName = "main".ToUpper(), Value = "main", Rank = 1, OrderNo = "1.1", ParentId = 1, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 3, Name = "������", SystemName = "gold".ToUpper(), Value = "gold", Rank = 2, OrderNo = "1.2", ParentId = 1, IsLeaf = true },
                new Parameter{ Id = 4, Name = "��˿��", SystemName = "blue".ToUpper(), Value = "blue", Rank = 3, OrderNo = "1.3", ParentId = 1, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, theme.ToArray());
            context.SaveChanges();

            var itemType = new List<Parameter>
            {
                new Parameter{ Id = 5, Name = "���ѷ���", SystemName = "ItemType", Value = "ItemType", Rank = 2, OrderNo = "2"  },
                new Parameter{ Id = 6, Name = "����", SystemName = "sr".ToUpper(), Value = "sr", Rank = 1, OrderNo = "2.1", ParentId = 5, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 7, Name = "֧��", SystemName = "zc".ToUpper(), Value = "zc", Rank = 2, OrderNo = "2.2", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 8, Name = "����", SystemName = "jr".ToUpper(), Value = "jr", Rank = 3, OrderNo = "2.3", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 9, Name = "���", SystemName = "jc".ToUpper(), Value = "jc", Rank = 4, OrderNo = "2.4", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 10, Name = "����", SystemName = "hr".ToUpper(), Value = "hr", Rank = 5, OrderNo = "2.5", ParentId = 5, IsLeaf = true  },
                new Parameter{ Id = 11, Name = "����", SystemName = "hc".ToUpper(), Value = "hc", Rank = 6, OrderNo = "2.6", ParentId = 5, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, itemType.ToArray());
            context.SaveChanges();

            var regionType = new List<Parameter>
            {
                new Parameter{ Id = 12, Name = "�������", SystemName = "RegionType", Value = "RegionType", Rank = 3, OrderNo = "3"  },
                new Parameter{ Id = 13, Name = "ÿ��", SystemName = "d".ToUpper(), Value = "d", Rank = 1, OrderNo = "3.1", ParentId = 12, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 14, Name = "ÿ��", SystemName = "w".ToUpper(), Value = "w", Rank = 2, OrderNo = "3.2", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 15, Name = "ÿ��", SystemName = "m".ToUpper(), Value = "m", Rank = 3, OrderNo = "3.3", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 16, Name = "ÿ��", SystemName = "j".ToUpper(), Value = "j", Rank = 4, OrderNo = "3.4", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 17, Name = "ÿ��", SystemName = "y".ToUpper(), Value = "y", Rank = 5, OrderNo = "3.5", ParentId = 12, IsLeaf = true  },
                new Parameter{ Id = 18, Name = "������", SystemName = "b".ToUpper(), Value = "b", Rank = 6, OrderNo = "3.6", ParentId = 12, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, regionType.ToArray());
            context.SaveChanges();

            var workDay = new List<Parameter>
            {
                new Parameter{ Id = 19, Name = "������", SystemName = "WorkDay", Value = "WorkDay", Rank = 4, OrderNo = "4"  },
                new Parameter{ Id = 20, Name = "ֻ��һ�ϰ�", SystemName = "1", Value = "1", Rank = 1, OrderNo = "4.1", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 21, Name = "һ�����ϰ�", SystemName = "2", Value = "2", Rank = 2, OrderNo = "4.2", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 22, Name = "һ�����ϰ�", SystemName = "3", Value = "3", Rank = 3, OrderNo = "4.3", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 23, Name = "һ�����ϰ�", SystemName = "4", Value = "4", Rank = 4, OrderNo = "4.4", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 24, Name = "��ĩ˫��", SystemName = "5", Value = "5", Rank = 5, OrderNo = "4.5", ParentId = 19, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 25, Name = "��ĩ����", SystemName = "6", Value = "6", Rank = 6, OrderNo = "4.6", ParentId = 19, IsLeaf = true  },
                new Parameter{ Id = 26, Name = "ȫ������", SystemName = "7", Value = "7", Rank = 7, OrderNo = "4.7", ParentId = 19, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, workDay.ToArray());
            context.SaveChanges();

            var userFrom = new List<Parameter>
            {
                new Parameter{ Id = 27, Name = "�û�����", SystemName = "UserFrom", Value = "UserFrom", Rank = 5, OrderNo = "5"  },
                new Parameter{ Id = 28, Name = "QQ�ռ�", SystemName = "qz".ToUpper(), Value = "qz", Rank = 1, OrderNo = "5.1", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 29, Name = "QQ��¼", SystemName = "qq".ToUpper(), Value = "qq", Rank = 2, OrderNo = "5.2", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 30, Name = "������", SystemName = "py".ToUpper(), Value = "py", Rank = 3, OrderNo = "5.3", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 31, Name = "�ֻ�QQ", SystemName = "sjqq".ToUpper(), Value = "sjqq", Rank = 4, OrderNo = "5.4", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 32, Name = "Web QQ", SystemName = "qw".ToUpper(), Value = "qw", Rank = 5, OrderNo = "5.5", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 33, Name = "��վ", SystemName = "web".ToUpper(), Value = "web", Rank = 6, OrderNo = "5.6", ParentId = 27, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 34, Name = "�ֻ�", SystemName = "sj".ToUpper(), Value = "sj", Rank = 7, OrderNo = "5.7", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 35, Name = "�ֻ�Web", SystemName = "sjweb".ToUpper(), Value = "sjweb", Rank = 8, OrderNo = "5.8", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 36, Name = "�ֻ�APP", SystemName = "sjapp".ToUpper(), Value = "sjapp", Rank = 9, OrderNo = "5.9", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 37, Name = "360�ֻ�����", SystemName = "360".ToUpper(), Value = "360", Rank = 10, OrderNo = "5.10", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 38, Name = "�ٶ��ֻ�����", SystemName = "bd".ToUpper(), Value = "bd", Rank = 11, OrderNo = "5.11", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 39, Name = "�������̵�", SystemName = "lx".ToUpper(), Value = "lx", Rank = 12, OrderNo = "5.12", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 40, Name = "С��Ӧ���̵�", SystemName = "mi".ToUpper(), Value = "mi", Rank = 13, OrderNo = "5.13", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 41, Name = "��ѶӦ�ñ�", SystemName = "tx".ToUpper(), Value = "tx", Rank = 14, OrderNo = "5.14", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 42, Name = "�㶹��", SystemName = "wdj".ToUpper(), Value = "wdj", Rank = 15, OrderNo = "5.15", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 43, Name = "Ӧ�û�", SystemName = "yyh".ToUpper(), Value = "yyh", Rank = 16, OrderNo = "5.16", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 44, Name = "����", SystemName = "qt".ToUpper(), Value = "qt", Rank = 17, OrderNo = "5.17", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 45, Name = "����Ӧ���̵�", SystemName = "mz".ToUpper(), Value = "mz", Rank = 18, OrderNo = "5.18", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 46, Name = "��׿�г�", SystemName = "az".ToUpper(), Value = "az", Rank = 19, OrderNo = "5.19", ParentId = 27, IsLeaf = true },
                new Parameter{ Id = 47, Name = "APP����", SystemName = "upd".ToUpper(), Value = "upd", Rank = 20, OrderNo = "5.20", ParentId = 27, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userFrom.ToArray());
            context.SaveChanges();

            var userLevel = new List<Parameter>
            {
                new Parameter{ Id = 48, Name = "�û��ȼ�", SystemName = "UserLevel", Value = "UserLevel", Rank = 6, OrderNo = "6"  },
                new Parameter{ Id = 49, Name = "�û�", SystemName = "1", Value = "1", Rank = 1, OrderNo = "6.1", ParentId = 48, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 50, Name = "����Ա", SystemName = "9", Value = "9", Rank = 2, OrderNo = "6.2", ParentId = 48, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userLevel.ToArray());
            context.SaveChanges();
        }
    }
}
