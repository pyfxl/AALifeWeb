using AALife.Core.Infrastructure.Mapper;
using AALife.Data.Domain;
using AALife.WebMvc.Models.ViewModel;

namespace AALife.WebMvc.Infrastructure.Mapper
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }
        
        #region Item

        public static ItemViewModel ToModel(this ItemTable entity)
        {
            return entity.MapTo<ItemTable, ItemViewModel>();
        }

        public static ItemTable ToEntity(this ItemViewModel model)
        {
            return model.MapTo<ItemViewModel, ItemTable>();
        }

        public static ItemTable ToEntity(this ItemViewModel model, ItemTable destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region User

        public static UserViewModel ToModel(this UserTable entity)
        {
            return entity.MapTo<UserTable, UserViewModel>();
        }

        public static UserTable ToEntity(this UserViewModel model)
        {
            return model.MapTo<UserViewModel, UserTable>();
        }

        public static UserTable ToEntity(this UserViewModel model, UserTable destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Permission

        public static PermissionViewModel ToModel(this UserPermission entity)
        {
            return entity.MapTo<UserPermission, PermissionViewModel>();
        }

        public static UserPermission ToEntity(this PermissionViewModel model)
        {
            return model.MapTo<PermissionViewModel, UserPermission>();
        }

        public static UserPermission ToEntity(this PermissionViewModel model, UserPermission destination)
        {
            return model.MapTo(destination);
        }

        #endregion

    }
}