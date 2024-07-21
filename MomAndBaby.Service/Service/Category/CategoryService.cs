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

        public async Task<bool> NameExistAsync(string categoryName)
        {
            var isExist = await _unitOfWork.CategoryRepository.NameExistAsync(categoryName);
            return isExist;
        }

        public async Task<bool> AddCategory(string categoryName)
        {
            var category = new Category
            {
                Name = categoryName
            };

            await _unitOfWork.CategoryRepository.AddCategory(category);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var check = await _unitOfWork.CategoryRepository.HasProduct(categoryId);
            if (check)
            {
                throw new ArgumentException("There are products associated with this category.");
            }
            
            await _unitOfWork.CategoryRepository.DeleteCategory(categoryId);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
