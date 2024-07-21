using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Repository.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task AddCategory(Category category);
        Task DeleteCategory(int categoryId);
        Task<bool> NameExistAsync(string categoryName);
        Task<bool> HasProduct(int categoryId);
    }
}
