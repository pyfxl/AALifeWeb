using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
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
    /// 根据用户选组织接口
    /// </summary>
    public class UsersDeptmentApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public UsersDeptmentApiController(IUserService userService,
            IUserDeptmentService userDeptmentService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(Guid id)
        {
            var user = _userService.Get(id);

            var grid = new DataSourceResult
            {
                Data = user.UserDeptments.Select(x =>
                {
                    var m = x.MapTo<UserDeptment, UserDeptmentModel>();
                    m.Parent = x.Parent;
                    return m;
                }),
                Total = user.UserDeptments.Count()
            };

            return Json(grid);
        }

    }
}