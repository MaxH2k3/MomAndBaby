﻿using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public interface IArticleRepository
    {
        public IEnumerable<Article> GetListArticle();
    }
}
