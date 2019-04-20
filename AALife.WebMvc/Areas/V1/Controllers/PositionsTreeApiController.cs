using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class PositionsTreeApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public PositionsTreeApiController(IUserService userService,
            IUserDeptmentService userDeptmentService,
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/Positions/5
        public IHttpActionResult Get(Guid? id)
        {
            //var tree = SortForTree(id);

            return Json("");
        }

        #region 其它方法 

        private List<TreeViewModel> SortForTree(Guid? id = null, Guid? parentId = null)
        {
            var model = new List<TreeViewModel>();
            foreach (var p in _userPositionService.FindAll(a => a.Id != default(Guid)))
            {
                var pm = new TreeViewModel
                {
                    Id = p.Id,
                    ParentId = p.ParentId.GetValueOrDefault(),
                    text = p.Name,
                    value = p.Name
                };
                pm.items.AddRange(SortForTree(id, p.Id));
                pm.hasChildren = pm.items.Count > 0;
                model.Add(pm);
            }
            return model;
        }

        #endregion
    }
}
