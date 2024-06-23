using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MomAndBabyContext _context;

        public ArticleRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
