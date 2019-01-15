using AALife.Core.Domain;

namespace AALife.Core.Services
{
    public interface IUserService
    {
        IPagedList<UserTable> GetAllUser(string userName = "", int pageIndex = 0, int pageSize = int.MaxValue);

        UserTable GetUser(int userId);

        UserTable GetUserByUserName(string userName);

    }
}
