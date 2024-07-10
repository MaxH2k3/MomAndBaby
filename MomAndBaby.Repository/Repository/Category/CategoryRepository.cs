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
            return await _context.Categories.ToListAsync();
        }
    }
}
