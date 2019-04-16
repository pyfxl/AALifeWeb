using AALife.Core.Infrastructure.Mapper;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Models.ViewModel;
using AutoMapper;
using System;
using System.Data.Entity;

namespace AALife.WebMvc.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class MapperConfiguration : IMapperConfiguration
    {
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <returns>Mapper configuration action</returns>
        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            //TODO remove 'CreatedOnUtc' ignore mappings because now presentation layer models have 'CreatedOn' property and core entities have 'CreatedOnUtc' property (distinct names)

            Action<IMapperConfigurationExpression> action = cfg =>
            {
                //item
                cfg.CreateMap<ItemTable, ItemViewModel>()
                    .ForMember(dest => dest.UserName, mo => mo.MapFrom(src => src.User.UserNameFull()))
                    .ForMember(dest => dest.ItemTypeName, mo => mo.MapFrom(src => AALife.Data.Constant.ItemTypeDic[src.ItemType]));

                //cfg.CreateMap<ItemViewModel, ItemTable>()
                //    .ForMember(dest => dest.User, src => src.Ignore());

                //zhuanzhang
                cfg.CreateMap<ZhuanZhangTable, ZhuanZhangViewModel>()
                    .ForMember(dest => dest.ZhuanZhangFromName, src => src.Ignore())
                    .ForMember(dest => dest.ZhuanZhangToName, src => src.Ignore());

                //user
                cfg.CreateMap<UserTable, UserViewModel>()
                    .ForMember(dest => dest.UserNameFull, mo => mo.MapFrom(src => src.UserNameFull()))
                    .ForMember(dest => dest.UserImageFull, mo => mo.MapFrom(src => src.UserImageFull()));

                //user manage
                cfg.CreateMap<UserTable, UserManageViewModel>()
                    .ForMember(dest => dest.UserNameFull, mo => mo.MapFrom(src => src.UserNameFull()))
                    .ForMember(dest => dest.UserImageFull, mo => mo.MapFrom(src => src.UserImageFull()));

                //user manage
                cfg.CreateMap<UserTable, UserDetailViewModel>()
                    .ForMember(dest => dest.UserNameFull, mo => mo.MapFrom(src => src.UserNameFull()))
                    .ForMember(dest => dest.UserImageFull, mo => mo.MapFrom(src => src.UserImageFull()))
                    .ForMember(dest => dest.JoinDay, mo => mo.MapFrom(src => DateTime.Now.Subtract(src.CreateDate).Days + 1))
                    .ForMember(dest => dest.ItemCount, mo => mo.MapFrom(src => src.ItemTables.Count));

                //user role
                cfg.CreateMap<UserTable, UserRoleViewModel>()
                    .ForMember(dest => dest.UserNameFull, mo => mo.MapFrom(src => src.UserNameFull()))
                    .ForMember(dest => dest.UserImageFull, mo => mo.MapFrom(src => src.UserImageFull()));

                //permission
                cfg.CreateMap<PermissionViewModel, UserPermission>()
                    .ForMember(dest => dest.UserRoles, src => src.Ignore());

                //deptment
                cfg.CreateMap<UserDeptment, UserDeptmentModel>()
                    .ForMember(dest => dest.CategoryName, src => src.Ignore());

                //position
                cfg.CreateMap<UserPosition, UserPositionModel>();

            };
            return action;
        }

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }
    }
}