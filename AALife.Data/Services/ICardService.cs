using AALife.Core.Services;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public interface ICardService : IBaseUserService<CardTable>, IBaseService<CardTable>
    {
    }
}
