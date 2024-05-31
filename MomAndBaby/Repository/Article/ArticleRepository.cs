using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly MomAndBabyContext _context;

        public ArticleRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        public IEnumerable<Article> GetListArticle()
        {
            return _context.Articles.ToList();
        }
    }
}
