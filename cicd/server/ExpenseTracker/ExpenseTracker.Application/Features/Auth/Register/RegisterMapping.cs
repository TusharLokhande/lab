using AutoMapper;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Features.Auth.Register
{
    internal class RegisterMapping: Profile
    {
        public RegisterMapping()
        { 
            CreateMap<User, RegisterResponse>();
            CreateMap<RegisterRequest, User>();
        }
    }

}
