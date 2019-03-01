using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Areas.Manage.Models
{
    public class MenuViewModel : BaseModel
    {
        public MenuViewModel()
        {
            this.ChildMenus = new List<MenuViewModel>();
        }

        public string Name { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public bool Selected { get; set; }

        public List<MenuViewModel> ChildMenus { get; set; }
    }
}