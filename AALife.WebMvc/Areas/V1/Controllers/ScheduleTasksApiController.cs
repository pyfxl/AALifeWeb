using AALife.Core.Domain.Logging;
using AALife.Core.Domain.Tasks;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ScheduleTasksApiController : BaseApiController
    {
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly ICustomerActivityService _customerActivityService;

        public ScheduleTasksApiController(IScheduleTaskService scheduleTaskService,
            ICustomerActivityService customerActivityService)
        {
            this._scheduleTaskService = scheduleTaskService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var result = _scheduleTaskService.GetAllTasks();

            var grid = new DataSourceResult
            {
                Data = result,
                Total = result.Count()
            };

            return Json(grid);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]ScheduleTask model)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            //插入
            _scheduleTaskService.InsertTask(model);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入定时任务记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]ScheduleTask model)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            var task = _scheduleTaskService.GetTaskById(model.Id);

            task.Name = model.Name;
            task.Type = model.Type;
            task.Enabled = model.Enabled;
            task.StopOnError = model.StopOnError;

            //更新
            _scheduleTaskService.UpdateTask(model);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "更新定时任务记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}