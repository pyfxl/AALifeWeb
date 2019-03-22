using AALife.Core.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class ScheduleTasksController : BaseAdminController
    {
        private readonly IScheduleTaskService _scheduleTaskService;

        public ScheduleTasksController(IScheduleTaskService scheduleTaskService)
        {
            this._scheduleTaskService = scheduleTaskService;
        }

        // GET: Manage/ScheduleTasks
        public ActionResult Index()
        {
            return View();
        }
        
        public virtual ActionResult RunNow(int id)
        {
            try
            {
                var scheduleTask = _scheduleTaskService.GetTaskById(id);
                if (scheduleTask == null)
                    throw new Exception("Schedule task cannot be loaded");

                var task = new Task(scheduleTask);
                //ensure that the task is enabled
                task.Enabled = true;
                //do not dispose. otherwise, we can get exception that DbContext is disposed
                task.Execute(true, false, false);
                SuccessNotification("执行成功。");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
            }

            return RedirectToAction("Index");
        }
    }
}