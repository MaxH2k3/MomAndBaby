using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.UserDto;
using MomAndBaby.Repository.Uow;
using MomAndBaby.Service.Helper;
using MomAndBaby.Service.Service.Email;

namespace MomAndBaby.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> AddNewUser(LoginUserDto loginUser)
        {
            var userCheck = await _unitOfWork.UserRepository.GetUserByEmail(loginUser.Email);
            var userEntity = new User();
            if (userCheck == null)
            {
                

                AuthenHelper.CreatePasswordHash(loginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userEntity.Email = loginUser.Email;
                userEntity.Username = loginUser.UserName;
                userEntity.FullName = loginUser.UserName;
                userEntity.Id = Guid.NewGuid();
                userEntity.Password = passwordHash;
                userEntity.PasswordSalt = passwordSalt;
               
                await _unitOfWork.UserRepository.AddUser(userEntity);
            }
            else
            {
                return false;
            }
                
            await _unitOfWork.SaveChangesAsync();
            return await GenerateAndSendOTP(userEntity.Email!, userEntity.FullName!, userEntity.Otp);
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
                existUser.Address = updateUserDto.Address;
                existUser.PhoneNumber = updateUserDto.PhoneNumber;
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

        public async Task<User> UpdateUserOtp(string email, string Otp)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByEmail(email);
            if (existUser != null)
            {
                existUser.Otp = Otp;
            }
            await _unitOfWork.UserRepository.UpdateUser(existUser);
            return existUser;

        }

        public async Task<bool> GenerateAndSendOTP(string email, string userName, string? Otp)
        {
            var otp = AuthenHelper.GenerateOTP();
            if (Otp == null)
            {
                Otp = otp;
            }
            var updatedUser = UpdateUserOtp(email, otp);

            var emailSent = await _emailService.SendEmailWithTemplate("Your OTP Code", new UserMailDto()
            {
                UserName = userName,
                Email = email,
                OTP = otp,
            });
            return true;

        }

      

       
    }
}
