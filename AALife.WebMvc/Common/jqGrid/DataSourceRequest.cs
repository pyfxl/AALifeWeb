namespace AALife.WebMvc.jqGrid
{
    public class DataSourceRequest
    {
        public int page { get; set; }

        public int rows { get; set; }

        public string sidx { get; set; }

        public string sord { get; set; }

        public DataSourceRequest()
        {
            this.page = 1;
            this.rows = 50;
            this.sidx = "";
            this.sord = "";
        }
    }
}
