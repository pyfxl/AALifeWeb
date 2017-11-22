using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class ViewTableBLL
    {
        private static readonly ViewTableDAL dal = new ViewTableDAL();
        
        /// <summary>
        /// 插入钱包
        /// </summary>
        public bool InsertView(ViewInfo view)
        {
            return dal.InsertView(view);
        }
        
    }
}
