using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Repository.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _unitOfWork.CategoryRepository.GetAllCategory();
        }
    }
}
