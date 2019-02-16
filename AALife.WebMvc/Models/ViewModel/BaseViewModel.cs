using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public abstract partial class BaseViewModel
    {
        public virtual int Id { get; set; }
    }
}