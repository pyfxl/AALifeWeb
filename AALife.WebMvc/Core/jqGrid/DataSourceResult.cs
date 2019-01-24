using System.Collections;

namespace AALife.WebMvc.jqGrid
{
    public class DataSourceResult
    {
        public IEnumerable rows { get; set; }

        public int page { get; set; }

        public int total { get; set; }

        public int records { get; set; }

        public object userdata { get; set; }
    }
}
