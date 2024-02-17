
using System.Linq.Expressions;

namespace Core.Management.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> EditUser(long userId, string email, string password, string firstname, string lastname, string usertype, string profileimage, string phone, string country, string provincestatecounty, string physicaladdress, string facebookid, string activationcode, int active, string appused, DateTime datecreated, DateTime dateupdated, int status);
        IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeExpressions);
        IEnumerable<TEntity> Fetch(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeExpressions);
        IEnumerable<TEntity> GetAll(string orderByExpression, out int totalCount, int pageSize = 1, int startIndex = 0);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate, string orderByExpression, int pageSize = 1, int startIndex = 0);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate, string orderByExpression, out int totalCount, int pageSize = 1, int startIndex = 0);
        Task<TEntity> ToggleActiveStatus(Expression<Func<TEntity, bool>> predicate, bool active, string propertyName = "IsActive");
        Task<bool> ValidatedAny(Expression<Func<TEntity, bool>> predicate, bool throwException = false, bool inverseCheck = true);
        Task<TEntity> ValidatedFind(Expression<Func<TEntity, bool>> predicate, bool throwException = true, bool inverseCheck = false, params Expression<Func<TEntity, object>>[] includeExpressions);
        Task<TEntity> ValidatedSearch(Expression<Func<TEntity, bool>> predicate, bool checkActive = false, string activePropertyName = "IsActive", bool throwException = true, bool inverseCheck = false, params Expression<Func<TEntity, object>>[] includeExpressions);
    }
}
