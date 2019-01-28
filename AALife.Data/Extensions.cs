using System;

namespace AALife.Data
{
    public static class Extensions
    {
        #region 基类

        public static void LiveOn(this UserEntity entity)
        {
            entity.ModifyDate = DateTime.Now;
            entity.Synchronize = 1;
            entity.Live = 1;
        }

        public static void LiveOff(this UserEntity entity)
        {
            entity.ModifyDate = DateTime.Now;
            entity.Synchronize = 1;
            entity.Live = 0;
        }

        #endregion

    }
}
