using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    [Authorize]
    public abstract partial class BaseMvcController : BaseController
    {
    }
}