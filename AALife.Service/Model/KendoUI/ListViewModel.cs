using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.Service.Model.KendoUI
{
    public class ListViewModel<T>
    {
        public List<T> rows { get; set; }

        public int total { get; set; }

        public string error { get; set; }
    }
}
