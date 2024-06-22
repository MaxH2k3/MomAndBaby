﻿using MomAndBaby.Dto.UserDto;
using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameOrEmail(string userSelection);
        Task<User?> GetUserByEmail(string email);
        Task<User> AddUser(User userRegitser);
    }
}
