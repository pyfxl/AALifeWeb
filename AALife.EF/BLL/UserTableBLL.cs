using AALife.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.EF.BLL
{
    public class UserTableBLL
    {
        public IEnumerable<UserTableView> GetUserTable()
        {
            using(var db = new AALifeDbContext())
            {
                return db.UserTableView.OrderBy(a => a.UserID).ToList();
            }
        }
    }
}
