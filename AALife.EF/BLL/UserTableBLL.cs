using AALife.EF.Models;
using AALife.EF.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        /// 构造函数
        /// </summary>
        public UserTableBLL()
        {
            AutoMapper.Mapper.Initialize(m => m.CreateMap<UserTable, UserTableViewModel>()
                .ForMember(vm => vm.UserID, dto => dto.MapFrom(mo => mo.UserID))
                .ForMember(vm => vm.UserName, dto => dto.MapFrom(mo => mo.UserName))
                .ForMember(vm => vm.UserPassword, dto => dto.MapFrom(mo => mo.UserPassword))
                .ForMember(vm => vm.UserNickName, dto => dto.MapFrom(mo => mo.UserNickName))
                .ForMember(vm => vm.UserImage, dto => dto.MapFrom(mo => mo.UserImage))
                .ForMember(vm => vm.UserPhone, dto => dto.MapFrom(mo => mo.UserPhone))
                .ForMember(vm => vm.UserEmail, dto => dto.MapFrom(mo => mo.UserEmail))
                .ForMember(vm => vm.UserTheme, dto => dto.MapFrom(mo => mo.UserTheme))
                .ForMember(vm => vm.UserLevel, dto => dto.MapFrom(mo => mo.UserLevel))
                .ForMember(vm => vm.UserFrom, dto => dto.MapFrom(mo => mo.UserFrom))
                .ForMember(vm => vm.UserFromName, dto => dto.MapFrom(mo => mo.UserFromTable.UserFromName))
                .ForMember(vm => vm.ModifyDate, dto => dto.MapFrom(mo => mo.ModifyDate))
                .ForMember(vm => vm.CreateDate, dto => dto.MapFrom(mo => mo.CreateDate))
                .ForMember(vm => vm.UserCity, dto => dto.MapFrom(mo => mo.UserCity))
                .ForMember(vm => vm.UserMoney, dto => dto.MapFrom(mo => mo.UserMoney))
                .ForMember(vm => vm.UserWorkDay, dto => dto.MapFrom(mo => mo.UserWorkDay))
                .ForMember(vm => vm.UserWorkDayName, dto => dto.MapFrom(mo => mo.WorkDayTable.WorkDayName))
                .ForMember(vm => vm.UserFunction, dto => dto.MapFrom(mo => mo.UserFunction))
                .ForMember(vm => vm.CategoryRate, dto => dto.MapFrom(mo => mo.CategoryRate))
                .ForMember(vm => vm.Synchronize, dto => dto.MapFrom(mo => mo.Synchronize))
                .ForMember(vm => vm.MoneyStart, dto => dto.MapFrom(mo => mo.MoneyStart))
                .ForMember(vm => vm.IsUpdate, dto => dto.MapFrom(mo => mo.IsUpdate)).ReverseMap());
        }

        /// <summary>
        /// 取用户根据日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<UserTableViewModel> GetUserTable(DateTime startDate, DateTime endDate)
        {
            using (var db = new AALifeDbContext())
            {
                var lists = db.Set<UserTable>().Where(a => a.CreateDate >= startDate && a.CreateDate <= endDate).OrderByDescending(a => a.UserID);
                
                var result = lists
                    .Include(a=>a.UserFromTable)
                    .Include(a=>a.WorkDayTable).ToList();

                return AutoMapper.Mapper.Map<List<UserTable>, List<UserTableViewModel>>(result);
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
                    .Include(a => a.WorkDayTable).ToList();

                return AutoMapper.Mapper.Map<List<UserTable>, List<UserTableViewModel>>(result);
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
                var userTable = AutoMapper.Mapper.Map<UserTableViewModel, UserTable>(models);
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
                    var userTable = AutoMapper.Mapper.Map<UserTableViewModel, UserTable>(model);
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
                var userTable = AutoMapper.Mapper.Map<UserTableViewModel, UserTable>(models);
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
                var userTable = AutoMapper.Mapper.Map<List<UserTableViewModel>, List<UserTable>>(models);
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
                var userTable = AutoMapper.Mapper.Map<UserTableViewModel, UserTable>(models);
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
                    var userTable = AutoMapper.Mapper.Map<UserTableViewModel, UserTable>(model);
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

    }
}
