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

        /// <summary>
        /// 获取多级目录
        /// </summary>
        /// <param name="deptment"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string GetFormattedBreadCrumb(UserDeptment deptment, string separator = "--");

    }
}
