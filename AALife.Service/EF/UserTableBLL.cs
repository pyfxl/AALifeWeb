using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using AALife.Service.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Data.Entity.Core.Objects;

namespace AALife.Service.EF
{
    /// <summary>
    /// 用户表业务逻辑
    /// </summary>
    public class UserTableBLL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserTableBLL()
        {
            //TypeAdapterConfig<Models.UserTable, UserTableViewModel>.NewConfig()
            //    .Map(dest => dest.UserID, src => src.UserID)
            //    .Map(dest => dest.UserName, src => src.UserName)
            //    .Map(dest => dest.UserPassword, src => src.UserPassword)
            //    .Map(dest => dest.UserNickName, src => src.UserNickName)
            //    .Map(dest => dest.UserImage, src => src.UserImage)
            //    .Map(dest => dest.UserPhone, src => src.UserPhone)
            //    .Map(dest => dest.UserEmail, src => src.UserEmail)
            //    .Map(dest => dest.UserTheme, src => src.UserTheme)
            //    .Map(dest => dest.UserLevel, src => src.UserLevel)
            //    .Map(dest => dest.UserFrom, src => src.UserFrom)
            //    .Map(dest => dest.UserFromName, src => src.UserFromTable.UserFromName)
            //    .Map(dest => dest.ModifyDate, src => src.ModifyDate)
            //    .Map(dest => dest.CreateDate, src => src.CreateDate)
            //    .Map(dest => dest.UserCity, src => src.UserCity)
            //    .Map(dest => dest.UserMoney, src => src.UserMoney)
            //    .Map(dest => dest.UserWorkDay, src => src.UserWorkDay)
            //    .Map(dest => dest.UserWorkDayName, src => src.WorkDayTable.WorkDayName)
            //    .Map(dest => dest.UserFunction, src => src.UserFunction)
            //    .Map(dest => dest.CategoryRate, src => src.CategoryRate)
            //    .Map(dest => dest.Synchronize, src => src.Synchronize)
            //    .Map(dest => dest.MoneyStart, src => src.MoneyStart)
            //    .Map(dest => dest.IsUpdate, src => src.IsUpdate)
            //    .Map(dest => dest.ItemCount, src => src.ItemTable.Count())
            //    .Map(dest => dest.JoinDay, src => DbFunctions.DiffDays(src.CreateDate, DateTime.Now) + 1)
            //    .Compile();
        }

        /// <summary>
        /// 取用户根据日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<UserTableViewModel> GetUserTable(QueryPageModel query, out int count)
        {
            using (var db = new AALifeDbContext())
            {
                //默认
                var lists = db.Set<UserTable>()
                    .Where(a => a.CreateDate >= query.startDate && a.CreateDate <= query.endDate);
                
                //关键字
                if (query.keySearch != null && query.keySearch.Any())
                {
                    int userId = 0;
                    Int32.TryParse(query.keySearch, out userId);

                    lists = db.Set<UserTable>().Where(a => a.UserID == userId || a.UserName.Contains(query.keySearch) || a.UserPassword.Contains(query.keySearch) || a.UserNickName.Contains(query.keySearch) || a.UserEmail.Contains(query.keySearch));
                }

                //结果
                var result = lists
                    .Include(a => a.UserFromTable)
                    .Include(a => a.WorkDayTable)
                    .ProjectToType<UserTableViewModel>();
                
                //where查询
                if (query.filter != null)
                {
                    result = result.Where(query.filterString);
                }

                //排序
                if (query.sort != null && query.sort.Count > 0)
                {
                    result = result.OrderBy(query.sortString);
                }
                else
                {
                    result = result.OrderByDescending(a => a.UserID);
                }

                //总数
                count = result.Count();
                
                //返回
                return result.Skip(query.skip).Take(query.take).ToList();
            }
        }

        /// <summary>
        /// 取用户根据关键字
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public IEnumerable<UserTableViewModel> GetUserTable(string keySearch)
        {
            using (var db = new AALifeDbContext())
            {
                int userId = 0;
                Int32.TryParse(keySearch, out userId);

                var lists = db.Set<UserTable>().Where(a => a.UserID == userId || a.UserName.Contains(keySearch) || a.UserPassword.Contains(keySearch) || a.UserNickName.Contains(keySearch) || a.UserEmail.Contains(keySearch)).OrderByDescending(a => a.UserID);

                var result = lists
                    .Include(a => a.UserFromTable)
                    .Include(a => a.WorkDayTable)
                    .ProjectToType<UserTableViewModel>();

                return result.ToList();
            }
        }

        /// <summary>
        /// 取1个用户根据id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserTableViewModel GetUser(int userId)
        {
            using (var db = new AALifeDbContext())
            {
                var item = db.Set<UserTable>().Where(a => a.UserID == userId);

                var result = item
                    .Include(a => a.UserFromTable)
                    .Include(a => a.WorkDayTable)
                    .ProjectToType<UserTableViewModel>();

                return result.FirstOrDefault();
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="models"></param>
        public void UpdateUserTable(UserTableViewModel models)
        {
            using (var db = new AALifeDbContext())
            {
                var userTable = models.Adapt<UserTable>();
                db.Entry<UserTable>(userTable).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 更新用户--批量
        /// </summary>
        /// <param name="models"></param>
        public void UpdateUserTable(List<UserTableViewModel> models)
        {
            using (var db = new AALifeDbContext())
            {
                models.ForEach((model) => {
                    var userTable = model.Adapt<UserTable>();
                    db.Entry<UserTable>(userTable).State = System.Data.Entity.EntityState.Modified;
                });

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="models"></param>
        public void AddUserTable(UserTableViewModel models)
        {
            using (var db = new AALifeDbContext())
            {
                var userTable = models.Adapt<UserTable>();
                db.UserTable.Add(userTable);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 增加用户--批量
        /// </summary>
        /// <param name="models"></param>
        public void AddUserTable(List<UserTableViewModel> models)
        {
            using (var db = new AALifeDbContext())
            {
                var userTable = models.Adapt<List<UserTable>>();
                db.UserTable.AddRange(userTable);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 更新用户关像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userImage"></param>
        public void UpdateUserImage(int userId, string userImage)
        {
            using (var db = new AALifeDbContext())
            {
                var user = db.Set<UserTable>().Find(userId);
                user.UserImage = userImage;

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="models"></param>
        public void RemoveUserTable(UserTableViewModel models)
        {
            using (var db = new AALifeDbContext())
            {
                var userTable = models.Adapt<UserTable>();
                db.Entry<UserTable>(userTable).State = System.Data.Entity.EntityState.Deleted;

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除用户--批量
        /// </summary>
        /// <param name="models"></param>
        public void RemoveUserTable(List<UserTableViewModel> models)
        {
            using (var db = new AALifeDbContext())
            {
                models.ForEach((model) => {
                    var userTable = model.Adapt<UserTable>();
                    db.Entry<UserTable>(userTable).State = System.Data.Entity.EntityState.Deleted;
                });

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="models"></param>
        /// <param name="userName"></param>
        public bool CheckUserExists(UserTableViewModel models, string type)
        {
            using (var db = new AALifeDbContext())
            {
                var exists = false;
                switch(type)
                {
                    case "UserName":
                        exists = db.Set<UserTable>().Any(a => a.UserName == models.UserName);
                        break;
                }

                return exists;
            }
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserTableView> GetAll()
        {
            var db = new AALifeDbContext();

            //默认
            var lists = db.Set<UserTableView>()
                .AsNoTracking();

            return lists;
        }

    }
}
