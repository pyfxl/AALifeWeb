using AALife.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.EF.BLL
{
    /// <summary>
    /// 用户来自业务逻辑
    /// </summary>
    public class UserFromBLL
    {
        public IEnumerable<UserFromTable> GetUserFrom()
        {
            using (var db = new AALifeDbContext())
            {
                return db.Set<UserFromTable>().OrderBy(a => a.Rank).ToList();
            }
        }
    }
}
