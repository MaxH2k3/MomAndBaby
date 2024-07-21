using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Repository.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<UserValidation?> GetUser(Guid userId)
        {
            return _unitOfWork.UserValidationRepository.GetUser(userId);
        }
    }
}
