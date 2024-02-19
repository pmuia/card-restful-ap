

using Core.Domain.Entities.UserModule.Aggregates;

namespace Core.Management.Interfaces.AdminModule
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> CreateUser(string email, byte role, string password, string createdBy);
        Task<bool> EditUser(long userId, string email, byte role, string password, string modifiedBy);
        Task<User> GetUserById(long userId);
        Task<(List<User> Users, int newStartIndex, int newPageSize, int totalCount)> GetUsers(string searchTerm, int offset, int pageSize);
    }
}
