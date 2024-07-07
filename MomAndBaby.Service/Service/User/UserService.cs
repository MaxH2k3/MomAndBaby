using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.UserDto;
using MomAndBaby.Repository.Uow;
using MomAndBaby.Service.Helper;

namespace MomAndBaby.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddNewUser(LoginUserDto loginUser)
        {
            var userCheck = await _unitOfWork.UserRepository.GetUserByEmail(loginUser.Email);
            if (userCheck == null)
            {
                var userEntity = new User();

                AuthenHelper.CreatePasswordHash(loginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userEntity.Email = loginUser.Email;
                userEntity.Username = loginUser.UserName;
                userEntity.Id = Guid.NewGuid();
                userEntity.Password = passwordHash;
                userEntity.PasswordSalt = passwordSalt;
               
                await _unitOfWork.UserRepository.AddUser(userEntity);
            }
            else
            {
                return false;
            }
                
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> SigninGoogle(User user)
        {
            var userCheck = await _unitOfWork.UserRepository.GetUserByEmail(user.Email);
            if(userCheck == null)
            {
                await _unitOfWork.UserRepository.AddUser(user);
                return await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                return true;
            }
            
            
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            
            return user;
        }

        public async Task<User?> Login(string userSelection, string password)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameOrEmail(userSelection);

            if (user == null)
            {
                return null;
            }

            if (!AuthenHelper.VerifyPasswordHash(password, user.Password, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<User> UpdateUser(string email, UpdateUserDto updateUserDto)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByEmail(email);
            if (existUser != null)
            {
                existUser.Username = updateUserDto.UserName;
                existUser.FullName = updateUserDto.FullName;
                if(updateUserDto.Password != null)
                {
                    AuthenHelper.CreatePasswordHash(updateUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    existUser.Password = passwordHash;
                    existUser.PasswordSalt = passwordSalt;
                }
            }
            await _unitOfWork.UserRepository.UpdateUser(existUser);
            return existUser;
        }
    }
}
