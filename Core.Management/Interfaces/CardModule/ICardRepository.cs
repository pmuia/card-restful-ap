

using Core.Domain.Entities.CardModule.Aggregates;

namespace Core.Management.Interfaces.CardModule
{
    public interface ICardRepository : IGenericRepository<Card>
    {
        Task<Card> CreateCard(long userId, string name, string description, string color, string createdBy);
        Task<bool> EditCard(long cardId, long userId, string name, string description, string color, byte recordStatus, string modifiedBy);
        Task<Card> GetCardById(long CardId);
        Task<(List<Card> Cards, int newStartIndex, int newPageSize, int totalCount)> GetCards(string searchTerm, int offset, int pageSize);
        Task<(List<Card> Cards, int newStartIndex, int newPageSize, int totalCount)> GetCardsByUser(string searchTerm, int offset, int pageSize, long userId);
    }
}
