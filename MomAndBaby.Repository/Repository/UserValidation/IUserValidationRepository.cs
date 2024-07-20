using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Repository.Repository
{
    public interface IUserValidationRepository
    {
        Task<UserValidation?> GetUser(Guid userId);
        Task AddUserValidation (UserValidation userValidation);
        Task UpdateUserValidation (UserValidation userValidation);
        Task<UserValidation?> GetUserValidationById(Guid userId);
        
    }
}
