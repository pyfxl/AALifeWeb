using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class TreeViewModel
    {
        public TreeViewModel()
        {
            items = new List<TreeViewModel>();
        }

        public int id { get; set; }

        public string text { get; set; }

        public List<TreeViewModel> items { get; set; }

        public bool hasChildren { get; set; }

        public string spriteCssClass { get; set; }

        public int? parentId { get; set; }

        public string value { get; set; }

        public string name { get; set; }

        public string systemName { get; set; }

        public byte? rank { get; set; }

        [JsonProperty(PropertyName = "checked")]
        public bool isChecked { get; set; }

    }
}