using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    /// <summary>
    /// 操作指定用户下的部门
    /// </summary>
    public class UserDeptmentsApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public UserDeptmentsApiController(IUserService userService,
            IUserDeptmentService userDeptmentService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // PUT api/<controller>/5
        public void Put(Guid id, [FromBody]IEnumerable<UserDeptment> models)
        {
            var user = _userService.Get(id);

            user.UserDeptments.Clear();

            models.ToList().ForEach(d =>
            {
                var dept = _userDeptmentService.Get(d.Id);
                user.UserDeptments.Add(dept);
            });

            //提交
            _userService.Update(user);
        }

    }
}