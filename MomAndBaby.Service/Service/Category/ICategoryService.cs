using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategory();
    Task<bool> NameExistAsync(string categoryName);
    Task<bool> AddCategory(string categoryName);
    Task<bool> DeleteCategory(int categoryId);
}