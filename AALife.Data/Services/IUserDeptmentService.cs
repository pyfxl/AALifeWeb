using AALife.Core.Services;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public interface IUserDeptmentService : IBaseService<UserDeptment>
    {
        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        UserDeptment GetUserDeptmentByName(string name);
    }
}
