﻿using MomAndBaby.Configuration.Uow;
using MomAndBaby.Entity;

namespace MomAndBaby.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Article> GetListArticle()
        {
            return _unitOfWork.ArticleRepository.GetListArticle();
        }
    }
}
