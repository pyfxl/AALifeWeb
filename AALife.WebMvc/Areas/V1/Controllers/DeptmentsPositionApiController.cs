using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    /// <summary>
    /// 操作指定部门下的岗位
    /// </summary>
    public class DeptmentsPositionApiController : BaseApiController
    {
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;

        public DeptmentsPositionApiController(IUserDeptmentService userDeptmentService,
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService)
        {
            this._userDeptmentService = userDeptmentService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var users = _userPositionService.FindAll(x => x.Deptments.Any(a => a.Id == id));

            var grid = new DataSourceResult
            {
                Data = users,
                Total = users.Count()
            };

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Query, "浏览部门岗位记录。{0}", id);

            return Json(grid);
        }

        // POST api/<controller>/5
        public void Post(int id, [FromBody]IEnumerable<UserPosition> models)
        {
            var deptment = _userDeptmentService.Get(id);
            models.ToList().ForEach(a =>
            {
                var position = _userPositionService.Get(a.Id);
                if (position != null)
                    deptment.Positions.Add(position);
            });

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入部门岗位记录。{0}", models.ToJson());
        }

        // DELETE api/<controller>/5
        public void Delete(int id, [FromBody]IEnumerable<UserPosition> models)
        {
            var deptment = _userDeptmentService.Get(id);
            models.ToList().ForEach(a =>
            {
                var position = _userPositionService.Get(a.Id);
                if (deptment.Positions.Contains(position))
                    deptment.Positions.Remove(position);
            });

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Delete, "删除部门岗位记录。{0}", models.ToJson());
        }

    }
}