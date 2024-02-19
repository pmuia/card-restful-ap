using Core.Domain.Entities.UserModule.Aggregates;
using Core.Domain.Infrastructure.Database;
using Core.Domain.Utils;
using Core.Management.Interfaces;
using Core.Management.Interfaces.AdminModule;
using IdGen;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

using static Core.Management.Repositories.HelperRepository;

namespace Core.Management.Repositories.AdminModule
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IIdGenerator<long> idGenerator;

        public UserRepository(CardContext context, IIdGenerator<long> idGenerator) : base(context)
        {
            this.idGenerator = idGenerator;
        }
        private CardContext Context => context as CardContext;
        public async Task<User> CreateUser(string email, byte role, string password, string createdBy)
        {
            User User = new User

            {
                UserId = idGenerator.CreateId(),
                Email = email,
                Role = role,
                Password = password,
                CreatedBy = createdBy
            };

            await Context.Users.AddAsync(User).ConfigureAwait(false);

            await Context.SaveChangesAsync().ConfigureAwait(false);

            return User;
        }

        public async Task<bool> EditUser(long userId, string email, byte role, string password, string modifiedBy)
        {
            User user = await ValidatedFind(x => x.UserId == userId).ConfigureAwait(false);

            user.UserId = userId;
            user.Email = email;
            user.Role = role;
            user.Password = password;
            user.ModifiedBy = modifiedBy;

            return await Context.SaveChangesAsync().ConfigureAwait(false) > 0;

        }

        public async Task<User> GetUserById(long userId)
        {
            return await Context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId).ConfigureAwait(false);

        }
        public async Task<(List<User> Users, int newStartIndex, int newPageSize, int totalCount)> GetUsers(string searchTerm, int offset, int pageSize)
        {
            ValidatedParameter("searchTerm", searchTerm, out searchTerm, throwException: false);
            searchTerm = searchTerm.ToUpper();

            int totalCount = await Context.Users.AsNoTracking().AsExpandable().CountAsync().ConfigureAwait(false);

            Expression<Func<User, bool>> expressionFilter = x => true;

            if (searchTerm.Length < 1)
                return (await Context.Users
                .AsNoTracking()
                .AsExpandable()
                .Where(expressionFilter)
                .OrderByDescending(x => x.CreatedAt).Skip(offset).Take(pageSize).ToListAsync().ConfigureAwait(false),
            offset + pageSize, pageSize, totalCount);

            Expression<Func<User, bool>> innerExpressionFilter = x => false;

            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.Email, $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.CreatedAt.ToString(), $"%{searchTerm}%"));
            innerExpressionFilter = innerExpressionFilter.Or(x => EF.Functions.Like(x.CreatedBy, $"%{searchTerm}%"));

            expressionFilter = expressionFilter.And(innerExpressionFilter);

            return (await Context.Users
                .AsNoTracking()
                .AsExpandable()
                .Where(expressionFilter)
                .OrderByDescending(x => x.CreatedAt).Skip(offset).Take(pageSize).ToListAsync().ConfigureAwait(false),
            offset + pageSize, pageSize, totalCount);
        }       
    }
}
