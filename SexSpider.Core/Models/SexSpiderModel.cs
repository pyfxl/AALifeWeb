using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SexSpider.Core.Models
{
    public class SexSpiderModel
    {

        public List<SexSpiders> site_list { get; set; }

        public List<string> ext_dic { get; set; }

        public List<string> stop_dic { get; set; }

        public List<string> del_dic { get; set; }

    }
}
