﻿using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service
{
    public interface IUserValidationService
    {
        Task<UserValidation?> GetUser(Guid userId);
    }
}
