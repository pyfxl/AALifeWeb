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

            #region ϵͳ����

            var root = new List<Parameter>
            {
                new Parameter{ Id = 1, Name = "��Ŀ¼", Value = "Root", Rank = 1, OrderNo = "01"  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Name }, root.ToArray());
            context.SaveChanges();

            var theme = new List<Parameter>
            {
                new Parameter{ Id = 2, Name = "����", Value = "Theme", Rank = 1, OrderNo = "01.01", ParentId = 1  },
                new Parameter{ Id = 3, Name = "�͵���", Value = "main", Rank = 1, OrderNo = "01.01.01", ParentId = 2, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 4, Name = "������", Value = "gold", Rank = 2, OrderNo = "01.01.02", ParentId = 2, IsLeaf = true },
                new Parameter{ Id = 5, Name = "��˿��", Value = "blue", Rank = 3, OrderNo = "01.01.03", ParentId = 2, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Name }, theme.ToArray());
            context.SaveChanges();

            var itemType = new List<Parameter>
            {
                new Parameter{ Id = 6, Name = "���ѷ���", Value = "ItemType", Rank = 2, OrderNo = "01.02", ParentId = 1  },
                new Parameter{ Id = 7, Name = "����", Value = "sr", Rank = 1, OrderNo = "01.02.01", ParentId = 6, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 8, Name = "֧��", Value = "zc", Rank = 2, OrderNo = "01.02.02", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 9, Name = "����", Value = "jr", Rank = 3, OrderNo = "01.02.03", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 10, Name = "���", Value = "jc", Rank = 4, OrderNo = "01.02.04", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 11, Name = "����", Value = "hr", Rank = 5, OrderNo = "01.02.05", ParentId = 6, IsLeaf = true  },
                new Parameter{ Id = 12, Name = "����", Value = "hc", Rank = 6, OrderNo = "01.02.06", ParentId = 6, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Name }, itemType.ToArray());
            context.SaveChanges();

            var regionType = new List<Parameter>
            {
                new Parameter{ Id = 13, Name = "�������", Value = "RegionType", Rank = 3, OrderNo = "01.03", ParentId = 1  },
                new Parameter{ Id = 14, Name = "ÿ��", Value = "d", Rank = 1, OrderNo = "01.03.01", ParentId = 13, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 15, Name = "ÿ��", Value = "w", Rank = 2, OrderNo = "01.03.02", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 16, Name = "ÿ��", Value = "m", Rank = 3, OrderNo = "01.03.03", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 17, Name = "ÿ��", Value = "j", Rank = 4, OrderNo = "01.03.04", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 18, Name = "ÿ��", Value = "y", Rank = 5, OrderNo = "01.03.05", ParentId = 13, IsLeaf = true  },
                new Parameter{ Id = 19, Name = "������", Value = "b", Rank = 6, OrderNo = "01.03.06", ParentId = 13, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, regionType.ToArray());
            context.SaveChanges();

            var workDay = new List<Parameter>
            {
                new Parameter{ Id = 20, Name = "������", Value = "WorkDay", Rank = 4, OrderNo = "01.04", ParentId = 1  },
                new Parameter{ Id = 21, Name = "ֻ��һ�ϰ�", Value = "1", Rank = 1, OrderNo = "01.04.01", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 22, Name = "һ�����ϰ�", Value = "2", Rank = 2, OrderNo = "01.04.02", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 23, Name = "һ�����ϰ�", Value = "3", Rank = 3, OrderNo = "01.04.03", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 24, Name = "һ�����ϰ�", Value = "4", Rank = 4, OrderNo = "01.04.04", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 25, Name = "��ĩ˫��", Value = "5", Rank = 5, OrderNo = "01.04.05", ParentId = 20, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 26, Name = "��ĩ����", Value = "6", Rank = 6, OrderNo = "01.04.06", ParentId = 20, IsLeaf = true  },
                new Parameter{ Id = 27, Name = "ȫ������", Value = "7", Rank = 7, OrderNo = "01.04.07", ParentId = 20, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, workDay.ToArray());
            context.SaveChanges();

            var userFrom = new List<Parameter>
            {
                new Parameter{ Id = 28, Name = "�û�����", Value = "UserFrom", Rank = 5, OrderNo = "01.05", ParentId = 1  },
                new Parameter{ Id = 29, Name = "QQ�ռ�", Value = "qz", Rank = 1, OrderNo = "01.05.01", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 30, Name = "QQ��¼", Value = "qq", Rank = 2, OrderNo = "01.05.02", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 31, Name = "������", Value = "py", Rank = 3, OrderNo = "01.05.03", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 32, Name = "�ֻ�QQ", Value = "sjqq", Rank = 4, OrderNo = "01.05.04", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 33, Name = "Web QQ", Value = "qw", Rank = 5, OrderNo = "01.05.05", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 34, Name = "��վ", Value = "web", Rank = 6, OrderNo = "01.05.06", ParentId = 28, IsLeaf = true, IsDefault = true },
                new Parameter{ Id = 35, Name = "�ֻ�", Value = "sj", Rank = 7, OrderNo = "01.05.07", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 36, Name = "�ֻ�Web", Value = "sjweb", Rank = 8, OrderNo = "01.05.08", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 37, Name = "�ֻ�APP", Value = "sjapp", Rank = 9, OrderNo = "01.05.09", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 38, Name = "360�ֻ�����", Value = "360", Rank = 10, OrderNo = "01.05.10", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 39, Name = "�ٶ��ֻ�����", Value = "bd", Rank = 11, OrderNo = "01.05.11", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 40, Name = "�������̵�", Value = "lx", Rank = 12, OrderNo = "01.05.12", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 41, Name = "С��Ӧ���̵�", Value = "mi", Rank = 13, OrderNo = "01.05.13", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 42, Name = "��ѶӦ�ñ�", Value = "tx", Rank = 14, OrderNo = "01.05.14", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 43, Name = "�㶹��", Value = "wdj", Rank = 15, OrderNo = "01.05.15", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 44, Name = "Ӧ�û�", Value = "yyh", Rank = 16, OrderNo = "01.05.16", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 45, Name = "����", Value = "qt", Rank = 17, OrderNo = "01.05.17", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 46, Name = "����Ӧ���̵�", Value = "mz", Rank = 18, OrderNo = "01.05.18", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 47, Name = "��׿�г�", Value = "az", Rank = 19, OrderNo = "01.05.19", ParentId = 28, IsLeaf = true },
                new Parameter{ Id = 48, Name = "APP����", Value = "upd", Rank = 20, OrderNo = "01.05.20", ParentId = 28, IsLeaf = true }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userFrom.ToArray());
            context.SaveChanges();

            var userLevel = new List<Parameter>
            {
                new Parameter{ Id = 49, Name = "�û��ȼ�", Value = "UserLevel", Rank = 6, OrderNo = "01.06", ParentId = 1  },
                new Parameter{ Id = 50, Name = "�û�", Value = "1", Rank = 1, OrderNo = "01.06.01", ParentId = 49, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 51, Name = "����Ա", Value = "9", Rank = 2, OrderNo = "01.06.02", ParentId = 49, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, userLevel.ToArray());
            context.SaveChanges();

            var deptmentCategory = new List<Parameter>
            {
                new Parameter{ Id = 52, Name = "��֯����", Value = "OrgType", Rank = 7, OrderNo = "01.07", ParentId = 1  },
                new Parameter{ Id = 53, Name = "����", Value = "Group", Rank = 1, OrderNo = "01.07.01", ParentId = 52, IsLeaf = true, IsDefault = true  },
                new Parameter{ Id = 54, Name = "���", Value = "Area", Rank = 2, OrderNo = "01.07.02", ParentId = 52, IsLeaf = true  },
                new Parameter{ Id = 55, Name = "��˾", Value = "Company", Rank = 3, OrderNo = "01.07.03", ParentId = 52, IsLeaf = true  },
                new Parameter{ Id = 56, Name = "����", Value = "Deptment", Rank = 4, OrderNo = "01.07.04", ParentId = 52, IsLeaf = true  },
                new Parameter{ Id = 57, Name = "С��", Value = "Class", Rank = 5, OrderNo = "01.07.05", ParentId = 52, IsLeaf = true  }
            };
            context.Set<Parameter>().AddOrUpdate(m => new { m.Id }, deptmentCategory.ToArray());
            context.SaveChanges();

            #endregion

            #region �ʼ�ģ��

            var template = new MessageTemplate
            {
                Id = 1,
                Name = "�����ʼ�",
                SystemName = "TestEmail",
                Subject = "����һ������ʼ�",
                Body = "���ã�����һ������ʼ����յ����ʼ���˵���ʼ����������������ġ�",
                IsActive = true
            };
            context.Set<MessageTemplate>().AddOrUpdate(m => new { m.SystemName }, template);
            context.SaveChanges();

            #endregion

            #region ��ʱ����

            var taskMessages = new ScheduleTask
            {
                Id = 1,
                Name = "��Ϣ�ʼ�",
                SystemName = "QueuedMessages",
                Type = "AALife.Core.Services.Messages.QueuedMessagesSendTask, AALife.Core",
                Seconds = 60,
                Enabled = true,
                StopOnError = false
            };
            var taskKeepAlive = new ScheduleTask
            {
                Id = 2,
                Name = "��������",
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
