using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{

    public partial class BaseTreeModel<TPrimaryKey>
    {
        public BaseTreeModel()
        {
            items = new List<BaseTreeModel<TPrimaryKey>>();
        }

        public virtual TPrimaryKey Id { get; set; }

        public string text { get; set; }

        public string value { get; set; }

        public virtual List<BaseTreeModel<TPrimaryKey>> items { get; set; }

        public bool hasChildren { get; set; }

        public bool expanded { get; set; }

        public string spriteCssClass { get; set; }

        [JsonProperty(PropertyName = "checked")]
        public bool isChecked { get; set; }

    }

    public partial class TreeViewModel : BaseTreeModel<Guid>
    {
        public Guid? ParentId { get; set; }
    }

    public partial class ParameterTreeModel : BaseTreeModel<int>
    {
        public int? ParentId { get; set; }
    }

    public partial class DeptmentPositionTreeModel : BaseTreeModel<Guid>
    {
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public Guid? DeptmentId { get; set; }

        public bool IsDeptment { get; set; }

        public bool IsPosition { get; set; }
    }

    public partial class PermissionTreeModel : BaseTreeModel<Guid>
    {
        public Guid? ParentId { get; set; }
    }
}