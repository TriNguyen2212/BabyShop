using BabyShop.Data.Infrastructure;
using BabyShop.Model.Models;

namespace BabyShop.Data.Reponsitories
{
    public interface IApplicationRoleGroupRepository : IRepository<ApplicationRoleGroup>
    {
    }

    public class ApplicationRoleGroupRepository : RepositoryBase<ApplicationRoleGroup>, IApplicationRoleGroupRepository
    {
        public ApplicationRoleGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}