using AutoMapper;
using MomAndBaby.Configuration.Uow;
using MomAndBaby.Dto.UserDto;
using MomAndBaby.Entity;
using MomAndBaby.Helper;

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
            var userEntity = new User();
            var authenHelper = new AuthenHelper();

            authenHelper.CreatePasswordHash(loginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userEntity.Email = loginUser.Email;
            userEntity.Username = loginUser.UserName;
            userEntity.Id = Guid.NewGuid(); 
            userEntity.Password = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
            await _unitOfWork.UserRepository.AddUser(userEntity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> AddNewUserGoogle(User user)
        {
            await _unitOfWork.UserRepository.AddUser(user);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            
            return user;
        }

        public async Task<bool> Login(string userSelection, string password)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameOrEmail(userSelection);

            if (user == null)
            {
                return false;
            }
            var authenHelper = new AuthenHelper();
            if (!authenHelper.VerifyPasswordHash(password, user.Password, user.PasswordSalt))
            {
                return false;
            }
            return true;
        }
    }
}
