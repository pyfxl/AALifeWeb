using AALife.Core.Domain;
using AALife.WebMvc.Models.ViewModel;
using AutoMapper;
using System;

namespace AALife.WebMvc.Mapper
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
                    .ForMember(dest => dest.UserName,
                        mo => mo.MapFrom(src => src.User != null ? src.User.UserNickName != "" ? src.User.UserNickName : src.User.UserName : null));
                cfg.CreateMap<ItemViewModel, ItemTable>()
                    .ForMember(dest => dest.User, src => src.Ignore());

                //user
                cfg.CreateMap<UserTable, UserViewModel>()
                    .ForMember(dest => dest.UserFromName,
                        mo => mo.MapFrom(src => src.UserFromTable != null ? src.UserFromTable.UserFromName : null))
                    .ForMember(dest => dest.UserLevelName,
                        mo => mo.MapFrom(src => src.UserLevelTable != null ? src.UserLevelTable.UserLevelName : null));
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