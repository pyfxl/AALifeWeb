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
        /// <summary>
        /// 取用户根据日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<UserTable> GetUserTable(DateTime startDate, DateTime endDate)
        {
            using (var db = new AALifeDbContext())
            {
                return db.Set<UserTable>().Where(a => a.CreateDate >= startDate && a.CreateDate <= endDate).OrderByDescending(a => a.UserID).ToList();
            }
        }

        /// <summary>
        /// 取用户根据关键字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<UserTable> GetUserTable(string key)
        {
            using (var db = new AALifeDbContext())
            {
                int userId = 0;
                Int32.TryParse(key, out userId);

                return db.Set<UserTable>().Where(a => a.UserID == userId || a.UserName.Contains(key) || a.UserPassword.Contains(key) || a.UserNickName.Contains(key) || a.UserEmail.Contains(key)).OrderByDescending(a => a.UserID).ToList();
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="models"></param>
        public void UpdateUserTable(UserTable models)
        {
            using (var db = new AALifeDbContext())
            {
                db.Entry<UserTable>(models).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 更新用户--批量
        /// </summary>
        /// <param name="models"></param>
        public void UpdateUserTable(IEnumerable<UserTable> models)
        {
            using (var db = new AALifeDbContext())
            {
                models.ToList().ForEach((model) => {
                    db.Entry<UserTable>(model).State = System.Data.Entity.EntityState.Modified;
                });

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="models"></param>
        public void AddUserTable(UserTable models)
        {
            using (var db = new AALifeDbContext())
            {
                db.UserTable.Add(models);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 增加用户--批量
        /// </summary>
        /// <param name="models"></param>
        public void AddUserTable(IEnumerable<UserTable> models)
        {
            using (var db = new AALifeDbContext())
            {
                db.UserTable.AddRange(models);
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
        public void RemoveUserTable(UserTable models)
        {
            using (var db = new AALifeDbContext())
            {
                db.Entry<UserTable>(models).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除用户--批量
        /// </summary>
        /// <param name="models"></param>
        public void RemoveUserTable(IEnumerable<UserTable> models)
        {
            using (var db = new AALifeDbContext())
            {
                models.ToList().ForEach((model) => {
                    db.Entry<UserTable>(model).State = System.Data.Entity.EntityState.Deleted;
                });

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="models"></param>
        /// <param name="userName"></param>
        public bool CheckUserExists(UserTable models, string type)
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

    }
}
