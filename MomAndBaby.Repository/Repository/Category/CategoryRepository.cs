using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MomAndBabyContext _context;

        public CategoryRepository(MomAndBabyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            _context.Categories.Remove(category);
        }

        public async Task<bool> NameExistAsync(string categoryName)
        {
            return await _context.Categories.AnyAsync(x => x.Name.ToLower().Equals(categoryName));
        }

        public async Task<bool> HasProduct(int categoryId)
        {
            return await _context.Products.AnyAsync(x => x.CategoryId == categoryId);
        }
    }
}
