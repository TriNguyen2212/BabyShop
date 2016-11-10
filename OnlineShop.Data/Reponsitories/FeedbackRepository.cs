using BabyShop.Data.Infrastructure;
using BabyShop.Model.Models;

namespace BabyShop.Data.Reponsitories
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
    }

    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}