using AALife.Core.Domain;
using System.Collections.Generic;

namespace AALife.Core.Services
{
    public interface ICardService : IBaseUserService<CardTable>, IBaseService<CardTable>
    {
        CardTable GetCard(int userId, int cardId);

        int GetMaxId(int userId);
    }
}
