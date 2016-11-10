using BabyShop.Data.Infrastructure;
using BabyShop.Model.Models;

namespace BabyShop.Data.Reponsitories
{
    public interface IErrorRepository : IRepository<Error>
    {
    }

    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}