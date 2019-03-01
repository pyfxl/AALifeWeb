﻿using AALife.Core.Services;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public interface IUserRoleService : IBaseService<UserRole>
    {
        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        UserRole GetUserRoleBySystemName(string systemName);
    }
}
