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

        public virtual Guid id { get; set; }

        public virtual Guid? parentId { get; set; }

        public string text { get; set; }

        public List<TreeViewModel> items { get; set; }

        public bool hasChildren { get; set; }

        public bool expanded { get; set; }

        public string spriteCssClass { get; set; }

        public string value { get; set; }

        public string name { get; set; }

        public string systemName { get; set; }

        public byte? rank { get; set; }

        [JsonProperty(PropertyName = "checked")]
        public bool isChecked { get; set; }

        public bool isDeptment { get; set; }

        public bool isPosition { get; set; }

        public string code { get; set; }

        public Guid deptmentId { get; set; }

    }

    public partial class ParameterTreeViewModel : TreeViewModel
    {
        public new int id { get; set; }

        public new int? parentId { get; set; }

    }

    public partial class PositionTreeViewModel : TreeViewModel
    {

    }
}