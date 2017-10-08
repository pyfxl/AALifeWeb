using AALife.EF.Models;
using AALife.EF.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AALife.EF.BLL
{
    /// <summary>
    /// 消费表业务逻辑
    /// </summary>
    public class ItemTableBLL : BaseBLL
    {
        public IEnumerable<ItemTable> GetItemTable(DateTime startDate, DateTime endDate)
        {
            using (var db = new AALifeDbContext())
            {
                return db.Set<ItemTable>().Where(a => a.ItemBuyDate >= startDate && a.ItemBuyDate <= endDate).OrderByDescending(a => a.ItemID).ToList();
            }
        }

        public IEnumerable<ItemTable> GetItemTable(DateTime startDate, DateTime endDate, int userId)
        {
            using (var db = new AALifeDbContext())
            {
                return db.Set<ItemTable>().Where(a => a.UserID == userId && a.ItemBuyDate >= startDate && a.ItemBuyDate <= endDate).OrderByDescending(a => a.ItemID).ToList();
            }
        }

        public IEnumerable<ItemTable> GetItemTable(string key)
        {
            using (var db = new AALifeDbContext())
            {
                return db.Set<ItemTable>().Where(a => a.ItemName.Contains(key)).OrderByDescending(a => a.ItemID).ToList();
            }
        }

        public IEnumerable<ItemTable> GetItemTable(int userId)
        {
            using (var db = new AALifeDbContext())
            {
                return db.Set<ItemTable>().Where(a => a.UserID == userId).OrderByDescending(a => a.ItemID).ToList();
            }
        }

        public IEnumerable<ItemTable> GetItemTableDapper(DateTime startDate, DateTime endDate)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string sql = "select * from ItemTable where ItemBuyDate >= @start and ItemBuyDate <= @end";
                return conn.Query<ItemTable>(sql, new { start = startDate, end = endDate });
            }
        }

    }
}
