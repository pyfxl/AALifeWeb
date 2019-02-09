using AALife.Data.Domain;
using AALife.Core.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using AutoMapper;
using System;

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
                    .ForMember(dest => dest.UserName, mo => mo.MapFrom(src => src.User != null ? src.User.UserNickName != "" ? src.User.UserNickName : src.User.UserName : null))
                    .ForMember(dest => dest.ItemTypeName, mo => mo.MapFrom(src => Constant.ItemTypeDic[src.ItemType]));

                cfg.CreateMap<ItemViewModel, ItemTable>()
                    .ForMember(dest => dest.User, src => src.Ignore());

                //zhuanzhang
                cfg.CreateMap<ZhuanZhangTable, ZhuanZhangViewModel>()
                    .ForMember(dest => dest.ZhuanZhangFromName, src => src.Ignore())
                    .ForMember(dest => dest.ZhuanZhangToName, src => src.Ignore());

                //user
                cfg.CreateMap<UserTable, UserViewModel>()
                    .ForMember(dest => dest.UserSettings, src => src.Ignore());

            };
            return action;
        }

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}