

using Core.Domain.Entities.CardModule.Aggregates;
using Core.Domain.Infrastructure.Database;
using Core.Domain.Utils;
using Core.Management.Interfaces.CardModule;
using IdGen;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq.Expressions;
using System.Xml.Linq;

using static Core.Management.Repositories.HelperRepository;

namespace Core.Management.Repositories.CardModule
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        private readonly IIdGenerator<long> idGenerator;

        public CardRepository(CardContext context, IIdGenerator<long> idGenerator) : base(context)
        {
            this.idGenerator = idGenerator;
        }
        private CardContext Context => context as CardContext;
        public async Task<Card> CreateCard(string name, string description, string color, string createdBy)
        {
            Card Card = new Card

            {
                CardId = idGenerator.CreateId(),
                Name = name,
                Description = description,
                Color = color,
                RecordStatus = (int)RecordStatus.ToDo,
                CreatedBy = createdBy
            };

            await Context.Cards.AddAsync(Card).ConfigureAwait(false);

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return Card;
        }

        public async Task<bool> EditCard(long cardId, string name, string description, string color, byte recordStatus, string modifiedBy)
        {
            Card Card = await ValidatedFind(x => x.CardId == cardId).ConfigureAwait(false);

            Card.Name = name;
            Card.Description = description;
            Card.Color = color;
            Card.RecordStatus = recordStatus;
            Card.ModifiedBy = modifiedBy;

            return await Context.SaveChangesAsync().ConfigureAwait(false) > 0;

        }

        public async Task<Card> GetCardById(long CardId)
        {
            return await Context.Cards.AsNoTracking().FirstOrDefaultAsync(x => x.CardId == CardId).ConfigureAwait(false);

        }
        public async Task<(List<Card> Cards, int newStartIndex, int newPageSize, int totalCount)> GetCards(string searchTerm, int offset, int pageSize)
        {
            ValidatedParameter("searchTerm", searchTerm, out searchTerm, throwException: false);
            searchTerm = searchTerm.ToUpper();

            int totalCount = await Context.Cards.AsNoTracking().AsExpandable().CountAsync().ConfigureAwait(false);

            Expression<Func<Card, bool>> expressionFilter = x => true;

            if (searchTerm.Length < 1)
                return (await Context.Cards
                .AsNoTracking()
                .AsExpandable()
                .Where(expressionFilter)
                .OrderByDescending(x => x.CreatedAt).Skip(offset).Take(pageSize).ToListAsync().ConfigureAwait(false),
            offset + pageSize, pageSize, totalCount);

            Expression<Func<Card, bool>> innerExpressionFilter = x => false;

            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.Name, $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.Description, $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.Color, $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.CreatedAt.ToString(), $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.CreatedBy, $"%{searchTerm}%"));

            expressionFilter = expressionFilter.And(innerExpressionFilter);

            return (await Context.Cards
                .AsNoTracking()
                .AsExpandable()
                .Where(expressionFilter)
                .OrderByDescending(x => x.CreatedAt).Skip(offset).Take(pageSize).ToListAsync().ConfigureAwait(false),
            offset + pageSize, pageSize, totalCount);
        }

        public async Task<(List<Card> Cards, int newStartIndex, int newPageSize, int totalCount)> GetCardsByUser(string searchTerm, int offset, int pageSize, string createdBy)
        {
            ValidatedParameter("searchTerm", searchTerm, out searchTerm, throwException: false);
            searchTerm = searchTerm.ToUpper();

            int totalCount = await Context.Cards.AsNoTracking().AsExpandable().CountAsync().ConfigureAwait(false);

            Expression<Func<Card, bool>> expressionFilter = x => true;

            expressionFilter = expressionFilter.And(x => x.CreatedBy == createdBy);

            if (searchTerm.Length < 1)
                return (await Context.Cards
                .AsNoTracking()
                .AsExpandable()
                .Where(expressionFilter)
                .OrderByDescending(x => x.CreatedAt).Skip(offset).Take(pageSize).ToListAsync().ConfigureAwait(false),
            offset + pageSize, pageSize, totalCount);

            Expression<Func<Card, bool>> innerExpressionFilter = x => false;

            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.Name, $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.Description, $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.Color, $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.CreatedAt.ToString(), $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.CreatedBy, $"%{searchTerm}%"));

            expressionFilter = expressionFilter.And(innerExpressionFilter);

            return (await Context.Cards
                .AsNoTracking()
                .AsExpandable()
                .Where(expressionFilter)
                .OrderByDescending(x => x.CreatedAt).Skip(offset).Take(pageSize).ToListAsync().ConfigureAwait(false),
            offset + pageSize, pageSize, totalCount);
        }
    }
}
