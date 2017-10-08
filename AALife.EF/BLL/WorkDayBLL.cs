using AALife.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.EF.BLL
{
    /// <summary>
    /// 工作日业务逻辑
    /// </summary>
    public class WorkDayBLL
    {
        public IEnumerable<WorkDayTable> GetWorkDay()
        {
            using (var db = new AALifeDbContext())
            {
                return db.Set<WorkDayTable>().OrderBy(a => a.Rank).ToList();
            }
        }

    }
}
