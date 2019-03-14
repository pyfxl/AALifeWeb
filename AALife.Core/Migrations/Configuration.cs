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

            var root = new List<Parameter>
            {
                new Parameter{ Id = 1, Name = "��Ŀ¼", SystemName = "Root", Value = "Root", Rank = 1, OrderNo = "1"  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, root.ToArray());
            context.SaveChanges();

            var theme = new List<Parameter>
            {
                new Parameter{ Id = 2, Name = "����", SystemName = "Theme", Value = "Theme", Rank = 1, OrderNo = "1.1", ParentId = 1  },
                new Parameter{ Id = 3, Name = "�͵���", SystemName = "main".ToUpper(), Value = "main", Rank = 1, OrderNo = "2.1", ParentId = 2, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 4, Name = "������", SystemName = "gold".ToUpper(), Value = "gold", Rank = 2, OrderNo = "2.2", ParentId = 2, IsLeaf = true },
                new Parameter{ Id = 5, Name = "��˿��", SystemName = "blue".ToUpper(), Value = "blue", Rank = 3, OrderNo = "2.3", ParentId = 2, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, theme.ToArray());
            context.SaveChanges();

            var itemType = new List<Parameter>
            {
                new Parameter{ Id = 6, Name = "���ѷ���", SystemName = "ItemType", Value = "ItemType", Rank = 2, OrderNo = "1.2", ParentId = 1  },
                new Parameter{ Id = 7, Name = "����", SystemName = "sr".ToUpper(), Value = "sr", Rank = 1, OrderNo = "6.1", ParentId = 6, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 8, Name = "֧��", SystemName = "zc".ToUpper(), Value = "zc", Rank = 2, OrderNo = "6.2", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 9, Name = "����", SystemName = "jr".ToUpper(), Value = "jr", Rank = 3, OrderNo = "6.3", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 10, Name = "���", SystemName = "jc".ToUpper(), Value = "jc", Rank = 4, OrderNo = "6.4", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 11, Name = "����", SystemName = "hr".ToUpper(), Value = "hr", Rank = 5, OrderNo = "6.5", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 12, Name = "����", SystemName = "hc".ToUpper(), Value = "hc", Rank = 6, OrderNo = "6.6", ParentId = 6, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, itemType.ToArray());
            context.SaveChanges();

            var regionType = new List<Parameter>
            {
                new Parameter{ Id = 13, Name = "�������", SystemName = "RegionType", Value = "RegionType", Rank = 3, OrderNo = "1.3", ParentId = 1  },
                new Parameter{ Id = 14, Name = "ÿ��", SystemName = "d".ToUpper(), Value = "d", Rank = 1, OrderNo = "13.1", ParentId = 13, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 15, Name = "ÿ��", SystemName = "w".ToUpper(), Value = "w", Rank = 2, OrderNo = "13.2", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 16, Name = "ÿ��", SystemName = "m".ToUpper(), Value = "m", Rank = 3, OrderNo = "13.3", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 17, Name = "ÿ��", SystemName = "j".ToUpper(), Value = "j", Rank = 4, OrderNo = "13.4", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 18, Name = "ÿ��", SystemName = "y".ToUpper(), Value = "y", Rank = 5, OrderNo = "13.5", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 19, Name = "������", SystemName = "b".ToUpper(), Value = "b", Rank = 6, OrderNo = "13.6", ParentId = 13, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, regionType.ToArray());
            context.SaveChanges();

            var workDay = new List<Parameter>
            {
                new Parameter{ Id = 20, Name = "������", SystemName = "WorkDay", Value = "WorkDay", Rank = 4, OrderNo = "1.4", ParentId = 1  },
                new Parameter{ Id = 21, Name = "ֻ��һ�ϰ�", SystemName = "1", Value = "1", Rank = 1, OrderNo = "20.1", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 22, Name = "һ�����ϰ�", SystemName = "2", Value = "2", Rank = 2, OrderNo = "20.2", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 23, Name = "һ�����ϰ�", SystemName = "3", Value = "3", Rank = 3, OrderNo = "20.3", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 24, Name = "һ�����ϰ�", SystemName = "4", Value = "4", Rank = 4, OrderNo = "20.4", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 25, Name = "��ĩ˫��", SystemName = "5", Value = "5", Rank = 5, OrderNo = "20.5", ParentId = 20, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 26, Name = "��ĩ����", SystemName = "6", Value = "6", Rank = 6, OrderNo = "20.6", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 27, Name = "ȫ������", SystemName = "7", Value = "7", Rank = 7, OrderNo = "20.7", ParentId = 20, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, workDay.ToArray());
            context.SaveChanges();

            var userFrom = new List<Parameter>
            {
                new Parameter{ Id = 28, Name = "�û�����", SystemName = "UserFrom", Value = "UserFrom", Rank = 5, OrderNo = "1.5", ParentId = 1  },
                new Parameter{ Id = 29, Name = "QQ�ռ�", SystemName = "qz".ToUpper(), Value = "qz", Rank = 1, OrderNo = "28.1", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 30, Name = "QQ��¼", SystemName = "qq".ToUpper(), Value = "qq", Rank = 2, OrderNo = "28.2", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 31, Name = "������", SystemName = "py".ToUpper(), Value = "py", Rank = 3, OrderNo = "28.3", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 32, Name = "�ֻ�QQ", SystemName = "sjqq".ToUpper(), Value = "sjqq", Rank = 4, OrderNo = "28.4", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 33, Name = "Web QQ", SystemName = "qw".ToUpper(), Value = "qw", Rank = 5, OrderNo = "28.5", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 34, Name = "��վ", SystemName = "web".ToUpper(), Value = "web", Rank = 6, OrderNo = "28.6", ParentId = 28, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 35, Name = "�ֻ�", SystemName = "sj".ToUpper(), Value = "sj", Rank = 7, OrderNo = "28.7", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 36, Name = "�ֻ�Web", SystemName = "sjweb".ToUpper(), Value = "sjweb", Rank = 8, OrderNo = "28.8", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 37, Name = "�ֻ�APP", SystemName = "sjapp".ToUpper(), Value = "sjapp", Rank = 9, OrderNo = "28.9", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 38, Name = "360�ֻ�����", SystemName = "360".ToUpper(), Value = "360", Rank = 10, OrderNo = "28.10", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 39, Name = "�ٶ��ֻ�����", SystemName = "bd".ToUpper(), Value = "bd", Rank = 11, OrderNo = "28.11", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 40, Name = "�������̵�", SystemName = "lx".ToUpper(), Value = "lx", Rank = 12, OrderNo = "28.12", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 41, Name = "С��Ӧ���̵�", SystemName = "mi".ToUpper(), Value = "mi", Rank = 13, OrderNo = "28.13", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 42, Name = "��ѶӦ�ñ�", SystemName = "tx".ToUpper(), Value = "tx", Rank = 14, OrderNo = "28.14", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 43, Name = "�㶹��", SystemName = "wdj".ToUpper(), Value = "wdj", Rank = 15, OrderNo = "28.15", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 44, Name = "Ӧ�û�", SystemName = "yyh".ToUpper(), Value = "yyh", Rank = 16, OrderNo = "28.16", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 45, Name = "����", SystemName = "qt".ToUpper(), Value = "qt", Rank = 17, OrderNo = "28.17", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 46, Name = "����Ӧ���̵�", SystemName = "mz".ToUpper(), Value = "mz", Rank = 18, OrderNo = "28.18", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 47, Name = "��׿�г�", SystemName = "az".ToUpper(), Value = "az", Rank = 19, OrderNo = "28.19", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 48, Name = "APP����", SystemName = "upd".ToUpper(), Value = "upd", Rank = 20, OrderNo = "28.20", ParentId = 28, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userFrom.ToArray());
            context.SaveChanges();

            var userLevel = new List<Parameter>
            {
                new Parameter{ Id = 49, Name = "�û��ȼ�", SystemName = "UserLevel", Value = "UserLevel", Rank = 6, OrderNo = "1.6", ParentId = 1  },
                new Parameter{ Id = 50, Name = "�û�", SystemName = "1", Value = "1", Rank = 1, OrderNo = "49.1", ParentId = 49, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 51, Name = "����Ա", SystemName = "9", Value = "9", Rank = 2, OrderNo = "49.2", ParentId = 49, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userLevel.ToArray());
            context.SaveChanges();
        }
    }
}
