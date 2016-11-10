using BabyShop.Data.Infrastructure;
using BabyShop.Model.Models;

namespace BabyShop.Data.Reponsitories
{
    public interface IColorRepository : IRepository<Color>
    {
    }

    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}