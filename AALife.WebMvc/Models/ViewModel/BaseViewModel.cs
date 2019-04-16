using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public abstract partial class BaseViewModel<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }

    public abstract partial class BaseViewModel : BaseViewModel<int>
    {
        public new int Id { get; set; }
    }
}