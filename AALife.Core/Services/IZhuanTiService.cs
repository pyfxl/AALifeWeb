using AALife.Core.Domain;
using System.Collections.Generic;

namespace AALife.Core.Services
{
    public interface IZhuanTiService : IBaseUserService<ZhuanTiTable>, IBaseService<ZhuanTiTable>
    {
        ZhuanTiTable GetZhuanTi(int userId, int zhuanTiId);

        int GetMaxId(int userId);
    }
}
