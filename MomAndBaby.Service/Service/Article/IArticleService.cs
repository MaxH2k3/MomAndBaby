using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Service
{
    public interface IArticleService
    {
        public IEnumerable<Article> GetListArticle();
    
    }
}
