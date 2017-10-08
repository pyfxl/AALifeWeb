using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.EF.ViewModel
{
    public class QueryPageModel : QueryModel
    {
        public int page { get; set; }

        public int pageSize { get; set; }

        public int skip { get; set; }

        public int take { get; set; }
    }
}
