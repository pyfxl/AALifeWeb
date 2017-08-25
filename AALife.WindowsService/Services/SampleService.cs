using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.WindowsService
{
    public class SampleService : IJob
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            log.Info("开始测试自动服务。");
        }
    }
}
