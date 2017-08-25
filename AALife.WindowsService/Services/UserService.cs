using AALife.BLL;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.WindowsService
{
    public class UserService : IJob
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            log.Info("开始用户自动服务。");

            try
            {
                UserTableBLL bll = new UserTableBLL();
                DataTable dt = bll.GetUserList();

                log.Info("用户数：" + dt.Rows.Count);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                JobExecutionException jobException = new JobExecutionException(ex);
                throw jobException;
            }
        }
    }
}
