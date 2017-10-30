using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.Service.Model.Common
{
    public class ListModel<T> : ResultModel
    {
        public List<T> rows { get; set; }

        public int total { get; set; }
        
    }
}
