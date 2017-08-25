using NLog;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AALife.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private ISchedulerFactory schedulerFactory;
        private IScheduler scheduler;

        public Service1()
        {
            InitializeComponent();

            schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                scheduler.Start();
            }
            catch (Exception ex)
            {
                log.Fatal(string.Format("Scheduler start failed: {0}", ex.Message), ex);
                throw;
            }

            log.Info("Scheduler started successfully");
        }

        protected override void OnStop()
        {
            try
            {
                scheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Scheduler stop failed: {0}", ex.Message), ex);
                throw;
            }

            log.Info("Scheduler shutdown complete");
        }
    }
}
