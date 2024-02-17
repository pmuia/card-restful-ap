using Core.Domain.Infrastructure.Database;
using Core.Management.Interfaces;

namespace Core.Management.Repositories
{
    public class DataServiceFactory<T> : IDataServiceFactory<T> where T : class
    {
        public DataServiceFactory(CardContext context)
        {
            Invoke = new GenericRepository<T>(context);
        }
        public IGenericRepository<T> Invoke { get; }
    }
}
