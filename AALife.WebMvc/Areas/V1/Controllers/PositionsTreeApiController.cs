using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class PositionsTreeApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public PositionsTreeApiController(IUserService userService, 
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/Positions
        public IHttpActionResult Get()
        {
            var tree = SortForTree();

            return Json(tree);
        }

        // GET: api/Positions/5
        public IHttpActionResult Get(int id)
        {
            var tree = SortForTree(id);

            return Json(tree);
        }

        #region 其它方法 

        private List<TreeViewModel> SortForTree(int? id = null, int? parentId = null)
        {
            var userPosition = new List<UserPosition>();
            if(id != null)
                userPosition = _userService.Get(id.GetValueOrDefault()).UserPositions.ToList();

            var model = new List<TreeViewModel>();
            foreach (var p in _userPositionService.FindAll(a => a.Id > 0))
            {
                var pm = new TreeViewModel
                {
                    id = p.Id,
                    text = p.Name,
                    value = p.Name,
                    name = p.Name
                };
                //pm.items.AddRange(SortForTree(id, p.Id));
                pm.hasChildren = pm.items.Count > 0;
                model.Add(pm);
            }
            return model;
        }

        #endregion
    }
}
