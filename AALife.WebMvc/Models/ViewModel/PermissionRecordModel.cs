﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class PermissionRecordModel : BaseViewModel<Guid>
    {
        public string Name { get; set; }
    }
}