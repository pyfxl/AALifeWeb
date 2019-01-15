using AALife.Core.Domain;
using System.Collections.Generic;

namespace AALife.Core.Services
{
    public interface ICardService
    {
        IList<CardTable> GetAllCard(int userId);

        CardTable GetCard(int userId, int cardId);
    }
}
