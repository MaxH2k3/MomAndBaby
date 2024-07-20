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

        public async Task<User?> AddNewUser(LoginUserDto loginUser)
        {
            var userCheck = await _unitOfWork.UserRepository.GetUserByEmail(loginUser.Email);

            if (userCheck == null)
            {

                userCheck = new User();
                AuthenHelper.CreatePasswordHash(loginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userCheck.Email = loginUser.Email;
                userCheck.Username = loginUser.UserName;
                userCheck.FullName = loginUser.UserName;
                userCheck.Id = Guid.NewGuid();
                userCheck.Password = passwordHash;
                userCheck.PasswordSalt = passwordSalt;

                await _unitOfWork.UserRepository.AddUser(userCheck);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            await GenerateAndSendOTP(userCheck.Email!, userCheck.FullName!, userCheck.Id);

            return userCheck;
        }

        public async Task<bool> SigninGoogle(User user)
        {
            var userCheck = await _unitOfWork.UserRepository.GetUserByEmail(user.Email);
            if (userCheck == null)
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
            await GenerateAndSendOTP(user.Email!, user.FullName!, user.Id);
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
                if (updateUserDto.Password != null)
                {
                    AuthenHelper.CreatePasswordHash(updateUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    existUser.Password = passwordHash;
                    existUser.PasswordSalt = passwordSalt;
                }
            }
            await _unitOfWork.UserRepository.UpdateUser(existUser);
            return existUser;
        }

        public async Task<User> getUserById(Guid? id)
        {
            return await _unitOfWork.UserRepository.getUserById(id);
        }


        public async Task<bool> GenerateAndSendOTP(string email, string userName, Guid userId)
        {
            var otp = AuthenHelper.GenerateOTP();
            var existingUserValidation = await _unitOfWork.UserValidationRepository.GetUser(userId);

            bool isNewUserValidation = existingUserValidation == null;
            var userValidation = existingUserValidation ?? new UserValidation { UserId = userId };

            userValidation.Otp = otp;
            userValidation.ExpiredAt = DateTime.UtcNow.AddMinutes(5);
            userValidation.CreatedAt = userValidation.CreatedAt == default ? DateTime.UtcNow : userValidation.CreatedAt;

            if (isNewUserValidation)
            {
                await _unitOfWork.UserValidationRepository.AddUserValidation(userValidation);
            }
            else
            {
                await _unitOfWork.UserValidationRepository.UpdateUserValidation(userValidation);
            }

            var emailSent = await _emailService.SendEmailWithTemplate("Your OTP Code", new UserMailDto()
            {
                UserName = userName,
                Email = email,
                OTP = otp,
            });
            if (emailSent)
            {
                return true; // Return 200 OK if everything is successful
            }
            else
            {
                return false; // Return 500 Internal Server Error if email sending failed
            }

        }

        public async Task<bool> ValidateOTP(ValidateOtpDTO validateOtp)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmail(validateOtp.Email!);
            if (user == null)
            {
                return false;
            }

            var existOTP = await _unitOfWork.UserValidationRepository.GetUser(user.Id);
            if (existOTP!.Otp == null)
            {
                return false;
            }
            else if (existOTP.ExpiredAt < DateTime.UtcNow)
            {
                return false;
            }

            if (existOTP.Otp == validateOtp.Otp)
            {
                return await ProcessValidOTP(existOTP);
            }
            return false;
        }

        public async Task<bool> ProcessValidOTP(UserValidation userValidation)
        {

            await _unitOfWork.UserValidationRepository.UpdateUserValidation(userValidation);
            return await _unitOfWork.SaveChangesAsync();

        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _unitOfWork.UserRepository.GetAllUsers();
        }

        public async Task<User> UpdateStatus(User user)
        {
            return await _unitOfWork.UserRepository.UpdateStatus(user);
        }

        public async Task<User?> GetUserById(Guid? id)
        {
           return await _unitOfWork.UserRepository.GetUserById(id);
        }
    }
}
