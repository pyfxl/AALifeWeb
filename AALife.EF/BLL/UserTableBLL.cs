using AALife.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.EF.BLL
{
    /// <summary>
    /// 用户表业务逻辑
    /// </summary>
    public class UserTableBLL
    {
        public IEnumerable<UserTable> GetUserTable()
        {
            using(var db = new AALifeDbContext())
            {
                return db.UserTable.OrderBy(a => a.UserID).ToList();
            }
        }

        public IEnumerable<UserTableView> GetUserTableView()
        {
            using (var db = new AALifeDbContext())
            {
                return db.UserTableView.OrderBy(a => a.UserID).ToList();
            }
        }
    }
}
