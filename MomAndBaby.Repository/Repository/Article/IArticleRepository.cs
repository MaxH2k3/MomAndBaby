using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IArticleRepository
    {
        public IEnumerable<Article> GetListArticle();
    }
}
