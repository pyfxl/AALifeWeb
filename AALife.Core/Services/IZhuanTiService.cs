using AALife.Core.Domain;
using System.Collections.Generic;

namespace AALife.Core.Services
{
    public interface IZhuanTiService
    {
        IList<ZhuanTiTable> GetAllZhuanTi(int userId);

        ZhuanTiTable GetZhuanTi(int userId, int zhuanTiId);
    }
}
