﻿using AALife.BLL;
using AALife.Service.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AALife.WebMvc.Web2018.Manage2019
{
    public partial class Manage_ItemPivotGrid : AdminPage
    {
        public int UserID
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["userId"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}