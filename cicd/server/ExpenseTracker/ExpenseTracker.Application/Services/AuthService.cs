using AutoMapper;
using ExpenseTracker.Application.Common;
using ExpenseTracker.Application.Features.Auth.Register;
using ExpenseTracker.Application.Interfaces.Persistence;
using ExpenseTracker.Application.Interfaces.Repository;
using ExpenseTracker.Application.Interfaces.Services;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _UserRepo;
        private readonly IUserRoleMappingRepository _UserRoleMappingRepo;
        private readonly IAuthProviderRepository _AuthProviderRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        

        public AuthService(
            IUnitOfWork uow,
            IUserRepository UserRepo,
            IMapper mapper,
            IUserRoleMappingRepository UserRoleMappingRepo,
            IAuthProviderRepository AuthProviderRepository,
            IRoleRepository roleRepository
            
        )
        {
            _uow = uow;
            _mapper = mapper;   
            _UserRepo = UserRepo;
            _UserRoleMappingRepo = UserRoleMappingRepo;
            _AuthProviderRepository = AuthProviderRepository;
            _roleRepository = roleRepository;
        }



        public async Task<Result<RegisterResponse>> AuthenticateUser(RegisterRequest request)
        { 

            RegisterResponse registerResponse = null;
            await _uow.BeginTransactionAsync();
            try
            {

                //# if email id exists
                User user = await _UserRepo.GetByEmailAsync(request.Email);

                //# exists then check provider 
                //# create user
                if (user == null) 
                {
                    user = new User(request.Name, request.Email, request?.PhoneNumber);
                    await _UserRepo.AddAsync(user);
                    var userRole = await _roleRepository.GetRoleByCode(RoleEnum.User);
                    user.AddRole(userRole.Id);
                }
                
                registerResponse = _mapper.Map<RegisterResponse>(user);
                var response = Result<RegisterResponse>.Success(registerResponse);

                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();
                return response;
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync();
                throw;
            }
            
        }
    }
}
