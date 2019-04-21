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
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    /// <summary>
    /// 职位管理接口
    /// </summary>
    public class TitlesApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserTitleService _userTitleService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public TitlesApiController(IUserService userService,
            IUserTitleService userTitleService, 
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userTitleService = userTitleService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/<Controller>/5
        public IHttpActionResult Get()
        {
            var titles = _userTitleService.Get();

            return Json(titles);
        }

    }
}
