using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    /// <summary>
    /// 组织树接口
    /// </summary>
    public class DeptmentsTreeApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public DeptmentsTreeApiController(IUserService userService, 
            IUserDeptmentService userDeptmentService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/Deptments/5
        public IHttpActionResult Get(Guid? id = null)
        {
            var result = _userDeptmentService.FindAll(a => a.ParentId == id);

            //下级组织
            var grid = result.AsEnumerable().Select(x =>
            {
                var hasChildren = _userDeptmentService.IsExists(a => a.ParentId == x.Id);
                var pm = new DeptmentPositionTreeModel
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Name = x.Name,
                    Code = x.Code
                };
                pm.hasChildren = hasChildren;
                pm.IsDeptment = true;
                return pm;
            });

            return Json(grid);
        }

    }
}
