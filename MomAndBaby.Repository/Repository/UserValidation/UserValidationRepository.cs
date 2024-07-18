using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Repository.Repository
{
    public class UserValidationRepository : IUserValidationRepository
    {
        private readonly MomAndBabyContext _context;

        public UserValidationRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        public async Task AddUserValidation(UserValidation userValidation)
        {
            await _context.UserValidation.AddAsync(userValidation);
            await _context.SaveChangesAsync();
        }

        public async Task<UserValidation?> GetUser(Guid userId)
        {
            return await _context.UserValidation.FirstOrDefaultAsync(x => x.UserId!.Equals(userId));
        }

        

        public async Task<UserValidation?> GetUserValidationById(Guid userId)
        {
            var userValidationExisted = await _context.UserValidation.FirstOrDefaultAsync(e => e.UserId.Equals(userId));
            return userValidationExisted;
        }

        public async Task UpdateUserValidation(UserValidation userValidation)
        {
            var existUser = _context.UserValidation.Where(e => e.UserId.Equals(userValidation.UserId)).FirstOrDefault();
            if (existUser != null)
            {
                existUser.Otp = userValidation.Otp;
                existUser.CreatedAt = userValidation.CreatedAt;
                existUser.ExpiredAt = userValidation.ExpiredAt;
            }
            await _context.SaveChangesAsync();
            
        }
    }
}
